using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBD_TractionFive.Data;
using NBD_TractionFive.Models;
using Microsoft.EntityFrameworkCore.Storage;
using NBD_TractionFive.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace NBD_TractionFive.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly NBDContext _context;

        public ClientController(NBDContext context)
        {
            _context = context;
        }
        // GET: Clients/Index
        public async Task<IActionResult> IndexAsync(string SearchString, int? province, string actionButton, int? page, int? pageSizeID, string sortDirection = "asc", string sortField = "Client Name")
        {
            var clients = from p in _context.Clients
                          select p;


            if (!String.IsNullOrEmpty(SearchString))
            {
                clients = clients.Where(p => p.cliName.ToUpper().Contains(SearchString.ToUpper()));

                ViewData["Filtering"] = " show";
            }
            if (province.HasValue)
            {
                clients = clients.Where(p => (int)p.Province == province);
                ViewData["Filtering"] = " show";
            }

            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted so lets sort!
            {
                page = 1;
                if (actionButton != "Filter")//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }
            if (sortField == "Client Name")//Sorting by Client Name
            {
                if (sortDirection == "asc")
                {
                    clients = clients
                        .OrderBy(p => p.cliName);
                }
                else
                {
                    clients = clients
                        .OrderByDescending(p => p.cliName);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            int pageSize;//This is the value we will pass to PaginatedList
            if (pageSizeID.HasValue)
            {
                //Value selected from DDL so use and save it to Cookie
                pageSize = pageSizeID.GetValueOrDefault();
                CookieHelper.CookieSet(HttpContext, "pageSizeValue", pageSize.ToString(), 30);
            }
            else
            {
                //Not selected so see if it is in Cookie
                pageSize = Convert.ToInt32(HttpContext.Request.Cookies["pageSizeValue"]);
            }
            pageSize = (pageSize == 0) ? 3 : pageSize;//Neither Selected or in Cookie so go with default
            ViewData["pageSizeID"] =
                new SelectList(new[] { "3", "5", "10", "20", "30", "40", "50", "100", "500" }, pageSize.ToString());
            var pagedData = await PaginatedList<Client>.CreateAsync(clients.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }
        
        [Authorize(Roles ="Admin, Management, Sales")]
        public IActionResult Create()
        {

            //Add all (unchecked) Conditions to ViewBag
            var client = new Client();

            return View();
        }
        // POST: Clients/Create
        [HttpPost]
        [Authorize(Roles = "Admin, Management, Sales")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cliName,cliContactName,clicliPhonenumbernumber,clicontactType,Street,cliCity,Province, ZipCode,cliID")] Client client, string creating)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(creating))
            {

                switch (creating)
                {
                    case "Create":
                        _context.Add(client);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    case "Create and Add Project":
                        _context.Add(client);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Create", "Project", new { clientID = client.cliID });
                }

            }
            return View(client);
        }

        //GET: Clients/Edit
        [Authorize(Roles = "Admin, Management, Sales")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }
        //POST : EDIT
        [HttpPost]
        [Authorize(Roles = "Admin, Management, Sales")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("cliName,Street,cliCity,Province, ZipCode,cliContactName,cliPhonenumber,clicontactType,cliID")] Client client, Byte[] RowVersion)
        {
            if (id != client.cliID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(client).Property("RowVersion").OriginalValue = RowVersion;

                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Client)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Client was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Client)databaseEntry.ToObject();
                        if (databaseValues.cliName != clientValues.cliName)
                            ModelState.AddModelError("cliName", "Current value: "
                                + databaseValues.cliName);
                        if (databaseValues.cliContactName != clientValues.cliContactName)
                            ModelState.AddModelError("cliContactName", "Current value: "
                                + databaseValues.cliContactName);
                        if (databaseValues.clicontactType != clientValues.clicontactType)
                            ModelState.AddModelError("clicontactType", "Current value: "
                                + databaseValues.clicontactType);
                        if (databaseValues.cliPhonenumber != clientValues.cliPhonenumber)
                            ModelState.AddModelError("cliPhonenumber", "Current value: "
                                + String.Format("{0:(###) ###-####}", databaseValues.cliPhonenumber));

                        if (databaseValues.Street != clientValues.Street)
                            ModelState.AddModelError("Street", "Current value: "
                                + databaseValues.Street);
                        if (databaseValues.ZipCode != clientValues.ZipCode)
                            ModelState.AddModelError("ZipCode", "Current value: "
                                + databaseValues.ZipCode);
                        //if (databaseValues.DOB != clientValues.DOB)
                        //    ModelState.AddModelError("DOB", "Current value: "
                        //        + String.Format("{0:d}", databaseValues.DOB));

                        if (databaseValues.cliCity != clientValues.cliCity)
                            ModelState.AddModelError("cliCity", "Current value: "
                                + databaseValues.cliCity);
                        if (databaseValues.Province != clientValues.Province)
                            ModelState.AddModelError("Province", "Current value: "
                                + databaseValues.Province);
                        
                        
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you received your values. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to save your version of this record, click "
                                + "the Save button again. Otherwise click the 'Back to List' hyperlink.");
                        client.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
                 
            }
                
            
            return View(client);
            //    try
            //    {
            //        if (ModelState.IsValid)
            //        {
            //            _context.Clients.Update(client);
            //            _context.SaveChanges();
            //            return RedirectToAction(nameof(IndexAsync));
            //        }
            //    }
            //    catch (RetryLimitExceededException /* dex */)
            //    {
            //        ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            //    }
            //    catch (DbUpdateException dex)
            //    {
            //        if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
            //        {
            //            ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate addresses.");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            //        }
            //    }

            //    return View(client);
            //}

        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.cliID == id);
        }

        
    }
}

