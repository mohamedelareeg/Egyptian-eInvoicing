using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyptianeInvoicing.Core.Constants
{
    public static class PaymentMethods
    {
        public static readonly List<PaymentMethod> Codes = new List<PaymentMethod>
        {
            new PaymentMethod("C", "Cash", "نقداً"),
            new PaymentMethod("V", "Visa", "فيزا"),
            new PaymentMethod("CC", "Cash with contractor", "نقداً مع المقاول"),
            new PaymentMethod("VC", "Visa with contractor", "فيزا مع المقاول"),
            new PaymentMethod("VO", "Vouchers", "قسائم"),
            new PaymentMethod("PR", "Promotion", "ترويج"),
            new PaymentMethod("GC", "Gift Card", "بطاقة هدايا"),
            new PaymentMethod("P", "Points", "نقاط"),
            new PaymentMethod("O", "Others", "أخرى")
        };

        public static IEnumerable<PaymentMethod> GetAll()
        {
            return Codes;
        }

        public static PaymentMethod GetByCode(string code)
        {
            return Codes.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }

        public static PaymentMethod GetByEnglishDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.EnglishDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }

        public static PaymentMethod GetByArabicDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.ArabicDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }
    }
    public class PaymentMethod
    {
        public string Code { get; }
        public string EnglishDescription { get; }
        public string ArabicDescription { get; }

        public PaymentMethod(string code, string englishDescription, string arabicDescription)
        {
            Code = code;
            EnglishDescription = englishDescription;
            ArabicDescription = arabicDescription;
        }
    }

}
