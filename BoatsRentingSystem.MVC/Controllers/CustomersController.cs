using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BoatsRentingSystem.MVC.Models;

namespace BoatRentalSystem.MVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/customers");
            response.EnsureSuccessStatusCode();

            var customers = await JsonSerializer.DeserializeAsync<IEnumerable<CustomerViewModel>>(await response.Content.ReadAsStreamAsync());
            return View(customers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/customers/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var customer = await JsonSerializer.DeserializeAsync<CustomerViewModel>(await response.Content.ReadAsStreamAsync());
            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                var customerJson = new StringContent(JsonSerializer.Serialize(customer), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/customers", customerJson);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/customers/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var customer = await JsonSerializer.DeserializeAsync<CustomerViewModel>(await response.Content.ReadAsStreamAsync());
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerViewModel customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var customerJson = new StringContent(JsonSerializer.Serialize(customer), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/customers/{id}", customerJson);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/customers/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var customer = await JsonSerializer.DeserializeAsync<CustomerViewModel>(await response.Content.ReadAsStreamAsync());
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/customers/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
    }
}
