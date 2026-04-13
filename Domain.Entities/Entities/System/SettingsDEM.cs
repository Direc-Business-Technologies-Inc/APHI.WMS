using Ardalis.GuardClauses;
using Domain.Commons;

namespace Domain.Entities.Entities.System;

public class SettingsDEM : EntityDEM
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }

    protected SettingsDEM() { }

    protected SettingsDEM(string name, string descritpion, string type, string value)
    {
        Name = Guard.Against.NullOrEmpty(name);
        Description = descritpion;
        Type = Guard.Against.NullOrEmpty(type);
        Value = Guard.Against.NullOrEmpty(value);
    }

    public SettingsDEM Create(string name, string descritpion, string type, string value)
    {
        return new SettingsDEM(name, descritpion, type, value);
    }

    public SettingsDEM Update(string name, string descritpion, string type, string value)
    {
        Name = Guard.Against.NullOrEmpty(name);
        Description = descritpion;
        Type = Guard.Against.NullOrEmpty(type);
        Value = Guard.Against.NullOrEmpty(value);

        return this;
    }

}
