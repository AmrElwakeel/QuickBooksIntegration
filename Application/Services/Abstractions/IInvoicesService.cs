using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstractions
{
    public interface IInvoicesService
    {
        Task<List<InvoiceDto>> GetInvoicesAsync(string token);
        Task<InvoiceDto> CreateInvoiceAsync(string token, InvoiceDto invoice);
    }
}
