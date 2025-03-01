using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Response
{
    public class InvoiceResponse
    {
        public List<InvoiceDto> Invoices { get; set; }
        public string SyncToken { get; set; }
        public string QueryResponse { get; set; }
        // Add other relevant properties as required
    }
}
