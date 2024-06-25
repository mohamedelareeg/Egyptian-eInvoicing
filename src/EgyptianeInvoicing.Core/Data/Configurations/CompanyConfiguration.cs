using EgyptianeInvoicing.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgyptianeInvoicing.Core.Data.Configurations
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(TableNames.Companies);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(255);
            builder.Property(x => x.TaxNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CommercialRegistrationNo).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ImagePath).HasMaxLength(255);

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(a => a.BranchId).HasColumnName("BranchId");
                address.Property(a => a.Country).HasColumnName("Country").IsRequired().HasMaxLength(100);
                address.Property(a => a.Governorate).HasColumnName("Governorate").HasMaxLength(100);
                address.Property(a => a.RegionCity).HasColumnName("RegionCity").HasMaxLength(100);
                address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(255);
                address.Property(a => a.BuildingNumber).HasColumnName("BuildingNumber").HasMaxLength(20);
                address.Property(a => a.PostalCode).HasColumnName("PostalCode").HasMaxLength(20);
                address.Property(a => a.Floor).HasColumnName("Floor").HasMaxLength(10);
                address.Property(a => a.Room).HasColumnName("Room").HasMaxLength(10);
                address.Property(a => a.Landmark).HasColumnName("Landmark").HasMaxLength(255);
                address.Property(a => a.AdditionalInformation).HasColumnName("AdditionalInformation").HasMaxLength(500);
            });

            builder.Property(x => x.Type).IsRequired().HasConversion<string>();

            builder.OwnsOne(x => x.Credentials, credentials =>
            {
                credentials.Property(c => c.ClientId).HasColumnName("ClientId").IsRequired().HasMaxLength(100);
                credentials.Property(c => c.ClientSecret1).HasColumnName("ClientSecret1").IsRequired().HasMaxLength(100);
                credentials.Property(c => c.ClientSecret2).HasColumnName("ClientSecret2").IsRequired().HasMaxLength(100);
                credentials.Property(c => c.TokenPin).HasColumnName("TokenPin").IsRequired().HasMaxLength(100);
                credentials.Property(c => c.Certificate).HasColumnName("Certificate").HasMaxLength(100);
            });
            builder.OwnsMany(x => x.Payments, payments =>
            {
                payments.ToTable("CompanyPayments");
                payments.Property(p => p.BankName).HasColumnName("BankName").HasMaxLength(100);
                payments.Property(p => p.BankAddress).HasColumnName("BankAddress").HasMaxLength(255);
                payments.Property(p => p.BankAccountNo).HasColumnName("BankAccountNo").HasMaxLength(50);
                payments.Property(p => p.BankAccountIBAN).HasColumnName("BankAccountIBAN").HasMaxLength(100);
                payments.Property(p => p.SwiftCode).HasColumnName("SwiftCode").HasMaxLength(50);
                payments.Property(p => p.Terms).HasColumnName("Terms").HasMaxLength(500);
            });
        }
    }
}
