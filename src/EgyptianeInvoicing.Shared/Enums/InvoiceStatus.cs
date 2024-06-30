using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace EgyptianeInvoicing.Shared.Enums
{
    public enum InvoiceStatus
    {
        [Display(Name = "Valid")]
        Valid,

        [Display(Name = "Invalid")]
        Invalid,

        [Display(Name = "Rejected")]
        Rejected,

        [Display(Name = "Cancelled")]
        Cancelled,

        [Display(Name = "Submitted")]
        Submitted,

        [Display(Name = "Suspended")]
        Suspended
    }
}