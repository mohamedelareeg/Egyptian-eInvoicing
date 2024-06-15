using System.Collections.Generic;

namespace EgyptianeInvoicing.Shared.Dtos.CodesDto
{
    public class TaxatableType
    {
        public string Code { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }

        public static List<TaxatableType> TaxTypes { get; } = new List<TaxatableType>
        {
            new TaxatableType { Code = "T1", DescriptionEn = "Value added tax", DescriptionAr = "ضريبه القيمه المضافه" },
            new TaxatableType { Code = "T2", DescriptionEn = "Table tax (percentage)", DescriptionAr = "ضريبه الجدول (نسبيه)" },
            new TaxatableType { Code = "T3", DescriptionEn = "Table tax (Fixed Amount)", DescriptionAr = "ضريبه الجدول (قطعيه)" },
            new TaxatableType { Code = "T4", DescriptionEn = "Withholding tax (WHT)", DescriptionAr = "الخصم تحت حساب الضريبه" },
            new TaxatableType { Code = "T5", DescriptionEn = "Stamping tax (percentage)", DescriptionAr = "ضريبه الدمغه (نسبيه)" },
            new TaxatableType { Code = "T6", DescriptionEn = "Stamping Tax (amount)", DescriptionAr = "ضريبه الدمغه (قطعيه بمقدار ثابت )" },
            new TaxatableType { Code = "T7", DescriptionEn = "Entertainment tax", DescriptionAr = "ضريبة الملاهى" },
            new TaxatableType { Code = "T8", DescriptionEn = "Resource development fee", DescriptionAr = "رسم تنميه الموارد" },
            new TaxatableType { Code = "T9", DescriptionEn = "Table tax (percentage)", DescriptionAr = "رسم خدمة" },
            new TaxatableType { Code = "T10", DescriptionEn = "Municipality Fees", DescriptionAr = "رسم المحليات" },
            new TaxatableType { Code = "T11", DescriptionEn = "Medical insurance fee", DescriptionAr = "رسم التامين الصحى" },
            new TaxatableType { Code = "T12", DescriptionEn = "Other fees", DescriptionAr = "رسوم أخري" }
        };
    }
}
