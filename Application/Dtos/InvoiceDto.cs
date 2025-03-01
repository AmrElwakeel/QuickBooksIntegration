using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class InvoiceDto
    {
        public string Id { get; set; }
        public string CustomerRef { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<LineItem> LineItems { get; set; } // Include line items for invoice
        public decimal TotalAmt { get; set; }
        // Add other relevant fields as necessary
    }

    public class LineItem
    {
        public string Amount { get; set; }
        public string Description { get; set; }
        public string DetailType { get; set; }
        public string ItemRef { get; set; } // Reference to the item
    }
}
