
namespace EgyptianeInvoicing.Shared.Dtos
{
    public class ImportedInvoiceDto
    {
        public string SerialNumber { get; set; } // مسلسل الفاتورة
        public DateTime IssueDate { get; set; } = DateTime.Now; // تاريخ الاصدار
        public string InvoiceType { get; set; } = "فاتورة"; // نوع الفاتورة
        public decimal? AdditionalDiscount { get; set; } // خصم اضافى على اجمالى الفاتورة
        public string? Reference { get; set; } // المرجع
        public string CustomerType { get; set; } = "شركة"; // الصفة
        public string Name { get; set; } // الاسم
        public string RegisterNumber { get; set; } // الرقم التعريفى
        public string Country { get; set; } = "مصر"; // الدولة
        public string? Governorate { get; set; } // المحافظة
        public string? District { get; set; } // الحى
        public string? Street { get; set; } // الشارع
        public string? PropertyNumber { get; set; } // رقم العقار
        public List<ImportedInvoiceItemDto> Items { get; set; }// بيان الاصناف

        public bool Uploaded { get; set; } = false;



    }
}
