using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

            //SeedCompanies(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        private void SeedCompanies(ModelBuilder modelBuilder)
        {
            var address = Address.Create(
                branchId: 0,
                country: "Egypt",
                governorate: "Cairo",
                regionCity: "NasrCity",
                street: "Dummy Street",
                buildingNumber: "123",
                postalCode: "000",
                floor: null,
                room: null,
                landmark: null,
                additionalInformation: null);

            var credentials = ClientCredentials.Create(
                clientId: "client-id",
                clientSecret1: "client-secret-1",
                clientSecret2: "client-secret-2",
                tokenPin: "token-pin",
                certificate: null);

            var company = Company.Create(
                name: "Dummy Company",
                phone: "01000000000",
                email: "dummy@example.com",
                taxNumber: "123456789",
                commercialRegistrationNo: "123456789",
                address: address.Value,
                activityCode: "6209",
                type: CompanyType.B,
                credentials: credentials.Value,
                eInvoiceToken: null);

            modelBuilder.Entity<Company>().HasData(company);
        }

    }
}
