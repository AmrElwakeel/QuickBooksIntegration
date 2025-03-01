using Application.Dtos;
using Application.Dtos.Response;
using Application.Services.Abstractions;
using Infrastructure.Integrations.QuickBooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Integrations.Invoices
{
    public class InvoicesService : IInvoicesService
    {
        public async Task<List<InvoiceDto>> GetInvoicesAsync(string _accessToken)
        {
            // The base URL for QuickBooks API
            string url = $"https://sandbox.quickbooks.api.intuit.com/v3/company/YOUR_COMPANY_ID/invoice"; // Replace with your company ID

            using (var httpClient = new HttpClient())
            {
                // Set the access token in the Authorization header
                if (string.IsNullOrEmpty(_accessToken))
                {
                    throw new Exception("Access token is not set. Please authenticate first.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    // Send the GET request
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Read and deserialize the response content
                    var content = await response.Content.ReadAsStringAsync();
                    var invoiceResponse = JsonSerializer.Deserialize<InvoiceResponse>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // To handle case insensitivity
                    });

                    return invoiceResponse.Invoices; // Assuming the response has a property 'Invoices'
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Error fetching invoices: {ex.Message}");
                }
            }
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(string _accessToken, InvoiceDto invoice)
        {
            // The base URL for creating an invoice
            string url = $"https://sandbox.quickbooks.api.intuit.com/v3/company/YOUR_COMPANY_ID/invoice"; // Replace with your company ID

            using (var httpClient = new HttpClient())
            {
                // Set the access token in the Authorization header
                if (string.IsNullOrEmpty(_accessToken))
                {
                    throw new Exception("Access token is not set. Please authenticate first.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    // Serialize the invoice object to JSON
                    var jsonContent = JsonSerializer.Serialize(invoice);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Send the POST request
                    var response = await httpClient.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();

                    // Read and deserialize the response content
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var createdInvoice = JsonSerializer.Deserialize<InvoiceDto>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // To handle case insensitivity
                    });

                    return createdInvoice; // Return the created invoice
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Error creating invoice: {ex.Message}");
                }
            }
        }
    }
}
