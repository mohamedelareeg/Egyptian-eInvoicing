using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Shared.Requests
{
    public class SearchDocumentsRequestDto
    {
        private DateTime? _submissionDateFrom;
        private DateTime? _submissionDateTo;

        public DateTime? SubmissionDateFrom
        {
            get
            {
                if (!_submissionDateFrom.HasValue)
                {
                    _submissionDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                }
                return _submissionDateFrom;
            }
            set { _submissionDateFrom = value; }
        }

        public DateTime? SubmissionDateTo
        {
            get
            {
                if (!_submissionDateTo.HasValue)
                {
                    _submissionDateTo = DateTime.Today;
                }
                return _submissionDateTo;
            }
            set { _submissionDateTo = value; }
        }
        public string? ContinuationToken { get; set; } = "";
        public int PageSize { get; set; } = 10;
        public string? Direction { get; set; } = "";
        public DateTime? IssueDateFrom { get; set; } = null;
        public DateTime? IssueDateTo { get; set; } = null;
        public string? Status { get; set; } = "Valid";
        public string? DocumentType { get; set; } = "";
        public string? ReceiverType { get; set; } = "";
        public string? ReceiverId { get; set; } = "";
        public string? IssuerType { get; set; } = "";
        public string? IssuerId { get; set; } = "";
        public string? UUID { get; set; } = "";
        public string? InternalID { get; set; } = "";
    }
}
