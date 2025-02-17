﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApplication.Data;
using BookingApplication.Models;
using System.Security.Claims;
using BookingApplication.Models.DTO;

namespace BookingApplication.Controllers
{
    public class BookingListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingLists
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users
                .Include(z => z.bookingList)
                .Include(z => z.bookingList.bookReservations)
                .Include("bookingList.bookReservations.reservation")
                .Include("bookingList.bookReservations.reservation.Apartment")
                .SingleOrDefault(z => z.Id == userId);

            var bookingList = user.bookingList;

            BookingListDTO model = new BookingListDTO()
            {
                AllReservations = bookingList.bookReservations.ToList() ?? new List<BookReservation>(),
                TotalPrice = bookingList.bookReservations.Sum(z => z.Number_of_nights * z.reservation.Apartment.Price_per_night)
            };

            return View(model);
        }

        // GET: BookingLists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingList = await _context.BookingLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingList == null)
            {
                return NotFound();
            }

            return View(bookingList);
        }

        // GET: BookingLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookingLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerId")] BookingList bookingList)
        {
            if (ModelState.IsValid)
            {
                bookingList.Id = Guid.NewGuid();
                _context.Add(bookingList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookingList);
        }

        // GET: BookingLists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingList = await _context.BookingLists.FindAsync(id);
            if (bookingList == null)
            {
                return NotFound();
            }
            return View(bookingList);
        }

        // POST: BookingLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OwnerId")] BookingList bookingList)
        {
            if (id != bookingList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingListExists(bookingList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookingList);
        }

        // GET: BookingLists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingList = await _context.BookingLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingList == null)
            {
                return NotFound();
            }

            return View(bookingList);
        }

        // POST: BookingLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bookingList = await _context.BookingLists.FindAsync(id);
            if (bookingList != null)
            {
                _context.BookingLists.Remove(bookingList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingListExists(Guid id)
        {
            return _context.BookingLists.Any(e => e.Id == id);
        }
        public async Task<IActionResult> DeleteReservationFromBooking(Guid reservationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
                
            var user = _context.Users
                .Include(u => u.bookingList)
                    .ThenInclude(bl => bl.bookReservations)
                        .ThenInclude(br => br.reservation)
                .SingleOrDefault(u => u.Id == userId);

            var bookingList = user.bookingList;

            var itemToDelete = _context.BookReservation.Where(z => z.reservationId == reservationId).FirstOrDefault();
            bookingList.bookReservations.Remove(itemToDelete);
            _context.Update(bookingList);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> BookNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            var user = _context.Users
                .Include(z => z.bookingList)
                .Include(z => z.bookingList.bookReservations)
                .Include("bookingList.bookReservations.reservation")
                .SingleOrDefault(z => z.Id == userId);

            var bookingList = user.bookingList;
            bookingList.bookReservations.Clear();
            _context.BookingLists.Update(bookingList);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
