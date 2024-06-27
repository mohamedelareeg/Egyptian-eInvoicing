using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using System.Collections.Generic;

namespace EgyptianeInvoicing.Core.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public int BranchId { get; private set; } = 0;
        public string Country { get; private set; } = "EG";
        public string Governorate { get; private set; } = "Cairo";
        public string? RegionCity { get; private set; }
        public string? Street { get; private set; }
        public string? BuildingNumber { get; private set; }
        public string? PostalCode { get; private set; }
        public string? Floor { get; private set; }
        public string? Room { get; private set; }
        public string? Landmark { get; private set; }
        public string? AdditionalInformation { get; private set; }

        private Address() { }

        private Address(int branchId , string country, string governorate, string? regionCity, string? street, string? buildingNumber, string? postalCode, string? floor, string? room, string? landmark, string? additionalInformation)
        {
            BranchId = branchId;
            Country = country;
            Governorate = governorate;
            RegionCity = regionCity;
            Street = street;
            BuildingNumber = buildingNumber;
            PostalCode = postalCode;
            Floor = floor;
            Room = room;
            Landmark = landmark;
            AdditionalInformation = additionalInformation;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return BranchId;
            yield return Country;
            yield return Governorate;
            yield return RegionCity;
            yield return Street;
            yield return BuildingNumber;
            yield return PostalCode;
            yield return Floor;
            yield return Room;
            yield return Landmark;
            yield return AdditionalInformation;
        }

        public static Result<Address> Create(int branchId, string country, string governorate, string? regionCity, string? street, string? buildingNumber, string? postalCode, string? floor, string? room, string? landmark, string? additionalInformation)
        {
            if (string.IsNullOrEmpty(country))
                return Result.Failure<Address>("Address.Create", "Country is required.");

            var address = new Address(branchId, country, governorate, regionCity, street, buildingNumber, postalCode, floor, room, landmark, additionalInformation);
            return Result.Success(address);
        }

        public Result<bool> Modify(int branchId = 0, string? country = null, string? governorate = null, string? regionCity = null, string? street = null, string? buildingNumber = null, string? postalCode = null, string? floor = null, string? room = null, string? landmark = null, string? additionalInformation = null)
        {
            if (country != null && string.IsNullOrEmpty(country))
                return Result.Failure<bool>("Address.Modify", "Country cannot be null or empty.");

            BranchId = branchId;
            Country = country ?? Country;
            Governorate = governorate ?? Governorate;
            RegionCity = regionCity;
            Street = street;
            BuildingNumber = buildingNumber;
            PostalCode = postalCode;
            Floor = floor;
            Room = room;
            Landmark = landmark;
            AdditionalInformation = additionalInformation;

            return Result.Success(true);
        }
    }
}
