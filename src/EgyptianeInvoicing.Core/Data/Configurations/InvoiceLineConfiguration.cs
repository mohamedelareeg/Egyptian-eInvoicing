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
    internal class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.ToTable(TableNames.InvoiceLines);

            builder.HasKey(x => x.Id);

            builder.HasOne<Invoice>()
                    .WithMany(i => i.InvoiceLines)
                    .HasForeignKey(x => x.InvoiceId)
                    .IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);
            builder.Property(x => x.InternationalCode).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UnitType).IsRequired().HasConversion<string>();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.InternalCode).HasMaxLength(50);

            builder.OwnsOne(x => x.UnitValue, uv =>
            {
                uv.Property(u => u.CurrencySold).HasColumnName("UnitValueCurrencySold").HasMaxLength(3).IsRequired();
                uv.Property(u => u.AmountSold).HasColumnName("UnitValueAmountSold").HasColumnType("decimal(18,2)").IsRequired();
                uv.Property(u => u.Amount).HasColumnName("UnitValueAmount").HasColumnType("decimal(18,2)");
                uv.Property(u => u.CurrencyExchangeRate).HasColumnName("UnitValueCurrencyExchangeRate");
            });

            builder.OwnsOne(x => x.Discount, d =>
            {
                d.Property(dd => dd.Rate).HasColumnName("DiscountRate").IsRequired();
                d.Property(dd => dd.Amount).HasColumnName("DiscountAmount").HasColumnType("decimal(18,2)");
            });

            builder.OwnsMany<TaxableItem>("TaxableItems", ti =>
            {
                ti.ToTable("InvoiceLineTaxableItems");
                ti.Property<string>("ParentCode").IsRequired();
                ti.Property<string>("Code").IsRequired();
                ti.Property<double>("Rate");
                ti.Property<double>("Amount").HasColumnType("decimal(18,2)");

            });
        }
    }
}
