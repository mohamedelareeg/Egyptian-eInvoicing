using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyptianeInvoicing.Core.Constants
{
    public static class WeightUnitTypes
    {
        public static readonly List<WeightUnitType> Codes = new List<WeightUnitType>
        {
            new WeightUnitType("GRM", "Gram ( g )", "جرام"),
            new WeightUnitType("KGM", "Kilogram ( KG )", "كيلوجرام"),
            new WeightUnitType("LB", "pounds", "جنيه"),
            new WeightUnitType("MGM", "Milligram ( mg )", "مليجرام"),
            new WeightUnitType("ONZ", "Ounce ( oz )", "أونصة"),
            new WeightUnitType("ST", "Ton (short,2000 lb)", "طن (قصير، 2000 رطل)"),
            new WeightUnitType("TNE", "Tonne ( t )", "طن"),
            new WeightUnitType("TON", "Ton (metric)", "طن متري"),
            new WeightUnitType("KG", "Keg", "برميل صغير")
        };

        public static IEnumerable<WeightUnitType> GetAll()
        {
            return Codes;
        }

        public static WeightUnitType GetByCode(string code)
        {
            return Codes.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }

        public static WeightUnitType GetByEnglishDescription(string description)
        {
            return Codes.FirstOrDefault(c => c.EnglishDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }

        public static WeightUnitType GetByArabicDescription(string description)
        {
            return Codes.FirstOrDefault(c => c.ArabicDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }
    }
    public class WeightUnitType
    {
        public string Code { get; }
        public string EnglishDescription { get; }
        public string ArabicDescription { get; }

        public WeightUnitType(string code, string englishDescription, string arabicDescription)
        {
            Code = code;
            EnglishDescription = englishDescription;
            ArabicDescription = arabicDescription;
        }
    }

}
