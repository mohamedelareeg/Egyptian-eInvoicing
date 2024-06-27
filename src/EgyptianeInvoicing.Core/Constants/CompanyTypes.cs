using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Constants
{
    public class CompanyTypes
    {
        public static readonly CompanyTypes BusinessInEgypt = new CompanyTypes("B", "شركة");
        public static readonly CompanyTypes NaturalPerson = new CompanyTypes("P", "أجنبي");
        public static readonly CompanyTypes Foreigner = new CompanyTypes("F", "فرد");

        public string Code { get; }
        public string ArabicName { get; }


        private CompanyTypes(string code, string arabicName)
        {
            Code = code;
            ArabicName = arabicName;
        }

        public static IEnumerable<CompanyTypes> List()
        {
            return new[] { BusinessInEgypt, NaturalPerson, Foreigner };
        }

        public static CompanyTypes FromCode(string code)
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

        public static CompanyTypes FromName(string name)
        {
            foreach (var type in List())
            {
                if (type.ArabicName == name)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
