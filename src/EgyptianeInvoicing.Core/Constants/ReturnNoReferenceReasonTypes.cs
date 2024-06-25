using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Constants
{
    public static class ReturnNoReferenceReasonTypes
    {
        public static readonly List<ReturnNoReferenceReasonType> Codes = new List<ReturnNoReferenceReasonType>
        {
            new ReturnNoReferenceReasonType("I", "International return receipt", "ايصال عودة دولى"),
            new ReturnNoReferenceReasonType("B", "Sales issued before adoption", "المبيعات الصادرة قبل التبنى")
        };

        public static IEnumerable<ReturnNoReferenceReasonType> GetAll()
        {
            return Codes;
        }

        public static ReturnNoReferenceReasonType GetByCode(string code)
        {
            return Codes.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }

        public static ReturnNoReferenceReasonType GetByEnglishDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.EnglishDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }

        public static ReturnNoReferenceReasonType GetByArabicDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.ArabicDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public class ReturnNoReferenceReasonType
    {
        public string Code { get; }
        public string EnglishDescription { get; }
        public string ArabicDescription { get; }

        public ReturnNoReferenceReasonType(string code, string englishDescription, string arabicDescription)
        {
            Code = code;
            EnglishDescription = englishDescription;
            ArabicDescription = arabicDescription;
        }
    }

}
