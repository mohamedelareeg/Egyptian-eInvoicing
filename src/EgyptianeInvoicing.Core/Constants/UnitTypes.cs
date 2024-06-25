using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Constants
{
    public static class UnitTypes
    {
        public static readonly List<UnitType> Codes = new List<UnitType>
        {
       new UnitType("2Z", "Millivolt ( mV )", "ميلي فولت"),
            new UnitType("4K", "Milliampere ( mA )", "ميلي أمبير"),
            new UnitType("4O", "Microfarad ( microF )", "ميكروفاراد"),
            new UnitType("A87", "Gigaohm ( GOhm )", "جيجا أوم"),
            new UnitType("A93", "Gram/Cubic meter ( g/m3 )", "جرام / متر مكعب"),
            new UnitType("A94", "Gram/cubic centimeter ( g/cm3 )", "جرام / سنتيمتر مكعب"),
            new UnitType("AMP", "Ampere ( A )", "أمبير"),
            new UnitType("ANN", "Years ( yr )", "سنوات"),
            new UnitType("B22", "Kiloampere ( kA )", "كيلو أمبير"),
            new UnitType("B49", "Kiloohm ( kOhm )", "كيلو أوم"),
            new UnitType("B75", "Megohm ( MOhm )", "ميجا أوم"),
            new UnitType("B78", "Megavolt ( MV )", "ميجا فولت"),
            new UnitType("B84", "Microampere ( microA )", "ميكرو أمبير"),
            new UnitType("BAR", "bar ( bar )", "بار"),
            new UnitType("BBL", "Barrel (oil 42 gal.)", "برميل (نفط 42 غالون)"),
            new UnitType("BG", "Bag ( Bag )", "حقيبة"),
            new UnitType("BO", "Bottle ( Bt. )", "زجاجة"),
            new UnitType("BOX", "Box", "صندوق"),
            new UnitType("C10", "Millifarad ( mF )", "ميلي فاراد"),
            new UnitType("C39", "Nanoampere ( nA )", "نانو أمبير"),
            new UnitType("C41", "Nanofarad ( nF )", "نانو فاراد"),
            new UnitType("C45", "Nanometer ( nm )", "نانومتر"),
            new UnitType("C62", "Activity unit ( AU )", "وحدة النشاط"),
            new UnitType("CA", "Canister ( Can )", "علبة"),
            new UnitType("CMK", "Square centimeter ( cm2 )", "سنتيمتر مربع"),
            new UnitType("CMQ", "Cubic centimeter ( cm3 )", "سنتيمتر مكعب"),
            new UnitType("CMT", "Centimeter ( cm )", "سنتيمتر"),
            new UnitType("CS", "Case ( Case )", "حالة"),
            new UnitType("CT", "Carton ( Car )", "كرتون"),
            new UnitType("CTL", "Centiliter ( Cl )", "سنتيلتر"),
            new UnitType("D10", "Siemens per meter ( S/m )", "سيمنز / متر"),
            new UnitType("D33", "Tesla ( D )", "تسلا"),
            new UnitType("D41", "Ton/Cubic meter ( t/m3 )", "طن / متر مكعب"),
            new UnitType("DAY", "Days ( d )", "أيام"),
            new UnitType("DMT", "Decimeter ( dm )", "ديسيمتر"),
            new UnitType("DRM", "DRUM", "برميل"),
            new UnitType("EA", "each (ST) ( ST )", "كل"),
            new UnitType("FAR", "Farad ( F )", "فاراد"),
            new UnitType("FOT", "Foot ( Foot )", "قدم"),
            new UnitType("FTK", "Square foot ( ft2 )", "قدم مربع"),
            new UnitType("FTQ", "Cubic foot ( ft3 )", "قدم مكعب"),
            new UnitType("G42", "Microsiemens per centimeter ( microS/cm )", "ميكرو سيمنز / سنتيمتر"),
            new UnitType("GL", "Gram/liter ( g/l )", "جرام / لتر"),
            new UnitType("GLL", "gallon ( gal )", "غالون"),
            new UnitType("GM", "Gram/square meter ( g/m2 )", "جرام / متر مربع"),
            new UnitType("GPT", "Gallon per thousand", "غالون لكل ألف"),
            new UnitType("GRM", "Gram ( g )", "جرام"),
            new UnitType("H63", "Milligram/Square centimeter ( mg/cm2 )", "مليجرام / سنتيمتر مربع"),
            new UnitType("HHP", "Hydraulic Horse Power", "قوة حصان هيدروليكية"),
            new UnitType("HLT", "Hectoliter ( hl )", "هكتولتر"),
            new UnitType("HTZ", "Hertz (1/second) ( Hz )", "هرتز"),
            new UnitType("HUR", "Hours ( hrs )", "ساعات"),
            new UnitType("IE", "Number of Persons ( PRS )", "عدد الأشخاص"),
            new UnitType("INH", "Inch ( “ )", "بوصة"),
            new UnitType("INK", "Square inch ( Inch2 )", "بوصة مربعة"),
            new UnitType("JOB", "JOB", "وظيفة"),
            new UnitType("KGM", "Kilogram ( KG )", "كيلوجرام"),
            new UnitType("KHZ", "Kilohertz ( kHz )", "كيلوهرتز"),
            new UnitType("KMH", "Kilometer/hour ( km/h )", "كيلومتر / ساعة"),
            new UnitType("KMK", "Square kilometer ( km2 )", "كيلومتر مربع"),
            new UnitType("KMQ", "Kilogram/cubic meter ( kg/m3 )", "كيلوجرام / متر مكعب"),
            new UnitType("KMT", "Kilometer ( km )", "كيلومتر"),
            new UnitType("KSM", "Kilogram/Square meter ( kg/m2 )", "كيلوجرام / متر مربع"),
            new UnitType("KVT", "Kilovolt ( kV )", "كيلو فولت"),
            new UnitType("KWT", "Kilowatt ( KW )", "كيلو واط"),
            new UnitType("LB", "pounds", "جنيه"),
            new UnitType("LTR", "Liter ( l )", "لتر"),
            new UnitType("LVL", "Level", "مستوى"),
            new UnitType("M", "Meter ( m )", "متر"),
            new UnitType("MAN", "Man", "رجل"),
            new UnitType("MAW", "Megawatt ( VA )", "ميجاوات"),
            new UnitType("MGM", "Milligram ( mg )", "مليجرام"),
            new UnitType("MHZ", "Megahertz ( MHz )", "ميجاهرتز"),
            new UnitType("MIN", "Minute ( min )", "دقيقة"),
            new UnitType("MMK", "Square millimeter ( mm2 )", "ملليمتر مربع"),
            new UnitType("MMQ", "Cubic millimeter ( mm3 )", "ملليمتر مكعب"),
            new UnitType("MMT", "Millimeter ( mm )", "ملليمتر"),
            new UnitType("MON", "Months ( Months )", "شهور"),
            new UnitType("MTK", "Square meter ( m2 )", "متر مربع"),
            new UnitType("MTQ", "Cubic meter ( m3 )", "متر مكعب"),
            new UnitType("OHM", "Ohm ( Ohm )", "أوم"),
            new UnitType("ONZ", "Ounce ( oz )", "أونصة"),
            new UnitType("PAL", "Pascal ( Pa )", "باسكال"),
            new UnitType("PF", "Pallet ( PAL )", "منصة"),
            new UnitType("PK", "Pack ( PAK )", "حزمة"),
            new UnitType("SK", "Sack", "شيء"),
            new UnitType("SMI", "Mile ( mile )", "ميل"),
            new UnitType("ST", "Ton (short,2000 lb)", "طن (قصير، 2000 رطل)"),
            new UnitType("TNE", "Tonne ( t )", "طن"),
            new UnitType("TON", "Ton (metric)", "طن (متري)"),
            new UnitType("VLT", "Volt ( V )", "فولت"),
            new UnitType("WEE", "Weeks ( Weeks )", "أسابيع"),
            new UnitType("WTT", "Watt ( W )", "واط"),
            new UnitType("X03", "Meter/Hour ( m/h )", "متر / ساعة"),
            new UnitType("YDQ", "Cubic yard ( yd3 )", "ياردة مكعبة"),
            new UnitType("YRD", "Yards ( yd )", "ياردة"),
            new UnitType("NMP", "Number of packs", "عدد الحزم"),
            new UnitType("ST", "Sheet", "ورقة"),
            new UnitType("5I", "Standard cubic foot", "قدم مكعبة قياسية"),
            new UnitType("AE", "Ampere per metre", "أمبير لكل متر"),
            new UnitType("B4", "Barrel, Imperial", "برميل، إمبراطوري"),
            new UnitType("BB", "Base box", "صندوق أساسي"),
            new UnitType("BD", "Board", "لوحة"),
            new UnitType("BE", "Bundle", "حزمة"),
            new UnitType("BK", "Basket", "سلة"),
            new UnitType("BL", "Bale", "تعزية"),
            new UnitType("CH", "Container", "حاوية"),
            new UnitType("CR", "Crate", "حاوية خشبية"),
            new UnitType("DAA", "Decare", "دونم"),
            new UnitType("DTN", "Decitonne", "ديكاتون"),
            new UnitType("DZN", "Dozen", "دستة"),
            new UnitType("FP", "Pound per square foot", "جنيه لكل قدم مربعة"),
            new UnitType("HMT", "Hectometre", "هكتومتر"),
            new UnitType("INQ", "Cubic inch", "بوصة مكعبة"),
            new UnitType("KG", "Keg", "برميل صغير"),
            new UnitType("KTM", "Kilometre", "كيلومتر"),
            new UnitType("LO", "Lot [unit of procurement]", "مجموعة"),
            new UnitType("MLT", "Millilitre", "ملليلتر"),
            new UnitType("MT", "Mat", "بساط"),
            new UnitType("NA", "Milligram per kilogram", "مليجرام لكل كيلوجرام"),
            new UnitType("NAR", "Number of articles", "عدد المواد"),
            new UnitType("NC", "Car", "سيارة"),
            new UnitType("NE", "Net litre", "لتر صافي"),
            new UnitType("NPL", "Number of parcels", "عدد الطرود"),
            new UnitType("NV", "Vehicle", "مركبة"),
            new UnitType("PA", "Packet", "حزمة"),
            new UnitType("PG", "Plate", "صحن"),
            new UnitType("PL", "Pail", "دلو"),
            new UnitType("PR", "Pair", "زوج"),
            new UnitType("PT", "Pint (US)", "بنت (الولايات المتحدة)"),
            new UnitType("RL", "Reel", "بكرة"),
            new UnitType("RO", "Roll", "لفة"),
            new UnitType("SET", "Set", "مجموعة"),
            new UnitType("STK", "Stick, Cigarette", "عصا، سيجارة"),
            new UnitType("T3", "Thousand piece", "ألف قطعة"),
            new UnitType("TC", "Truckload", "حمولة شاحنة"),
            new UnitType("TK", "Tank, rectangular", "خزان، مستطيل"),
            new UnitType("TN", "Tin", "صفيحة"),
            new UnitType("TTS", "Ten thousand sticks", "عشرة آلاف عصا"),
            new UnitType("UC", "Telecommunication port", "منفذ الاتصالات"),
            new UnitType("VI", "Vial", "زجاجة صغيرة"),
            new UnitType("VQ", "Bulk", "بالجملة"),
            new UnitType("YDK", "Square yard", "ياردة مربعة"),
            new UnitType("Z3", "Cask", "برميل"),
        };
        public static IEnumerable<UnitType> GetAll()
        {
            return Codes;
        }

        public static UnitType GetByCode(string code)
        {
            return Codes.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }
        public static UnitType GetByEnglishDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.EnglishDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }
        public static UnitType GetByArabicDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.ArabicDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public class UnitType
    {
        public string Code { get; }
        public string EnglishDescription { get; }
        public string ArabicDescription { get; }

        public UnitType(string code, string englishDescription, string arabicDescription)
        {
            Code = code;
            EnglishDescription = englishDescription;
            ArabicDescription = arabicDescription;
        }
    }
}
