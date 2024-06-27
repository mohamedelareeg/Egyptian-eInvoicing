using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;

namespace EgyptianeInvoicing.MVC.Constants
{
    public static class CompanyDtoSingleton
    {
        private static CompanyDto _instance;

        static CompanyDtoSingleton()
        {
            _instance = new CompanyDto();
        }
        public static CompanyDto Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }
        public static void SetCompany(CompanyDto companyDto)
        {
            Instance = companyDto;
        }
    }
}
