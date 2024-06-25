using System;
using System.Collections.Generic;

namespace EgyptianeInvoicing.Core.Constants
{
    public static class NonTaxableTypes
    {
        public static readonly List<TaxType> Codes = new List<TaxType>
        {
            new TaxType("T13", "Stamping tax (percentage)", "ضريبه الدمغه (نسبيه)"),
            new TaxType("T14", "Stamping Tax (amount)", "ضريبه الدمغه (قطعيه بمقدار ثابت)"),
            new TaxType("T15", "Entertainment tax", "ضريبة الملاهى"),
            new TaxType("T16", "Resource development fee", "رسم تنميه الموارد"),
            new TaxType("T17", "Service charges", "رسم خدمة"),
            new TaxType("T18", "Municipality Fees", "رسم المحليات"),
            new TaxType("T19", "Medical insurance fee", "رسم التامين الصحى"),
            new TaxType("T20", "Other fees", "رسوم أخرى")
        };
        public static TaxType GetByCode(string code)
        {
            return Codes.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }

        public static TaxType GetByEnglishDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.EnglishDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }

        public static TaxType GetByArabicDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.ArabicDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }
    }
    public static class TaxSubtypes
    {
        public static readonly List<TaxSubtype> Codes = new List<TaxSubtype>
        {
            new TaxSubtype("V001", "Export", "تصدير للخارج", "T1"),
            new TaxSubtype("V002", "Export to free areas and other areas", "تصدير مناطق حرة وأخرى", "T1"),
            new TaxSubtype("V003", "Exempted good or service", "سلعة أو خدمة معفاة", "T1"),
            new TaxSubtype("V004", "A non-taxable good or service", "سلعة أو خدمة غير خاضعة للضريبة", "T1"),
            new TaxSubtype("V005", "Exemptions for diplomats, consulates and embassies.", "إعفاءات دبلوماسين والقنصليات والسفارات", "T1"),
            new TaxSubtype("V006", "Defence and National security Exemptions", "إعفاءات الدفاع والأمن القومى", "T1"),
            new TaxSubtype("V007", "Agreements exemptions", "إعفاءات اتفاقيات", "T1"),
            new TaxSubtype("V008", "Special Exemptios and other reasons", "إعفاءات خاصة و أخرى", "T1"),
            new TaxSubtype("V009", "General Item sales", "سلع عامة", "T1"),
            new TaxSubtype("V010", "Other Rates", "نسب ضريبة أخرى", "T1"),
            new TaxSubtype("Tbl01", "Table tax (percentage)", "ضريبه الجدول (نسبيه)", "T2"),
            new TaxSubtype("Tbl02", "Table tax (Fixed Amount)", "ضريبه الجدول (النوعية)", "T3"),
            new TaxSubtype("W001", "Contracting", "المقاولات", "T4"),
            new TaxSubtype("W002", "Supplies", "التوريدات", "T4"),
            new TaxSubtype("W003", "Purachases", "المشتريات", "T4"),
            new TaxSubtype("W004", "Services", "الخدمات", "T4"),
            new TaxSubtype("W005", "Sums paid by the cooperative societies for car transportation to their members", "المبالغ التي تدفعها الجميعات التعاونية للنقل بالسيارات لاعضائها", "T4"),
            new TaxSubtype("W006", "Commission agency & brokerage", "الوكالة بالعمولة والسمسرة", "T4"),
            new TaxSubtype("W007", "Discounts & grants & additional exceptional incentives granted by smoke & cement companies", "الخصومات والمنح والحوافز الاستثنائية ةالاضافية التي تمنحها شركات الدخان والاسمنت", "T4"),
            new TaxSubtype("W008", "All discounts & grants & commissions granted by petroleum & telecommunications & other companies", "جميع الخصومات والمنح والعمولات التي تمنحها شركات البترول والاتصالات … وغيرها من الشركات المخاطبة بنظام الخصم", "T4"),
            new TaxSubtype("W009", "Supporting export subsidies", "مساندة دعم الصادرات التي يمنحها صندوق تنمية الصادرات", "T4"),
            new TaxSubtype("W010", "Professional fees", "اتعاب مهنية", "T4"),
            new TaxSubtype("W011", "Commission & brokerage _A_57", "العمولة والسمسرة _م_57", "T4"),
            new TaxSubtype("W012", "Hospitals collecting from doctors", "تحصيل المستشفيات من الاطباء", "T4"),
            new TaxSubtype("W013", "Royalties", "الاتاوات", "T4"),
            new TaxSubtype("W014", "Customs clearance", "تخليص جمركي", "T4"),
            new TaxSubtype("W015", "Exemption", "أعفاء", "T4"),
            new TaxSubtype("W016", "Advance payment", "دفعات مقدمه", "T4"),
            new TaxSubtype("ST01", "Stamping tax (percentage)", "ضريبه الدمغه (نسبيه)", "T5"),
            new TaxSubtype("ST02", "Stamping Tax (amount)", "ضريبه الدمغه (قطعيه بمقدار ثابت)", "T6"),
            new TaxSubtype("Ent01", "Entertainment tax (rate)", "ضريبة الملاهى (نسبة)", "T7"),
            new TaxSubtype("Ent02", "Entertainment tax (amount)", "ضريبة الملاهى (قطعية)", "T7"),
            new TaxSubtype("RD01", "Resource development fee (rate)", "رسم تنميه الموارد (نسبة)", "T8"),
            new TaxSubtype("RD02", "Resource development fee (amount)", "رسم تنميه الموارد (قطعية)", "T8"),
            new TaxSubtype("SC01", "Service charges (rate)", "رسم خدمة (نسبة)", "T9"),
            new TaxSubtype("SC02", "Service charges (amount)", "رسم خدمة (قطعية)", "T9"),
            new TaxSubtype("Mn01", "Municipality Fees (rate)", "رسم المحليات (نسبة)", "T10"),
            new TaxSubtype("Mn02", "Municipality Fees (amount)", "رسم المحليات (قطعية)", "T10"),
            new TaxSubtype("MI01", "Medical insurance fee (rate)", "رسم التامين الصحى (نسبة)", "T11"),
            new TaxSubtype("MI02", "Medical insurance fee (amount)", "رسم التامين الصحى (قطعية)", "T11"),
            new TaxSubtype("OF01", "Other fees (rate)", "رسوم أخرى (نسبة)", "T12"),
            new TaxSubtype("OF02", "Other fees (amount)", "رسوم أخرى (قطعية)", "T12"),
            new TaxSubtype("ST03", "Stamping tax (percentage)", "ضريبه الدمغه (نسبيه)", "T13"),
            new TaxSubtype("ST04", "Stamping Tax (amount)", "ضريبه الدمغه (قطعيه بمقدار ثابت)", "T14"),
            new TaxSubtype("Ent03", "Entertainment tax (rate)", "ضريبة الملاهى (نسبة)", "T15"),
            new TaxSubtype("Ent04", "Entertainment tax (amount)", "ضريبة الملاهى (قطعية)", "T15"),
            new TaxSubtype("RD03", "Resource development fee (rate)", "رسم تنميه الموارد (نسبة)", "T16"),
            new TaxSubtype("RD04", "Resource development fee (amount)", "رسم تنميه الموارد (قطعية)", "T16"),
            new TaxSubtype("SC03", "Service charges (rate)", "رسم خدمة (نسبة)", "T17"),
            new TaxSubtype("SC04", "Service charges (amount)", "رسم خدمة (قطعية)", "T17"),
            new TaxSubtype("Mn03", "Municipality Fees (rate)", "رسم المحليات (نسبة)", "T18"),
            new TaxSubtype("Mn04", "Municipality Fees (amount)", "رسم المحليات (قطعية)", "T18"),
            new TaxSubtype("MI03", "Medical insurance fee (rate)", "رسم التامين الصحى (نسبة)", "T19"),
            new TaxSubtype("MI04", "Medical insurance fee (amount)", "رسم التامين الصحى (قطعية)", "T19"),
            new TaxSubtype("OF03", "Other fees (rate)", "رسوم أخرى (نسبة)", "T20"),
            new TaxSubtype("OF04", "Other fees (amount)", "رسوم أخرى (قطعية)", "T20")
        };
        public static TaxSubtype GetByCode(string code)
        {
            return Codes.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }

        public static TaxSubtype GetByEnglishDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.EnglishDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }

        public static TaxSubtype GetByArabicDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.ArabicDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }
        public static List<TaxSubtype> GetByTaxTypeReference(string taxTypeReference)
        {
            return Codes.Where(t => t.TaxTypeReference.Equals(taxTypeReference, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }
    public static class TaxableTypes
    {
        public static readonly List<TaxType> Codes = new List<TaxType>
        {
            new TaxType("T1", "Value added tax", "ضريبه القيمه المضافه"),
            new TaxType("T2", "Table tax (percentage)", "ضريبه الجدول (نسبيه)"),
            new TaxType("T3", "Table tax (Fixed Amount)", "ضريبه الجدول (النوعية)"),
            new TaxType("T4", "Withholding tax (WHT)", "الخصم تحت حساب الضريبه"),
            new TaxType("T5", "Stamping tax (percentage)", "ضريبه الدمغه (نسبيه)"),
            new TaxType("T6", "Stamping Tax (amount)", "ضريبه الدمغه (قطعيه بمقدار ثابت)"),
            new TaxType("T7", "Entertainment tax", "ضريبة الملاهى"),
            new TaxType("T8", "Resource development fee", "رسم تنميه الموارد"),
            new TaxType("T9", "Service charges", "رسم خدمة"),
            new TaxType("T10", "Municipality Fees", "رسم المحليات"),
            new TaxType("T11", "Medical insurance fee", "رسم التامين الصحى"),
            new TaxType("T12", "Other fees", "رسوم أخرى")
        };
        public static TaxType GetByCode(string code)
        {
            return Codes.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }

        public static TaxType GetByEnglishDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.EnglishDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }

        public static TaxType GetByArabicDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.ArabicDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public class TaxType
    {
        public string Code { get; }
        public string EnglishDescription { get; }
        public string ArabicDescription { get; }

        public TaxType(string code, string englishDescription, string arabicDescription)
        {
            Code = code;
            EnglishDescription = englishDescription;
            ArabicDescription = arabicDescription;
        }
    }

    public class TaxSubtype
    {
        public string Code { get; }
        public string EnglishDescription { get; }
        public string ArabicDescription { get; }
        public string TaxTypeReference { get; }

        public TaxSubtype(string code, string englishDescription, string arabicDescription, string taxTypeReference)
        {
            Code = code;
            EnglishDescription = englishDescription;
            ArabicDescription = arabicDescription;
            TaxTypeReference = taxTypeReference;
        }
    }
}
