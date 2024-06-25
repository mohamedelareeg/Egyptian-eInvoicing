using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Constants
{
    public static class OrderDeliveryModes
    {
        public static readonly List<OrderDeliveryMode> Codes = new List<OrderDeliveryMode>
        {
            new OrderDeliveryMode("FC", "From the company place", "من مكان الشركة"),
            new OrderDeliveryMode("TO", "Transport by others", "نقل بواسطة الآخرين"),
            new OrderDeliveryMode("TC", "Transported by the company", "نقل بواسطة الشركة")
        };

        public static IEnumerable<OrderDeliveryMode> GetAll()
        {
            return Codes;
        }

        public static OrderDeliveryMode GetByCode(string code)
        {
            return Codes.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }

        public static OrderDeliveryMode GetByEnglishDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.EnglishDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }

        public static OrderDeliveryMode GetByArabicDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.ArabicDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public class OrderDeliveryMode
    {
        public string Code { get; }
        public string EnglishDescription { get; }
        public string ArabicDescription { get; }

        public OrderDeliveryMode(string code, string englishDescription, string arabicDescription)
        {
            Code = code;
            EnglishDescription = englishDescription;
            ArabicDescription = arabicDescription;
        }
    }

}
