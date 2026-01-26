using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.Campaign.Api.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Campaign.Api.Data.Configuration;

[ExcludeFromCodeCoverage]
internal class UserDataEntityConfiguration : IEntityTypeConfiguration<UserDataEntity>
{
    public void Configure(EntityTypeBuilder<UserDataEntity> builder)
    {
        builder.ToTable("UserData");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int");
        builder.Property(x => x.FirstName).HasColumnName("FirstName").HasColumnType("varchar(250)").IsRequired();
        builder.Property(x => x.LastName).HasColumnName("LastName").HasColumnType("varchar(250)").IsRequired();
        builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar(250)").IsRequired();
        builder.Property(x => x.UkEmployerSize).HasColumnName("UkEmployerSize").HasColumnType("int").IsRequired();
        builder.Property(x => x.PrimaryIndustry).HasColumnName("PrimaryIndustry").HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.PrimaryLocation).HasColumnName("PrimaryLocation").HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.AppsgovSignUpDate).HasColumnName("AppsgovSignUpDate").HasColumnType("DateTime").IsRequired();
        builder.Property(x => x.PersonOrigin).HasColumnName("PersonOrigin").HasColumnType("varchar(50)");
        builder.Property(x => x.IncludeInUR).HasColumnName("IncludeInUR").HasColumnType("bit").IsRequired().HasDefaultValue(0);
    }
}