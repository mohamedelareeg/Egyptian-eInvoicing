using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Constants
{
    public static class Currencies
    {
        public static readonly List<Currency> Codes = new List<Currency>
        {
            new Currency("AED", "UAE Dirham", "درهم إماراتي"),
            new Currency("AFN", "Afghani", "أفغاني"),
            new Currency("ALL", "Lek", "ليك ألباني"),
            new Currency("AMD", "Armenian Dram", "درام أرميني"),
            new Currency("ANG", "Netherlands Antillean Guilder", "جلدر أنتيل هولندي"),
            new Currency("AOA", "Kwanza", "كوانزا أنغولي"),
            new Currency("ARS", "Argentine Peso", "بيزو أرجنتيني"),
            new Currency("AUD", "Australian Dollar", "دولار أسترالي"),
            new Currency("AWG", "Aruban Florin", "فلورن أروبي"),
            new Currency("AZN", "Azerbaijan Manat", "مانات أذربيجاني"),
            new Currency("BAM", "Convertible Mark", "مارك قابل للتحويل"),
            new Currency("BBD", "Barbados Dollar", "دولار بربادوسي"),
            new Currency("BDT", "Taka", "تاكا بنغلاديشي"),
            new Currency("BGN", "Bulgarian Lev", "ليف بلغاري"),
            new Currency("BHD", "Bahraini Dinar", "دينار بحريني"),
            new Currency("BIF", "Burundi Franc", "فرنك بوروندي"),
            new Currency("BMD", "Bermudian Dollar", "دولار برمودي"),
            new Currency("BND", "Brunei Dollar", "دولار بروناي"),
            new Currency("BOB", "Boliviano", "بوليفيانو"),
            new Currency("BOV", "Mvdol", "Mvdol"),
            new Currency("BRL", "Brazilian Real", "ريال برازيلي"),
            new Currency("BSD", "Bahamian Dollar", "دولار باهامي"),
            new Currency("BTN", "Ngultrum", "نغولترم"),
            new Currency("BWP", "Pula", "بولا بوتسواني"),
            new Currency("BYN", "Belarusian Ruble", "روبل بيلاروسي"),
            new Currency("BZD", "Belize Dollar", "دولار بليزي"),
            new Currency("CAD", "Canadian Dollar", "دولار كندي"),
            new Currency("CDF", "Congolese Franc", "فرنك كونغولي"),
            new Currency("CHE", "WIR Euro", "WIR يورو"),
            new Currency("CHF", "Swiss Franc", "فرنك سويسري"),
            new Currency("CHW", "WIR Franc", "WIR فرنك"),
            new Currency("CLF", "Unidad de Fomento", "وحدة التحفيز"),
            new Currency("CLP", "Chilean Peso", "بيزو شيلي"),
            new Currency("CNY", "Yuan Renminbi", "يوان صيني"),
            new Currency("COP", "Colombian Peso", "بيزو كولومبي"),
            new Currency("COU", "Unidad de Valor Real", "وحدة القيمة الحقيقية"),
            new Currency("CRC", "Costa Rican Colon", "كولون كوستاريكي"),
            new Currency("CUC", "Peso Convertible", "بيزو قابل للتحويل"),
            new Currency("CUP", "Cuban Peso", "بيزو كوبي"),
            new Currency("CVE", "Cabo Verde Escudo", "اسكودو الرأس الأخضر"),
            new Currency("CZK", "Czech Koruna", "كرونة تشيكية"),
            new Currency("DJF", "Djibouti Franc", "فرنك جيبوتي"),
            new Currency("DKK", "Danish Krone", "كرونة دانماركية"),
            new Currency("DOP", "Dominican Peso", "بيزو دومينيكاني"),
            new Currency("DZD", "Algerian Dinar", "دينار جزائري"),
            new Currency("EGP", "Egyptian Pound", "جنيه مصري"),
            new Currency("ERN", "Nakfa", "ناكفا"),
            new Currency("ETB", "Ethiopian Birr", "بير إثيوبي"),
            new Currency("EUR", "Euro", "يورو"),
            new Currency("FJD", "Fiji Dollar", "دولار فيجي"),
            new Currency("FKP", "Falkland Islands Pound", "جنيه جزر فوكلاند"),
            new Currency("GBP", "Pound Sterling", "جنيه إسترليني"),
            new Currency("GEL", "Lari", "لاري جورجي"),
            new Currency("GHS", "Ghana Cedi", "سيدي غانا"),
            new Currency("GIP", "Gibraltar Pound", "جنيه جبل طارق"),
            new Currency("GMD", "Dalasi", "دالاسي"),
            new Currency("GNF", "Guinean Franc", "فرنك غيني"),
            new Currency("GTQ", "Quetzal", "كويتزال"),
            new Currency("GYD", "Guyana Dollar", "دولار غيانا"),
            new Currency("HKD", "Hong Kong Dollar", "دولار هونغ كونغ"),
            new Currency("HNL", "Lempira", "ليمبيرا هندوراسي"),
            new Currency("HRK", "Kuna", "كونا كرواتي"),
            new Currency("HTG", "Gourde", "جورد هايتي"),
            new Currency("HUF", "Forint", "فورنت هنغاري"),
            new Currency("IDR", "Rupiah", "روبية إندونيسية"),
            new Currency("ILS", "New Israeli Sheqel", "شيقل إسرائيلي جديد"),
            new Currency("INR", "Indian Rupee", "روبية هندية"),
            new Currency("IQD", "Iraqi Dinar", "دينار عراقي"),
            new Currency("IRR", "Iranian Rial", "ريال إيراني"),
            new Currency("ISK", "Iceland Krona", "كرونة أيسلندية"),
            new Currency("JMD", "Jamaican Dollar", "دولار جامايكي"),
            new Currency("JOD", "Jordanian Dinar", "دينار أردني"),
            new Currency("JPY", "Yen", "ين ياباني"),
            new Currency("KES", "Kenyan Shilling", "شلن كيني"),
            new Currency("KGS", "Som", "سوم قيرغيزستاني"),
            new Currency("KHR", "Riel", "رييل كمبودي"),
            new Currency("KMF", "Comorian Franc", "فرنك جزر القمر"),
            new Currency("KPW", "North Korean Won", "وون كوريا الشمالية"),
            new Currency("KRW", "Won", "وون كوريا الجنوبية"),
            new Currency("KWD", "Kuwaiti Dinar", "دينار كويتي"),
            new Currency("KYD", "Cayman Islands Dollar", "دولار جزر كايمان"),
            new Currency("KZT", "Tenge", "تينغي كازاخستاني"),
            new Currency("LAK", "Lao Kip", "كيب لاوسي"),
            new Currency("LBP", "Lebanese Pound", "جنيه لبناني"),
            new Currency("LKR", "Sri Lanka Rupee", "روبية سريلانكية"),
            new Currency("LRD", "Liberian Dollar", "دولار ليبري"),
            new Currency("LSL", "Loti", "لوتي ليسوتو"),
            new Currency("LYD", "Libyan Dinar", "دينار ليبي"),
            new Currency("MAD", "Moroccan Dirham", "درهم مغربي"),
            new Currency("MDL", "Moldovan Leu", "ليو مولدوفي"),
            new Currency("MGA", "Malagasy Ariary", "أرياري مدغشقري"),
            new Currency("MKD", "Denar", "دينار مقدوني"),
            new Currency("MMK", "Kyat", "كيات ميانماري"),
            new Currency("MNT", "Tugrik", "توغريك منغولي"),
            new Currency("MOP", "Pataca", "باتاكا ماكاوي"),
            new Currency("MRU", "Ouguiya", "أوقية موريتانية"),
            new Currency("MUR", "Mauritius Rupee", "روبية موريشيوسية"),
            new Currency("MVR", "Rufiyaa", "رفيعة مالديفية"),
            new Currency("MWK", "Malawi Kwacha", "كواشا مالاوي"),
            new Currency("MXN", "Mexican Peso", "بيزو مكسيكي"),
            new Currency("MXV", "Mexican Unidad de Inversion (UDI)", "وحدة الاستثمار المكسيكية"),
            new Currency("MYR", "Malaysian Ringgit", "رينغيت ماليزي"),
            new Currency("MZN", "Mozambique Metical", "متيكال موزمبيقي"),
            new Currency("NAD", "Namibia Dollar", "دولار ناميبي"),
            new Currency("NGN", "Naira", "نايرا نيجيري"),
            new Currency("NIO", "Cordoba Oro", "كوردوبا نيكاراغوي"),
            new Currency("NOK", "Norwegian Krone", "كرونة نرويجية"),
            new Currency("NPR", "Nepalese Rupee", "روبية نيبالية"),
            new Currency("NZD", "New Zealand Dollar", "دولار نيوزيلندي"),
            new Currency("OMR", "Rial Omani", "ريال عماني"),
            new Currency("PAB", "Balboa", "بالبوا"),
            new Currency("PEN", "Sol", "سول بيروفي"),
            new Currency("PGK", "Kina", "كينا بابوا غينيا الجديدة"),
            new Currency("PHP", "Philippine Peso", "بيزو فلبيني"),
            new Currency("PKR", "Pakistan Rupee", "روبية باكستانية"),
            new Currency("PLN", "Zloty", "زلوتي بولندي"),
            new Currency("PYG", "Guarani", "غواراني باراغواي"),
            new Currency("QAR", "Qatari Rial", "ريال قطري"),
            new Currency("RON", "Romanian Leu", "ليو روماني"),
            new Currency("RSD", "Serbian Dinar", "دينار صربي"),
            new Currency("RUB", "Russian Ruble", "روبل روسي"),
            new Currency("RWF", "Rwanda Franc", "فرنك رواندي"),
            new Currency("SAR", "Saudi Riyal", "ريال سعودي"),
            new Currency("SBD", "Solomon Islands Dollar", "دولار جزر سليمان"),
            new Currency("SCR", "Seychelles Rupee", "روبية سيشيلية"),
            new Currency("SDG", "Sudanese Pound", "جنيه سوداني"),
            new Currency("SEK", "Swedish Krona", "كرونة سويدية"),
            new Currency("SGD", "Singapore Dollar", "دولار سنغافوري"),
            new Currency("SHP", "Saint Helena Pound", "جنيه سانت هيلينا"),
            new Currency("SLL", "Leone", "ليون سيراليوني"),
            new Currency("SOS", "Somali Shilling", "شلن صومالي"),
            new Currency("SRD", "Surinam Dollar", "دولار سورينامي"),
            new Currency("SSP", "South Sudanese Pound", "جنيه جنوب السودان"),
            new Currency("STN", "Dobra", "دوبرا ساو تومي وبرينسيبي"),
            new Currency("SVC", "El Salvador Colon", "كولون سلفادوري"),
            new Currency("SYP", "Syrian Pound", "جنيه سوري"),
            new Currency("SZL", "Lilangeni", "ليلانجيني"),
            new Currency("THB", "Baht", "بات تايلاندي"),
            new Currency("TJS", "Somoni", "سوموني طاجيكستاني"),
            new Currency("TMT", "Turkmenistan New Manat", "مانات تركمانستاني جديد"),
            new Currency("TND", "Tunisian Dinar", "دينار تونسي"),
            new Currency("TOP", "Pa’anga", "بانغا تونغا"),
            new Currency("TRY", "Turkish Lira", "ليرة تركية"),
            new Currency("TTD", "Trinidad and Tobago Dollar", "دولار ترينيداد وتوباغو"),
            new Currency("TWD", "New Taiwan Dollar", "دولار تايواني جديد"),
            new Currency("TZS", "Tanzanian Shilling", "شلن تنزاني"),
            new Currency("UAH", "Hryvnia", "هريفنيا أوكراني"),
            new Currency("UGX", "Uganda Shilling", "شلن أوغندي"),
            new Currency("USD", "US Dollar", "دولار أمريكي"),
            new Currency("USN", "US Dollar (Next day)", "دولار أمريكي (يوم الغد)"),
            new Currency("UYI", "Uruguay Peso en Unidades Indexadas (UI)", "بيزو أوروغواي بالوحدات المؤشرة"),
            new Currency("UYU", "Peso Uruguayo", "بيزو أوروغواي"),
            new Currency("UYW", "Unidad Previsional", "وحدة التقاعد"),
            new Currency("UZS", "Uzbekistan Sum", "سوم أوزبكستاني"),
            new Currency("VED", "Bolívar Soberano", "بوليفار صاحب السيادة"),
            new Currency("VES", "Bolívar Soberano", "بوليفار صاحب السيادة"),
            new Currency("VND", "Dong", "دونغ فيتنامي"),
            new Currency("VUV", "Vatu", "فاتو فانواتو"),
            new Currency("WST", "Tala", "تالا ساموا"),
            new Currency("XAF", "CFA Franc BEAC", "فرنك CFA BEAC"),
            new Currency("XAG", "Silver", "فضة"),
            new Currency("XAU", "Gold", "ذهب"),
            new Currency("XBA", "Bond Markets Unit European Composite Unit (EURCO)", "وحدة مؤشر السوق الأوروبي المركب"),
            new Currency("XBB", "Bond Markets Unit European Monetary Unit (E.M.U.-6)", "وحدة السوق الأوروبية النقدية"),
            new Currency("XBC", "Bond Markets Unit European Unit of Account 9 (E.U.A.-9)", "وحدة السوق الأوروبية للحساب 9"),
            new Currency("XBD", "Bond Markets Unit European Unit of Account 17 (E.U.A.-17)", "وحدة السوق الأوروبية للحساب 17"),
            new Currency("XCD", "East Caribbean Dollar", "دولار شرق الكاريبي"),
            new Currency("XDR", "SDR (Special Drawing Right)", "حقوق السحب الخاصة (SDR)"),
            new Currency("XOF", "CFA Franc BCEAO", "فرنك CFA BCEAO"),
            new Currency("XPD", "Palladium", "بلاديوم"),
            new Currency("XPF", "CFP Franc", "فرنك CFP"),
            new Currency("XPT", "Platinum", "بلاتينيوم"),
            new Currency("XSU", "Sucre", "سوكري"),
            new Currency("XTS", "Codes specifically reserved for testing purposes", "رموز محجوزة خصيصًا لأغراض الاختبار"),
            new Currency("XUA", "ADB Unit of Account", "وحدة الحساب للبنك الآسيوي للتنمية"),
            new Currency("XXX", "The codes assigned for transactions where no currency is involved", "الرموز المخصصة للمعاملات التي لا تنطوي على عملة"),
            new Currency("YER", "Yemeni Rial", "ريال يمني"),
            new Currency("ZAR", "Rand", "راند جنوب أفريقيا"),
            new Currency("ZMW", "Zambian Kwacha", "كواشا زامبي"),
            new Currency("ZWL", "Zimbabwe Dollar", "دولار زيمبابوي")
        };
        public static IEnumerable<Currency> GetAll()
        {
            return Codes;
        }

        public static Currency GetByCode(string code)
        {
            return Codes.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }
        public static Currency GetByEnglishDescription(string description)
        {
            return Codes.FirstOrDefault(a => a.EnglishDescription.Equals(description, StringComparison.InvariantCultureIgnoreCase));
        }

    }

    public class Currency
    {
        public string Code { get; }
        public string EnglishDescription { get; }
        public string ArabicDescription { get; }

        public Currency(string code, string englishDescription, string arabicDescription)
        {
            Code = code;
            EnglishDescription = englishDescription;
            ArabicDescription = arabicDescription;
        }
    }

}
