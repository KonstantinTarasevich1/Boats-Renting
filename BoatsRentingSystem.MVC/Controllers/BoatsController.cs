using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BoatsRentingSystem.MVC.Models;

namespace BoatRentalSystem.MVC.Controllers
{
    public class BoatsController : Controller
    {
        private readonly HttpClient _httpClient;

        public BoatsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BoatsApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/boats");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var boats = await JsonSerializer.DeserializeAsync<IEnumerable<BoatViewModel>>(await response.Content.ReadAsStreamAsync());
            return View(boats);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/boats/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var boat = await JsonSerializer.DeserializeAsync<BoatViewModel>(await response.Content.ReadAsStreamAsync());
            return View(boat);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoatViewModel boat)
        {
            if (ModelState.IsValid)
            {
                var boatJson = new StringContent(JsonSerializer.Serialize(boat), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/boats", boatJson);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(boat);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/boats/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var boat = await JsonSerializer.DeserializeAsync<BoatViewModel>(await response.Content.ReadAsStreamAsync());
            return View(boat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BoatViewModel boat)
        {
            if (id != boat.BoatId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var boatJson = new StringContent(JsonSerializer.Serialize(boat), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/boats/{id}", boatJson);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(boat);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/boats/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var boat = await JsonSerializer.DeserializeAsync<BoatViewModel>(await response.Content.ReadAsStreamAsync());
            return View(boat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/boats/{id}");
            if (!response.IsSuccessStatusCode)
            {
              
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
