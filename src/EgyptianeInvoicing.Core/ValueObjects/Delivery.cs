using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.ValueObjects
{
    public sealed class Delivery : ValueObject
    {
        public string? Approach { get; private set; }
        public string? Packaging { get; private set; }
        public DateTime? DateValidity { get; private set; }
        public string? ExportPort { get; private set; }
        public double? GrossWeight { get; private set; }
        public double? NetWeight { get; private set; }
        public string? Terms { get; private set; }

        private Delivery() { }

        private Delivery(string? approach, string? packaging, DateTime? dateValidity, string? exportPort, double? grossWeight, double? netWeight, string? terms)
        {
            Approach = approach;
            Packaging = packaging;
            DateValidity = dateValidity;
            ExportPort = exportPort;
            GrossWeight = grossWeight;
            NetWeight = netWeight;
            Terms = terms;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Approach;
            yield return Packaging;
            yield return DateValidity;
            yield return ExportPort;
            yield return GrossWeight;
            yield return NetWeight;
            yield return Terms;
        }

        public static Result<Delivery> Create(string? approach, string? packaging, DateTime? dateValidity, string? exportPort, double? grossWeight, double? netWeight, string? terms)
        {
            var delivery = new Delivery(approach, packaging, dateValidity, exportPort, grossWeight, netWeight, terms);
            return Result.Success(delivery);
        }

        public Result<bool> Modify(string? approach = null, string? packaging = null, DateTime? dateValidity = null, string? exportPort = null, double? grossWeight = null, double? netWeight = null, string? terms = null)
        {
            Approach = approach ?? Approach;
            Packaging = packaging ?? Packaging;
            DateValidity = dateValidity ?? DateValidity;
            ExportPort = exportPort ?? ExportPort;
            GrossWeight = grossWeight ?? GrossWeight;
            NetWeight = netWeight ?? NetWeight;
            Terms = terms ?? Terms;

            return Result.Success(true);
        }
    }
}
