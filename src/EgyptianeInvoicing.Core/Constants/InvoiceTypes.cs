using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Constants
{
    public class InvoiceType
    {
        public static readonly InvoiceType Invoice = new InvoiceType("I", "فاتورة", "Invoice");
        public static readonly InvoiceType CreditNote = new InvoiceType("C", "اشعار دائن", "Credit Note");
        public static readonly InvoiceType DebitNote = new InvoiceType("D", "اشعار مدين", "Debit Note");

        public string Code { get; }
        public string ArabicName { get; }
        public string EnglishName { get; }

        private InvoiceType(string code, string arabicName, string englishName)
        {
            Code = code;
            ArabicName = arabicName;
            EnglishName = englishName;
        }

        public static IEnumerable<InvoiceType> List()
        {
            return new[] { Invoice, CreditNote, DebitNote };
        }

        public static InvoiceType FromCode(string code)
        {
            foreach (var type in List())
            {
                if (type.Code == code)
                {
                    return type;
                }
            }
            return null;
        }

        public static InvoiceType FromName(string name)
        {
            foreach (var type in List())
            {
                if (type.EnglishName == name || type.ArabicName == name)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
