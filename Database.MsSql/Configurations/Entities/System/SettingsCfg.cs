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
    }
}
