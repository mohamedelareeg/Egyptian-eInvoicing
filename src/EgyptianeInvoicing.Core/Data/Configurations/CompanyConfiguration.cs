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
                address.Property(a => a.BranchId).HasColumnName("AddressBranchId");
                address.Property(a => a.Country).HasColumnName("AddressCountry").IsRequired().HasMaxLength(100);
                address.Property(a => a.Governorate).HasColumnName("AddressGovernorate").HasMaxLength(100);
                address.Property(a => a.RegionCity).HasColumnName("AddressRegionCity").HasMaxLength(100);
                address.Property(a => a.Street).HasColumnName("AddressStreet").HasMaxLength(255);
                address.Property(a => a.BuildingNumber).HasColumnName("AddressBuildingNumber").HasMaxLength(20);
                address.Property(a => a.PostalCode).HasColumnName("AddressPostalCode").HasMaxLength(20);
                address.Property(a => a.Floor).HasColumnName("AddressFloor").HasMaxLength(10);
                address.Property(a => a.Room).HasColumnName("AddressRoom").HasMaxLength(10);
                address.Property(a => a.Landmark).HasColumnName("AddressLandmark").HasMaxLength(255);
                address.Property(a => a.AdditionalInformation).HasColumnName("AddressAdditionalInformation").HasMaxLength(500);
            });

            builder.Property(x => x.Type).IsRequired().HasConversion<string>();

            builder.OwnsOne(x => x.Credentials, credentials =>
            {
                credentials.Property(c => c.ClientId).HasColumnName("CredentialsClientId").IsRequired().HasMaxLength(100);
                credentials.Property(c => c.ClientSecret1).HasColumnName("CredentialsClientSecret1").IsRequired().HasMaxLength(100);
                credentials.Property(c => c.ClientSecret2).HasColumnName("CredentialsClientSecret2").IsRequired().HasMaxLength(100);
                credentials.Property(c => c.TokenPin).HasColumnName("CredentialsTokenPin").IsRequired().HasMaxLength(100);
                credentials.Property(c => c.Certificate).HasColumnName("CredentialsCertificate").HasMaxLength(100);
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
