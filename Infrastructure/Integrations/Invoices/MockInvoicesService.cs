using Application.Dtos;
using Application.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Integrations.Invoices
{
    public class MockInvoicesService : IInvoicesService
    {
        private readonly List<InvoiceDto> _mockInvoices;

        public MockInvoicesService()
        {
            // Initialize with some mock data
            _mockInvoices = new List<InvoiceDto>
            {
                new InvoiceDto                 {
                    Id = "1",
                    CustomerRef = "Amr Elwakeel",
                    InvoiceDate = DateTime.Now,
                    TotalAmt = 1000,
                    LineItems = new List<LineItem>
                    {
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" },
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" },
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" },
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" },
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" }
                    }
                },
                new InvoiceDto{
                    Id = "2",
                    CustomerRef = "Mohammed",
                    InvoiceDate = DateTime.Now,
                    TotalAmt = 400,
                    LineItems = new List<LineItem>
                    {
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" },
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" }
                    }
                },
                new InvoiceDto{
                    Id = "3",
                    CustomerRef = "Sayed",
                    InvoiceDate = DateTime.Now,
                    TotalAmt = 400,
                    LineItems = new List<LineItem>
                    {
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" },
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" }

                    }
                },
                new InvoiceDto{
                    Id = "4",
                    CustomerRef = "Fahhd",
                    InvoiceDate = DateTime.Now,
                    TotalAmt = 200,
                    LineItems = new List<LineItem>
                    {
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" }
                    }
                },
                new InvoiceDto{
                    Id = "5",
                    CustomerRef = "Ahmed",
                    InvoiceDate = DateTime.Now,
                    TotalAmt = 200,
                    LineItems = new List<LineItem>
                    {
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" }
                    }
                },
                                new InvoiceDto{
                    Id = "5",
                    CustomerRef = "Mahmoud",
                    InvoiceDate = DateTime.Now,
                    TotalAmt = 200,
                    LineItems = new List<LineItem>
                    {
                        new LineItem { Amount = "200", Description = "Product 2", DetailType = "SalesItem", ItemRef = "Item2" }
                    }
                },
            };
        }

        public async Task<List<InvoiceDto>> GetInvoicesAsync(string _accessToken)
        {
            // Simulate a delay for async operation
            await Task.Delay(100);

            // Check for failure scenario
            if (string.IsNullOrEmpty(_accessToken))
            {
                throw new Exception("Access token is not set. Please authenticate first.");
            }

            // Simulate returning the mock invoices
            return _mockInvoices;
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(string _accessToken, InvoiceDto invoice)
        {
            // Simulate a delay for async operation
            await Task.Delay(100);

            // Check for failure scenario
            if (string.IsNullOrEmpty(_accessToken))
            {
                throw new Exception("Access token is not set. Please authenticate first.");
            }

            // Check for invoice validation (e.g., missing fields)
            if (invoice == null || string.IsNullOrEmpty(invoice.CustomerRef))
            {
                throw new Exception("Invalid invoice data.");
            }

            // Simulate adding the invoice to the mock list
            invoice.Id = (_mockInvoices.Count + 1).ToString(); // Assign a new ID
            _mockInvoices.Add(invoice);

            // Simulate returning the created invoice
            return invoice;
        }
    }
}