using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Evidence.Data;
using Evidence.Models;

namespace Evidence.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Database _context;

        public CustomerController(Database context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index(int? id)
        {
            var customer = from p in _context.Customers
                           select p;
            if (id == null)
            {
                customer = customer.Where(p => p.DocumentId == id);
            }

            
                return View(await _context.Customers.ToListAsync());
            
        }
        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(i => i.CustomerInsurances)
                    .ThenInclude(pi => pi.Insurances)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["Insurances"] = _context.Insurances.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,DocumentId,FirstName,LastName,Phone,Email,Street,RegistryNumber,OrientationNumber,City,Zip,Country")] Customer customer, int[] selectedInsurances)
        {
            if (ModelState.IsValid)
            {
                customer = _context.Add(customer).Entity;

                await _context.SaveChangesAsync();


            }
            else
            {
                ViewData["Insurances"] = _context.Insurances.ToList();
                return View(customer);
            }

            var customerToUpdate = await _context.Customers
                    .Include(i => i.CustomerInsurances)
                    .ThenInclude(pi => pi.Insurances)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            UpdateCustomerInsurances(selectedInsurances, customerToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(i => i.CustomerInsurances)
                    .ThenInclude(ci => ci.Insurances)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["Insurances"] = _context.Insurances.ToList();
            ViewData["PersonInsurances"] = customer.CustomerInsurances.Select(ci => ci.InsuranceId).ToList();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, int[] selectedInsurances)
        {
            var customerToUpdate = await _context.Customers
               .Include(c => c.CustomerInsurances)
                   .ThenInclude(pi => pi.Insurances)

               .FirstOrDefaultAsync(c => c.CustomerId == id);


            if (await TryUpdateModelAsync<Customer>(
        customerToUpdate, "",

        c => c.CustomerId, c => c.DocumentId, c => c.FirstName, c => c.LastName, c => c.Phone, c => c.Email, c => c.Street, c => c.RegistryNumber, c => c.OrientationNumber, c => c.City, c => c.Zip, c => c.Country, c => c.CustomerInsurances))
            {
                try
                {
                    UpdateCustomerInsurances(selectedInsurances, customerToUpdate);
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customerToUpdate.CustomerId))
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

            
            return View(customerToUpdate);
        }



        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                 .Include(i => i.CustomerInsurances)
                    .ThenInclude(pi => pi.Insurances)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(c => c.CustomerId == id);
        }

        private void UpdateCustomerInsurances(int[] selectedInsurances, Customer customer)
        {
            if (selectedInsurances == null)
            {
                customer.CustomerInsurances = new List<CustomerInsurance>();
                return;
            }
            var selectedInsurancesHS = new HashSet<int>(selectedInsurances);
            var customerInsurances = new HashSet<int>
                (customer.CustomerInsurances.Select(i => i.Insurances.InsuranceId));
            foreach (var insurance in _context.Insurances)
            {
                if (selectedInsurancesHS.Contains(insurance.InsuranceId))
                {
                    if (!customerInsurances.Contains(insurance.InsuranceId))
                    {
                        customer.CustomerInsurances.Add(new CustomerInsurance { CustomerId = customer.CustomerId, InsuranceId = insurance.InsuranceId });
                    }
                }
                else
                {

                    if (customerInsurances.Contains(insurance.InsuranceId))
                    {
                        CustomerInsurance insuranceToRemove = customer.CustomerInsurances.FirstOrDefault(i => i.InsuranceId == insurance.InsuranceId);
                        _context.Remove(insuranceToRemove);
                    }
                }
            }
        }
    }
}
