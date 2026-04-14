using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Shared.Libraries.Kernel;

public enum AppTypes
{
    [Description("STRING")]
    STRING,
    [Description("INT")]
    INT,
    [Description("DECIMAL")]
    DECIMAL,
    [Description("DOUBLE")]
    DOUBLE,
    [Description("DATETIME")]
    DATETIME,
    [Description("BOOL")]
    BOOL,
    [Description("GUID")]
    GUID,
    [Description("CHAR")]
    CHAR,
    [Description("STRING_LIST")]
    STRING_LIST
}

public class AppTypeConverter
{
    public static dynamic Convert(string value, AppTypes targetType)
    {
        var typeConverters = new Dictionary<AppTypes, Func<string, object>>
    {
        { AppTypes.STRING, val => val },
        { AppTypes.CHAR, val => val },
        { AppTypes.INT, val => int.Parse(val, CultureInfo.InvariantCulture) },
        { AppTypes.DECIMAL, val => decimal.Parse(val, CultureInfo.InvariantCulture) },
        { AppTypes.DOUBLE, val => double.Parse(val, CultureInfo.InvariantCulture) },
        { AppTypes.DATETIME, val => DateTime.Parse(val, CultureInfo.InvariantCulture) },
        { AppTypes.BOOL, val => bool.Parse(val) },
        { AppTypes.GUID, val => Guid.Parse(val) },
        { AppTypes.STRING_LIST, val => val.Split(',').Select(x => x.Trim()).ToArray() }
    };

        if (typeConverters.TryGetValue(targetType, out var converter))
        {
            try
            {
                return converter(value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        throw new NotSupportedException($"Conversion to {targetType} is not supported.");
    }

    public static string CSharpTypeToSqlType(AppTypes type)
    {
        switch (type)
        {
            case AppTypes.STRING:
                return "NVARCHAR(128)";
            case AppTypes.BOOL:
                return "BIT";
            case AppTypes.INT:
                return "int";
            case AppTypes.DECIMAL:
                return "DECIMAL(18,5)";
            case AppTypes.DATETIME:
                return "DATETIME";
            case AppTypes.GUID:
                return "UNIQUEIDENTIFIER";
            case AppTypes.CHAR:
                return "NCHAR(1)";
            default:
                return "NVARCHAR(128)";
        }
    }

    public static Type GetCSharpType(AppTypes typeName)
    {
        switch (typeName)
        {
            case AppTypes.STRING:
                return typeof(string);
            case AppTypes.BOOL:
                return typeof(bool);
            case AppTypes.INT:
                return typeof(int);
            case AppTypes.DECIMAL:
                return typeof(decimal);
            case AppTypes.DATETIME:
                return typeof(DateTime);
            case AppTypes.GUID:
                return typeof(Guid);
            case AppTypes.CHAR:
                return typeof(char);
            default:
                return typeof(string);
        }
    }

    public static List<Dictionary<string, object>> QueryToDictionary(IEnumerable<dynamic> data)
    {
        if (data is null)
            return new();

        var list = new List<Dictionary<string, object>>();

        foreach (var row in data)
        {
            var dict = new Dictionary<string, object>();

            foreach (var kvp in (IDictionary<string, object>)row)
            {
                dict[kvp.Key] = kvp.Value;
            }

            list.Add(dict);
        }

        return list;
    }

    public static Dictionary<string, object> ToDictionary(object obj)
    {
        var dict = new Dictionary<string, object>();

        if (obj == null) return dict;

        foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            dict[property.Name] = property.GetValue(obj)!;
        }

        return dict;
    }

    public static T FromDictionary<T>(T obj, Dictionary<string, object> dictionary, bool ignoreCase = false)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        Type type = obj.GetType();
        BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
        if (ignoreCase) bindingFlags |= BindingFlags.IgnoreCase;

        foreach (var kvp in dictionary)
        {
            try
            {
                PropertyInfo property = type.GetProperty(kvp.Key, bindingFlags);

                if (property != null && property.CanWrite)
                {
                    object convertedValue = ObjectToTyped(kvp.Value, property.PropertyType);
                    property.SetValue(obj, convertedValue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting property '{kvp.Key}': {ex.Message}");
            }
        }

        return obj;
    }

    private static object ObjectToTyped(object value, Type targetType)
    {
        if (value == null)
        {
            if (targetType.IsValueType && Nullable.GetUnderlyingType(targetType) == null)
            {
                throw new InvalidOperationException($"Cannot assign null to non-nullable type {targetType.Name}");
            }

            return null;
        }

        Type nullableType = Nullable.GetUnderlyingType(targetType);
        if (nullableType != null)
        {
            targetType = nullableType;
        }

        if (targetType.IsAssignableFrom(value.GetType()))
        {
            return value;
        }

        if (targetType.IsEnum)
        {
            if (value is string stringValue)
            {
                return Enum.Parse(targetType, stringValue, true);
            }
            return Enum.ToObject(targetType, value);
        }

        if (targetType == typeof(Guid))
        {
            return Guid.Parse(value.ToString());
        }

        if (value is IConvertible)
        {
            return System.Convert.ChangeType(value, targetType);
        }

        return null;
    }
}
