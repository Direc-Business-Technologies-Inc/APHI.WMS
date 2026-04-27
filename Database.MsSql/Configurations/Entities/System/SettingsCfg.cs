using Domain.Entities.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.MsSql.Configurations.Entities.System;

public class SettingsCfg : IEntityTypeConfiguration<SettingsDEM>
{
    public void Configure(EntityTypeBuilder<SettingsDEM> builder)
    {
        builder.ToTable("OSTN");
        builder.Property(u => u.Id).IsRequired();
        builder.HasKey(u => u.Id).HasName("PK_OSTN");

        builder.HasData(
            CreateSeed(
                new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                "Inventory Counting Posting Cycle",
                "Controls which cycle type is allowed to post inventory counting documents. " +
                "Valid values: Daily, Weekly, Monthly, Quarterly. " +
                "Set to None to hide the Post button for all documents.",
                "STRING",
                "None"
            ),
            CreateSeed(
                new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"),
                "Max Failed Login Attempts",
                "Number of consecutive failed login attempts before the user account is locked. " +
                "Must be a positive integer. Defaults to 5 if not configured.",
                "INT",
                "5"
            )
        );
    }

    private static SettingsDEM CreateSeed(Guid id, string name, string description, string type, string value)
    {
        var shell = (SettingsDEM)Activator.CreateInstance(typeof(SettingsDEM), nonPublic: true)!;
        var seed = shell.Create(name, description, type, value);
        seed.SetEntityId(id);
        return seed;
    }
}
