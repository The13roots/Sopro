﻿using System;
using System.Collections.Generic;
using Uebungsprojekt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Uebungsprojekt.Controllers
{
    /// <summary>
    /// Controller for Booking model
    /// </summary>
    public class BookingController : Controller
    {
        private IMemoryCache _cache;

        private List<Booking> bookingList = new List<Booking>();

        /// <summary>
        /// Constructor of controller.
        /// </summary>
        /// <param name="memoryCache">IMemoryCache object for initializing the memory cache</param>
        public BookingController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }       
        /// <summary>
        /// Displays the booking View and passes the booking list initialized in the constructor as well as the booking in the cache, if one exists
        /// </summary>
        /// <returns>
        /// The booking View displaying the list of bookings
        /// </returns>
        public IActionResult Index()
        {
            if (_cache.TryGetValue("CreateBooking", out List<Booking> createdBookings))
            {
                bookingList.AddRange(createdBookings);
            }
            return View(bookingList);
        }

        /// <summary>
        /// Displays the Create Booking View only on GET request
        /// </summary>
        /// <returns>
        /// Booking View
        /// </returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Booking());
        }
        [HttpPost]
        public IActionResult Edit(Booking booking)
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Receives a booking request from the Create Form, checks if the data is valid and inserts it into the cache
        /// </summary>
        /// <param name="booking">The booking to be inserted in the cache and the bookings table</param>
        /// <returns>
        /// Returns the booking View on success (valid booking object) and remains on create View on failure
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            // Server side validation
            if (!ModelState.IsValid) return View();
            
            if (_cache.TryGetValue("CreateBooking", out List<Booking> createdBookings))
            {
                createdBookings.Add(booking);
            } 
            else
            {
                createdBookings = new List<Booking> {booking};
                _cache.Set("CreateBooking", createdBookings);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Export()
        {
            if (_cache.TryGetValue("CreateBooking", out List<Booking> bookings))
            {
                string json = JsonConvert.SerializeObject(bookings, Formatting.Indented);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json); // file content for FileContentResult
                var exportfile = new FileContentResult(bytes, "application/octet-stream"); // Initializes a new instance of the FileContentResult class by using the specified arbitrary binary data
                exportfile.FileDownloadName = "Bookings.json";
                return exportfile;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Import(List<IFormFile> json_files)
        {
            if(json_files.Count == 1)
                if (json_files[0].FileName.EndsWith(".json") && json_files[0].Length < 1000000)
                {
                    // Deserialize list of bookings
                    StreamReader reader = new StreamReader(json_files[0].OpenReadStream());
                    string json = reader.ReadToEnd();
                    bool success = true;
                    var settings = new JsonSerializerSettings
                    {
                        Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
                        MissingMemberHandling = MissingMemberHandling.Error
                    };
                    List<Booking> importedBookings = JsonConvert.DeserializeObject<List<Booking>>(json, settings);
                    // If success, add to cached booking list
                    if (success)
                    {

                        if (_cache.TryGetValue("CreateBooking", out List<Booking> createdBookings))
                        {
                            createdBookings.AddRange(importedBookings);
                        }
                        else
                        {
                            _cache.Set("CreateBooking", importedBookings);
                        }
                    }
                }
                return RedirectToAction("Index");
        }
    }
}
