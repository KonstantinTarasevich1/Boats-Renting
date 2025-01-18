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
    public class ReservationsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReservationsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/reservations");
            response.EnsureSuccessStatusCode();

            var reservations = await JsonSerializer.DeserializeAsync<IEnumerable<ReservationViewModel>>(await response.Content.ReadAsStreamAsync());
            return View(reservations);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/reservations/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var reservation = await JsonSerializer.DeserializeAsync<ReservationViewModel>(await response.Content.ReadAsStreamAsync());
            return View(reservation);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                var reservationJson = new StringContent(JsonSerializer.Serialize(reservation), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/reservations", reservationJson);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/reservations/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var reservation = await JsonSerializer.DeserializeAsync<ReservationViewModel>(await response.Content.ReadAsStreamAsync());
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReservationViewModel reservation)
        {
            if (id != reservation.ReservationId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var reservationJson = new StringContent(JsonSerializer.Serialize(reservation), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/reservations/{id}", reservationJson);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/reservations/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var reservation = await JsonSerializer.DeserializeAsync<ReservationViewModel>(await response.Content.ReadAsStreamAsync());
            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/reservations/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
    }
}
