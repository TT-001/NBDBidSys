using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NBD_TractionFive.Data;
using Microsoft.EntityFrameworkCore;
using NBD_TractionFive.Models;
using Microsoft.EntityFrameworkCore.Storage;
using NBD_TractionFive.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.AspNetCore.Authorization;

namespace NBD_TractionFive.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly NBDContext _context;

        public ProjectController(NBDContext context)
        {
            _context = context;
        }
        // GET: Project/Index
        public async Task<IActionResult> IndexAsync(string SearchString, int? province, string actionButton, int? page, int? pageSizeID, string sortDirection = "asc", string sortField = "Project Site")
        {
            var dQuery = (from d in _context.Projects.Include(p => p.Client)
                          orderby d.projID, d.projBeginDate
                          select d).ToList();

            var projects = from a in _context.Projects
               .Include(a => a.Client)
                      select a;

            if (!String.IsNullOrEmpty(SearchString))
            {
                projects = projects.Where(a => a.Client.cliName.ToUpper().Contains(SearchString.ToUpper()));

                ViewData["Filtering"] = " show";
            }

            if (province.HasValue)
            {
                projects = projects.Where(p => (int)p.Province == province);
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
            if (sortField == "Project Site")//Sorting by Client Name
            {
                if (sortDirection == "asc")
                {
                    projects = projects
                        .OrderBy(p => p.projSite);
                }
                else
                {
                    projects = projects
                        .OrderByDescending(p => p.projSite);
                }
            }
            else if (sortField == "Begin Date")//Sorting by Client Name
            {
                if (sortDirection == "asc")
                {
                    projects = projects
                        .OrderBy(p => p.projBeginDate);
                }
                else
                {
                    projects = projects
                        .OrderByDescending(p => p.projBeginDate);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            //Handle Paging
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
            pageSize = (pageSize == 0) ? 5 : pageSize;//Neither Selected or in Cookie so go with default
            ViewData["pageSizeID"] =
                new SelectList(new[] { "3", "5", "10", "20", "30", "40", "50", "100", "500" }, pageSize.ToString());
            var pagedData = await PaginatedList<Project>.CreateAsync(projects.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);


            //int pageSize;//This is the value we will pass to PaginatedList
            //if (pageSizeID.HasValue)
            //{
            //    //Value selected from DDL so use and save it to Cookie
            //    pageSize = pageSizeID.GetValueOrDefault();
            //    CookieHelper.CookieSet(HttpContext, "pageSizeValue", pageSize.ToString(), 30);
            //}
            //else
            //{
            //    //Not selected so see if it is in Cookie
            //    pageSize = Convert.ToInt32(HttpContext.Request.Cookies["pageSizeValue"]);
            //}
            //pageSize = (pageSize == 0) ? 3 : pageSize;//Neither Selected or in Cookie so go with default
            //ViewData["pageSizeID"] =
            //    new SelectList(new[] { "3", "5", "10", "20", "30", "40", "50", "100", "500" }, pageSize.ToString());
            //var pagedData = await PaginatedList<Project>.CreateAsync(projects.AsNoTracking(), page ?? 1, pageSize);

            //return View(pagedData);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.projID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }


        [Authorize(Roles = "Admin, Management, Designer, Sales")]
        public IActionResult Create(int? clientID)
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Project");
            //Add all (unchecked) Conditions to ViewBag
            var project = new Project();
            Project p = new Project()
            {
                cliID = clientID.GetValueOrDefault(),
                projBeginDate = DateTime.Today,
                projCompletionDate = null
            };
            PopulateDropDownLists();
            return View(p);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Management, Designer, Sales")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("projBeginDate,projCompletionDate,projSite,projCity,Province,ZipCode,projID,cliID")] Project project, string creating)
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Project");
            if (ModelState.IsValid)
            {
                
                switch (creating)
                {
                    case "Create and Add Bid":
                        _context.Add(project);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Create", "Bids", new { projectID = project.projID });
                    case "Create":
                        _context.Add(project);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                }
                    
                
            }
            PopulateDropDownLists(project);
            return View(project);
        }


        //GET: Project/Edit
        [Authorize(Roles = "Admin, Management, Designer, Sales")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Project");
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.AsNoTracking().SingleOrDefaultAsync(p => p.projID == id);
            if (project == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(project);
            return View(project);
        }
        

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Management, Designer, Sales")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("projBeginDate,projCompletionDate,projSite,projCity,Province,ZipCode,projID")] Project project, Byte[] RowVersion)
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Project");

            var projectToUpdate = await _context.Projects
                .Include(p => p.Client)
                .SingleOrDefaultAsync(p => p.projID == id);
            //Check that you got it or exit with a not found error
            if (projectToUpdate == null)
            {
                return NotFound();
            }

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Projects");


            _context.Entry(projectToUpdate).Property("RowVersion").OriginalValue = RowVersion;

            if (await TryUpdateModelAsync<Project>(projectToUpdate, "",
                d => d.projBeginDate, d => d.projCompletionDate, d => d.projSite, d => d.projCity, d => d.Province, d => d.ZipCode, d => d.cliID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Project)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Project was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Project)databaseEntry.ToObject();
                        if (databaseValues.projBeginDate != clientValues.projBeginDate)
                            ModelState.AddModelError("projBeginDate", "Current value: "
                                + String.Format("{0:d}", databaseValues.projBeginDate));
                        if (databaseValues.projCompletionDate != clientValues.projCompletionDate)
                            ModelState.AddModelError("projCompletionDate", "Current value: "
                                + String.Format("{0:d}", databaseValues.projCompletionDate));
                        if (databaseValues.projSite != clientValues.projSite)
                            ModelState.AddModelError("projSite", "Current value: "
                                + databaseValues.projSite);
                        if (databaseValues.projCity != clientValues.projCity)
                            ModelState.AddModelError("projCity", "Current value: "
                                + databaseValues.projCity);
                        if (databaseValues.Province != clientValues.Province)
                            ModelState.AddModelError("Province", "Current value: "
                                + databaseValues.Province);
                        if (databaseValues.ZipCode != clientValues.ZipCode)
                            ModelState.AddModelError("ZipCode", "Current value: "
                                + databaseValues.ZipCode);


                        //For the foreign key, we need to go to the database to get the information to show
                        if (databaseValues.cliID != clientValues.cliID)
                        {
                            Client databaseDoctor = await _context.Clients.SingleOrDefaultAsync(i => i.cliID == databaseValues.cliID);
                            ModelState.AddModelError("cliID", $"Current value: {databaseDoctor?.cliName}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you received your values. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to save your version of this record, click "
                                + "the Save button again. Otherwise click the 'Back to List' hyperlink.");
                        projectToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                    {
                        ModelState.AddModelError("Name", "Unable to save changes. Remember, you cannot have duplicate position names.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }
            PopulateDropDownLists(projectToUpdate);
            return View(projectToUpdate);

            //var projectToUpdate = await _context.Projects.AsNoTracking()
            //    .SingleOrDefaultAsync(p => p.projID == id);
            //if (projectToUpdate == null)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(project);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProjectExists(project.projID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //PopulateDropDownLists(project);
            //return View(project);
            //public IActionResult Edit(Project project)
            //{
            //    try
            //    {
            //        if (ModelState.IsValid)
            //        {
            //            _context.Projects.Update(project);
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

            //    return View(project);

            //}
        }

        private void PopulateDropDownLists(Project project = null)
        {
            ViewData["ProjectID"] = ProjectSelectList(project?.cliID);
        }
        private SelectList ProjectSelectList(int? id)
        {
            var dQuery = from d in _context.Clients
                         orderby d.cliID
                         select d;
            return new SelectList(dQuery, "cliID", "cliName", id);
        }
        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.projID == id);
        }
    }
}
