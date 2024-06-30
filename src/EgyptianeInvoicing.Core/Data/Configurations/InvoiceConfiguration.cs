using EgyptianeInvoicing.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EgyptianeInvoicing.Core.ValueObjects;

namespace EgyptianeInvoicing.Core.Data.Configurations
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable(TableNames.Invoices);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IssuerId).IsRequired();
            builder.Property(x => x.ReceiverId).IsRequired();
            builder.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.EinvoiceId).HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired().HasConversion<string>();
            builder.Property(x => x.DocumentType).IsRequired().HasConversion<string>();
            builder.Property(x => x.Currency).IsRequired().HasConversion<string>();
            builder.Property(x => x.ExtraDiscountAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalDiscountAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalSalesAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.NetAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalItemsDiscountAmount).HasColumnType("decimal(18,2)");
            //builder.Ignore(x => x.TotalDiscountAmount);
            //builder.Ignore(x => x.TotalSalesAmount);
            //builder.Ignore(x => x.NetAmount);
            //builder.Ignore(x => x.TotalAmount);
            //builder.Ignore(x => x.TotalItemsDiscountAmount);

            builder.OwnsOne(x => x.Payment, payment =>
            {
                payment.Property(p => p.BankName).HasColumnName("PaymentBankName").HasMaxLength(100);
                payment.Property(p => p.BankAddress).HasColumnName("PaymentBankAddress").HasMaxLength(255);
                payment.Property(p => p.BankAccountNo).HasColumnName("PaymentBankAccountNo").HasMaxLength(50);
                payment.Property(p => p.BankAccountIBAN).HasColumnName("PaymentBankAccountIBAN").HasMaxLength(100);
                payment.Property(p => p.SwiftCode).HasColumnName("PaymentSwiftCode").HasMaxLength(50);
                payment.Property(p => p.Terms).HasColumnName("PaymentPaymentTerms").HasMaxLength(500);
            });

            builder.OwnsOne(x => x.Delivery, delivery =>
            {
                delivery.Property(d => d.Approach).HasColumnName("DeliveryApproach").HasMaxLength(100);
                delivery.Property(d => d.Packaging).HasColumnName("DeliveryPackaging").HasMaxLength(255);
                delivery.Property(d => d.DateValidity).HasColumnName("DeliveryDateValidity");
                delivery.Property(d => d.ExportPort).HasColumnName("DeliveryExportPort").HasMaxLength(100);
                delivery.Property(d => d.GrossWeight).HasColumnName("DeliveryGrossWeight").HasColumnType("decimal(18,2)");
                delivery.Property(d => d.NetWeight).HasColumnName("DeliveryNetWeight").HasColumnType("decimal(18,2)");
                delivery.Property(d => d.Terms).HasColumnName("DeliveryTerms").HasMaxLength(500);
            });
            builder.OwnsMany<TaxTotal>("TaxTotals", tt =>
            {
                tt.ToTable("InvoiceTaxTotals");
                tt.Property<string>("Code").IsRequired();
                tt.Property<double>("Amount").HasColumnType("decimal(18,2)").IsRequired();
            });

            builder.HasOne(x => x.Issuer)
              .WithMany()
              .HasForeignKey(x => x.IssuerId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Receiver)
                .WithMany()
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
