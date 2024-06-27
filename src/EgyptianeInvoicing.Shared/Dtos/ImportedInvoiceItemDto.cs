namespace EgyptianeInvoicing.Shared.Dtos
{
    public class ImportedInvoiceItemDto
    {
        public string ProductName { get; set; } // اسم الصنف
        public string CodeType { get; set; } = "GS1"; // طريقة التكويد
        public string? InternationalProductCode { get; set; } // كود الصنف
        public string? InternalProductCode { get; set; } // الكود الداخلى
        public string Unit { get; set; } = "كل"; // وحدة الصنف
        public double Quantity { get; set; } = 1; // الكمية المباعة
        public double UnitPrice { get; set; } // سعر الوحدة بالجنية
        public double UnitDiscount { get; set; } = 0; // اجمالى الخصم قبل الضريبة
        public string Currency { get; set; } = "جنيه مصري"; // العملة
        public double? CurrencyConvert { get; set; } // سعر الوحدة بالعملة الاجنبية
        public string? VATCode { get; set; }                // كود ضريبة القيمة المضافة
        public decimal VATPercentage { get; set; } = 0;         // نسبة ضريبة القيمة المضافة
        public decimal RelativeTableTaxPercentage { get; set; } = 0; // نسبة ضريبة الجدول النسبية
        public decimal SpecificTableTaxPercentage { get; set; } = 0; // نسبة ضريبة الجدول النوعية
        public string? DiscountTaxCode { get; set; }        // كود الخصم تحت حساب الضريبة
        public decimal DiscountTaxPercentage { get; set; } = 0; // نسبة الخصم تحت حساب الضريبة
    }
}
