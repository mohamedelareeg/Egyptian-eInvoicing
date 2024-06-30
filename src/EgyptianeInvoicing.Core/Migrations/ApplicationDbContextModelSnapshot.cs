﻿// <auto-generated />
using System;
using EgyptianeInvoicing.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EgyptianeInvoicing.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EgyptianeInvoicing.Core.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActivityCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<string>("CommercialRegistrationNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("EInvoiceToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TaxNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("EgyptianeInvoicing.Core.Models.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EinvoiceId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("ExtraDiscountAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("IssuerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("PurchaseOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SalesOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalDiscountAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalItemsDiscountAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalSalesAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IssuerId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("Invoices", (string)null);
                });

            modelBuilder.Entity("EgyptianeInvoicing.Core.Models.InvoiceLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("InternalCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("InternationalCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<double>("NetTotal")
                        .HasColumnType("float");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("SalesTotal")
                        .HasColumnType("float");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<double>("TotalTaxableFees")
                        .HasColumnType("float");

                    b.Property<string>("UnitType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceLines", (string)null);
                });

            modelBuilder.Entity("EgyptianeInvoicing.Core.Models.Company", b =>
                {
                    b.OwnsMany("EgyptianeInvoicing.Core.ValueObjects.Payment", "Payments", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("BankAccountIBAN")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("BankAccountIBAN");

                            b1.Property<string>("BankAccountNo")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("BankAccountNo");

                            b1.Property<string>("BankAddress")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("BankAddress");

                            b1.Property<string>("BankName")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("BankName");

                            b1.Property<string>("SwiftCode")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("SwiftCode");

                            b1.Property<string>("Terms")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("Terms");

                            b1.HasKey("CompanyId", "Id");

                            b1.ToTable("CompanyPayments", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("EgyptianeInvoicing.Core.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AdditionalInformation")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("AddressAdditionalInformation");

                            b1.Property<int>("BranchId")
                                .HasColumnType("int")
                                .HasColumnName("AddressBranchId");

                            b1.Property<string>("BuildingNumber")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("AddressBuildingNumber");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("AddressCountry");

                            b1.Property<string>("Floor")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)")
                                .HasColumnName("AddressFloor");

                            b1.Property<string>("Governorate")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("AddressGovernorate");

                            b1.Property<string>("Landmark")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("AddressLandmark");

                            b1.Property<string>("PostalCode")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("AddressPostalCode");

                            b1.Property<string>("RegionCity")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("AddressRegionCity");

                            b1.Property<string>("Room")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)")
                                .HasColumnName("AddressRoom");

                            b1.Property<string>("Street")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("AddressStreet");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("EgyptianeInvoicing.Core.ValueObjects.ClientCredentials", "Credentials", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Certificate")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("CredentialsCertificate");

                            b1.Property<string>("ClientId")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("CredentialsClientId");

                            b1.Property<string>("ClientSecret1")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("CredentialsClientSecret1");

                            b1.Property<string>("ClientSecret2")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("CredentialsClientSecret2");

                            b1.Property<string>("TokenPin")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("CredentialsTokenPin");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Credentials");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("EgyptianeInvoicing.Core.Models.Invoice", b =>
                {
                    b.HasOne("EgyptianeInvoicing.Core.Models.Company", "Issuer")
                        .WithMany()
                        .HasForeignKey("IssuerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EgyptianeInvoicing.Core.Models.Company", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("EgyptianeInvoicing.Core.ValueObjects.Payment", "Payment", b1 =>
                        {
                            b1.Property<Guid>("InvoiceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("BankAccountIBAN")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("PaymentBankAccountIBAN");

                            b1.Property<string>("BankAccountNo")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("PaymentBankAccountNo");

                            b1.Property<string>("BankAddress")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("PaymentBankAddress");

                            b1.Property<string>("BankName")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("PaymentBankName");

                            b1.Property<string>("SwiftCode")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("PaymentSwiftCode");

                            b1.Property<string>("Terms")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("PaymentPaymentTerms");

                            b1.HasKey("InvoiceId");

                            b1.ToTable("Invoices");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceId");
                        });

                    b.OwnsOne("EgyptianeInvoicing.Core.ValueObjects.Delivery", "Delivery", b1 =>
                        {
                            b1.Property<Guid>("InvoiceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Approach")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("DeliveryApproach");

                            b1.Property<DateTime?>("DateValidity")
                                .HasColumnType("datetime2")
                                .HasColumnName("DeliveryDateValidity");

                            b1.Property<string>("ExportPort")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("DeliveryExportPort");

                            b1.Property<decimal?>("GrossWeight")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("DeliveryGrossWeight");

                            b1.Property<decimal?>("NetWeight")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("DeliveryNetWeight");

                            b1.Property<string>("Packaging")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("DeliveryPackaging");

                            b1.Property<string>("Terms")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("DeliveryTerms");

                            b1.HasKey("InvoiceId");

                            b1.ToTable("Invoices");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceId");
                        });

                    b.OwnsMany("EgyptianeInvoicing.Core.ValueObjects.TaxTotal", "TaxTotals", b1 =>
                        {
                            b1.Property<Guid>("InvoiceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("InvoiceId", "Id");

                            b1.ToTable("InvoiceTaxTotals", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("InvoiceId");
                        });

                    b.Navigation("Delivery");

                    b.Navigation("Issuer");

                    b.Navigation("Payment");

                    b.Navigation("Receiver");

                    b.Navigation("TaxTotals");
                });

            modelBuilder.Entity("EgyptianeInvoicing.Core.Models.InvoiceLine", b =>
                {
                    b.HasOne("EgyptianeInvoicing.Core.Models.Invoice", null)
                        .WithMany("InvoiceLines")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("EgyptianeInvoicing.Core.ValueObjects.Discount", "Discount", b1 =>
                        {
                            b1.Property<Guid>("InvoiceLineId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal?>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("DiscountAmount");

                            b1.Property<double>("Rate")
                                .HasColumnType("float")
                                .HasColumnName("DiscountRate");

                            b1.HasKey("InvoiceLineId");

                            b1.ToTable("InvoiceLines");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceLineId");
                        });

                    b.OwnsMany("EgyptianeInvoicing.Core.ValueObjects.TaxableItem", "TaxableItems", b1 =>
                        {
                            b1.Property<Guid>("InvoiceLineId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ParentCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<double>("Rate")
                                .HasColumnType("float");

                            b1.HasKey("InvoiceLineId", "Id");

                            b1.ToTable("InvoiceLineTaxableItems", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("InvoiceLineId");
                        });

                    b.OwnsOne("EgyptianeInvoicing.Core.ValueObjects.UnitValue", "UnitValue", b1 =>
                        {
                            b1.Property<Guid>("InvoiceLineId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("UnitValueAmount");

                            b1.Property<decimal>("AmountSold")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("UnitValueAmountSold");

                            b1.Property<decimal>("CurrencyExchangeRate")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("UnitValueCurrencyExchangeRate");

                            b1.Property<string>("CurrencySold")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("UnitValueCurrencySold");

                            b1.HasKey("InvoiceLineId");

                            b1.ToTable("InvoiceLines");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceLineId");
                        });

                    b.Navigation("Discount");

                    b.Navigation("TaxableItems");

                    b.Navigation("UnitValue")
                        .IsRequired();
                });

            modelBuilder.Entity("EgyptianeInvoicing.Core.Models.Invoice", b =>
                {
                    b.Navigation("InvoiceLines");
                });
#pragma warning restore 612, 618
        }
    }
}
