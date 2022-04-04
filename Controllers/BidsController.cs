using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBD_TractionFive.Data;
using NBD_TractionFive.Models;
using NBD_TractionFive.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using NBD_TractionFive.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace NBD_TractionFive.Controllers
{
    [Authorize]
    public class BidsController : Controller
    {
        private readonly NBDContext _context;

        public BidsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Bids/Index
        public async Task<IActionResult> IndexAsync(string SearchString, int? SalesID, int? DesignerID, string actionButton, int? page, int? pageSizeID, string sortDirection = "asc", string sortField = "Bid Site")
        {
            var bids = from b in _context.Bids
                       //.Include(b => b.BidState)
                       .Include(b => b.Project).ThenInclude(b => b.Client)
                       .Include(b => b.SalesAssociate)
                       .Include(b => b.Designer)
                       .Include(b => b.BidLabours).ThenInclude(b => b.Labour)
                       .Include(b => b.BidMaterials).ThenInclude(b => b.Inventory).ThenInclude(b => b.InvType)
                       select b;

            var Sales = (from s in _context.Employees.Where(p => p.empType == "Sales Associate")
                        select s).ToList();

            var designers = (from d in _context.Employees.Where(p => p.empType == "Designer")
                        select d).ToList();

            ViewData["SalesID"] = new SelectList(Sales, "empID", "empFullName");
            ViewData["DesignerID"] = new SelectList(designers, "empID", "empFullName");
            ViewData["Filtering"] = "";  //Assume not filtering

            if (!String.IsNullOrEmpty(SearchString))
            {
                bids = bids.Where(b => b.Project.projSite.ToUpper().Contains(SearchString.ToUpper()));

                ViewData["Filtering"] = " show";
            }
            if (SalesID.HasValue)
            {
                bids = bids.Where(p => p.SalesID == SalesID);
                ViewData["Filtering"] = " show";
            }
            if (DesignerID.HasValue)
            {
                bids = bids.Where(p => p.DesignerID == DesignerID);
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
            if (sortField == "Bid Site")//Sorting by Bid Site
            {
                if (sortDirection == "asc")
                {
                    bids = bids
                        .OrderBy(b => b.Project.projSite);
                }
                else
                {
                    bids = bids
                        .OrderByDescending(b => b.Project.projSite);
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
            var pagedData = await PaginatedList<Bid>.CreateAsync(bids.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                //.Include(b => b.BidState)
                .Include(b => b.Project).ThenInclude(b => b.Client)
                .Include(b => b.SalesAssociate)
                .Include(b => b.Designer)
                .Include(b => b.BidLabours).ThenInclude(b => b.Labour)
                .Include(b => b.BidMaterials).ThenInclude(b => b.Inventory).ThenInclude(b => b.InvType)
                .FirstOrDefaultAsync(m => m.bidID == id);

            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // GET: Bids/Create
        [Authorize(Roles = "Admin, Management, Designer")]
        public IActionResult Create(int? bidID, DateTime? bidDate, int? clientID, int? salesID, int? designerID, BidState? bidState, int? projID, HashSet<BidLabour>? selectedLabours, HashSet<BidMaterial>? selectedMaterials)
        {
            var bid = new Bid();


            bid.bidDate = DateTime.Now;
            //ViewData["bidStateID"] = new SelectList(_context.BidStates, "bidStateID", "status");
            ViewData["projID"] = new SelectList(_context.Projects, "projID", "projSite");
            ViewData["salesID"] = new SelectList(_context.Employees.Where(e => e.empType == "Sales Associate").ToList(), "empID", "empFullName");
            ViewData["designerID"] = new SelectList(_context.Employees.Where(e => e.empType == "Designer").ToList(), "empID", "empFullName");
            PopulateAssignedLabourData(bid);
            PopulateAssignedMaterialData(bid);
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Management, Designer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("bidID,bidDate,BidState,projID,SalesID,DesignerID")] Bid bid, AssignedLabourVM[] labCheckboxes, string[] selectedLabours, AssignedMaterialVM[] invCheckboxes, string[] selectedMaterials)
        {
            try
            {
                decimal bAmount = 0;
                foreach (var labour in _context.Labours)
                {
                    for (int i = 0; i < labCheckboxes.Count(); i++)
                    {
                        if (labCheckboxes[i].ID == labour.labID)
                        {
                            if (labCheckboxes[i].assigned)
                            {
                                var newBidLab = new BidLabour
                                {
                                    bidID = bid.bidID,
                                    labID = labour.labID,
                                    blHours = labCheckboxes[i].displayNumber,
                                    blCost = labCheckboxes[i].displayNumber * labour.labPrice
                                };

                                bid.BidLabours.Add(newBidLab);
                                bAmount += Convert.ToDecimal(newBidLab.blCost);
                            }
                        }
                    }
                }
                foreach (var inventory in _context.Inventories)
                {
                    for (int i = 0; i < invCheckboxes.Count(); i++)
                    {
                        if (invCheckboxes[i].ID == inventory.invID)
                        {
                            if (invCheckboxes[i].assigned)
                            {
                                var newBidMat = new BidMaterial
                                {
                                    bidID = bid.bidID,
                                    invID = inventory.invID,
                                    bmQuantity = Convert.ToInt32(invCheckboxes[i].displayNumber),
                                    bmCost = invCheckboxes[i].displayNumber * inventory.invPrice
                                    
                                };

                                bid.BidMaterials.Add(newBidMat);
                                bAmount += Convert.ToDecimal(newBidMat.bmCost);
                            }
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    _context.Add(bid);
                    bid.bidAmount = bAmount;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unnknown error!");
            }

            //ViewData["bidStateID"] = new SelectList(_context.BidStates, "bidStateID", "status", bid.bidStateID);
            ViewData["projID"] = new SelectList(_context.Projects, "projID", "projSite", bid.projID);
            ViewData["salesID"] = new SelectList(_context.Employees.Where(e => e.empType == "Sales Associate").ToList(), "empID", "empFullName", bid.SalesID);
            ViewData["designerID"] = new SelectList(_context.Employees.Where(e => e.empType == "Designer").ToList(), "empID", "empFullName", bid.DesignerID);
            ViewBag.emp = _context.Bids.Select(a => a.SalesAssociate.empFullName);

            PopulateAssignedLabourData(bid);
            PopulateAssignedMaterialData(bid);
            return View(bid);
        }

        // GET: Bids/Edit/5
        [Authorize(Roles = "Admin, Management, Designer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                //.Include(b => b.BidState)
                .Include(b => b.Project).ThenInclude(b => b.Client)
                .Include(b => b.SalesAssociate)
                .Include(b => b.Designer)
                .Include(b => b.BidLabours).ThenInclude(b => b.Labour)
                .Include(b => b.BidMaterials).ThenInclude(b => b.Inventory).ThenInclude(b => b.InvType)
                .FirstOrDefaultAsync(m => m.bidID == id);
            if (bid == null)
            {
                return NotFound();
            }
            //ViewData["bidStateID"] = new SelectList(_context.BidStates, "bidStateID", "status", bid.bidStateID);
            ViewData["projID"] = new SelectList(_context.Projects, "projID", "projSite", bid.projID);
            ViewData["salesID"] = new SelectList(_context.Employees.Where(e => e.empType == "Sales Associate").ToList(), "empID", "empFullName", bid.SalesID);
            ViewData["designerID"] = new SelectList(_context.Employees.Where(e => e.empType == "Designer").ToList(), "empID", "empFullName", bid.DesignerID);
            PopulateAssignedLabourData(bid);
            PopulateAssignedMaterialData(bid);
            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Management, Designer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssignedLabourVM[] labCheckboxes, AssignedMaterialVM[] invCheckboxes,
            [Bind("bidID,bidDate,bidAmount,BidState,projID,SalesID,DesignerID")] Bid bid, Byte[] RowVersion, string editing)
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Bids");

            var bidToUpdate = await _context.Bids
                //.Include(b => b.BidState)
                .Include(b => b.Project).ThenInclude(b => b.Client)
                .Include(b => b.SalesAssociate)
                .Include(b => b.Designer)
                .Include(b => b.BidLabours).ThenInclude(b => b.Labour)
                .Include(b => b.BidMaterials).ThenInclude(b => b.Inventory).ThenInclude(b => b.InvType)
                .FirstOrDefaultAsync(m => m.bidID == id);

            if (bidToUpdate == null)
            {
                return NotFound();
            }

            UpdateBidLabours(labCheckboxes, bidToUpdate);
            UpdateBidMaterials(invCheckboxes, bidToUpdate);
            bidToUpdate.bidAmount = Convert.ToDecimal(_context.BidLabours.Where(x => x.bidID == bidToUpdate.bidID).Sum(x => x.blCost))
                                    + Convert.ToDecimal(_context.BidMaterials.Where(x => x.bidID == bidToUpdate.bidID).Sum(x => x.bmCost));


            _context.Entry(bidToUpdate).Property("RowVersion").OriginalValue = RowVersion;

            if (await TryUpdateModelAsync<Bid>(bidToUpdate, "",
                b => b.bidDate, b => b.BidState, b => b.projID, b => b.SalesID, b => b.DesignerID))
            {
                try
                {
                    switch (editing)
                    {
                        case "Save Changes":
                            // try
                            //  {
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        // }
                        case "Create New Bid from Template":
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Create", "Bids", new { bidID = bid.bidID, bidDate = DateTime.Now, salesID = bid.SalesID, designerID = bid.DesignerID, bidState = bid.BidState, projID = bid.projID, selectedLabours = bid.BidLabours, selectedMaterials = bid.BidMaterials });

                    }
                }

                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Bid)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Bid was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Bid)databaseEntry.ToObject();

                        if (databaseValues.bidDate != clientValues.bidDate)
                            ModelState.AddModelError("bidDate", "Current value: "
                                + String.Format("{0:d}", databaseValues.bidDate));
                        if (databaseValues.BidState != clientValues.BidState)
                            ModelState.AddModelError("BidState", "Current value: "
                                + databaseValues.BidState);
                        if (databaseValues.bidAmount != clientValues.bidAmount)
                            ModelState.AddModelError("bidAmount", "Current value: "
                                + databaseValues.bidAmount);

                        //For the foreign key, we need to go to the database to get the information to show
                        if (databaseValues.projID != clientValues.projID)
                        {
                            Project databaseDoctor = await _context.Projects.SingleOrDefaultAsync(i => i.projID == databaseValues.projID);
                            ModelState.AddModelError("projID", $"Current value: {databaseDoctor?.projSite}");
                        }
                        if (databaseValues.SalesID != clientValues.SalesID)
                        {
                            Employee databaseDoctor = await _context.Employees.SingleOrDefaultAsync(i => i.empID == databaseValues.SalesID);
                            ModelState.AddModelError("SalesID", $"Current value: {databaseDoctor?.empFullName}");
                        }
                        if (databaseValues.DesignerID != clientValues.DesignerID)
                        {
                            Employee databaseDoctor = await _context.Employees.SingleOrDefaultAsync(i => i.empID == databaseValues.DesignerID);
                            ModelState.AddModelError("DesignerID", $"Current value: {databaseDoctor?.empFullName}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you received your values. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to save your version of this record, click "
                                + "the Save button again. Otherwise click the 'Back to List' hyperlink.");
                        bidToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
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

            //ViewData["bidStateID"] = new SelectList(_context.BidStates, "bidStateID", "status", bid.bidStateID);
            ViewData["projID"] = new SelectList(_context.Projects, "projID", "projSite", bid.projID);
            ViewData["salesID"] = new SelectList(_context.Employees.Where(e => e.empType == "Sales Associate").ToList(), "empID", "empFullName", bid.SalesID);
            ViewData["designerID"] = new SelectList(_context.Employees.Where(e => e.empType == "Designer").ToList(), "empID", "empFullName", bid.DesignerID);
            PopulateAssignedLabourData(bidToUpdate);
            PopulateAssignedMaterialData(bidToUpdate);
            return View(bidToUpdate);
        }

        private void PopulateAssignedLabourData(Bid bid)
        {
            var allLabours = _context.Labours;
            var bLabours = new HashSet<int>(bid.BidLabours.Select(b => b.labID));
            var checkBoxes = new List<AssignedLabourVM>();

            foreach (var labour in allLabours)
            {
                BidLabour bl = labour.BidLabours.Where(x => x.labID == labour.labID && x.bidID == bid.bidID).FirstOrDefault();
                checkBoxes.Add(new AssignedLabourVM
                {
                    ID = labour.labID,
                    displayText = labour.labDescription,
                    assigned = bLabours.Contains(labour.labID),
                    displayNumber = bl == null ? 0 : bl.blHours
                });
            }
            ViewData["Labours"] = checkBoxes;
        }

        private void UpdateBidLabours(AssignedLabourVM[] labCheckboxes, Bid bidToUpdate)
        {
            if (labCheckboxes == null)
            {
                bidToUpdate.BidLabours = new List<BidLabour>();
                return;
            }

            var bidLaboursHS = new HashSet<int>(bidToUpdate.BidLabours.Select(b => b.labID));
            foreach (var labour in _context.Labours)
            {
                for (int i = 0; i < labCheckboxes.Count(); i++)
                {
                    if (labCheckboxes[i].ID == labour.labID)
                    {
                        if (labCheckboxes[i].assigned)
                        {
                            BidLabour labourToUpdate = bidToUpdate.BidLabours.SingleOrDefault(b => b.labID == labour.labID);
                            if (labourToUpdate == null)
                            {
                                _context.Add(new BidLabour { bidID = bidToUpdate.bidID, labID = labour.labID, blHours = labCheckboxes[i].displayNumber, blCost = labCheckboxes[i].displayNumber * labour.labPrice });
                            }
                            else
                            {
                                labourToUpdate.blHours = labCheckboxes[i].displayNumber;
                                labourToUpdate.blCost = labCheckboxes[i].displayNumber * labour.labPrice;
                                _context.Update(labourToUpdate);
                            }
                        }
                        else
                        {
                            BidLabour labourToRemove = bidToUpdate.BidLabours.SingleOrDefault(b => b.labID == labour.labID);
                            if (labourToRemove != null)
                            {
                                _context.Remove(labourToRemove);
                            }
                        }
                    }
                }
            }

        }

        private void PopulateAssignedMaterialData(Bid bid)
        {
            var allInventories = _context.Inventories;
            var bInventories = new HashSet<int>(bid.BidMaterials.Select(b => b.invID));
            var checkBoxes = new List<AssignedMaterialVM>();

            foreach (var inventories in allInventories)
            {
                BidMaterial bm = inventories.BidMaterials.Where(x => x.invID == inventories.invID && x.bidID == bid.bidID).FirstOrDefault();
                checkBoxes.Add(new AssignedMaterialVM
                {
                    ID = inventories.invID,
                    displayText = inventories.invName,
                    assigned = bInventories.Contains(inventories.invID),
                    price = inventories.invPrice,
                    displayNumber = bm == null ? 0 : bm.bmQuantity
                });
            }
            ViewData["Inventories"] = checkBoxes;
        }


        private void UpdateBidMaterials(AssignedMaterialVM[] invCheckboxes, Bid bidToUpdate)
        {
            if (invCheckboxes == null)
            {
                bidToUpdate.BidMaterials = new List<BidMaterial>();
                return;
            }

            var bidMaterialsHS = new HashSet<int>(bidToUpdate.BidMaterials.Select(b => b.invID));
            foreach (var inventory in _context.Inventories)
            {
                for (int i = 0; i < invCheckboxes.Count(); i++)
                {
                    if (invCheckboxes[i].ID == inventory.invID)
                    {
                        if (invCheckboxes[i].assigned)
                        {
                            BidMaterial materialToUpdate = bidToUpdate.BidMaterials.SingleOrDefault(b => b.invID == inventory.invID);
                            if (materialToUpdate == null)
                            {
                                _context.Add(new BidMaterial { bidID = bidToUpdate.bidID, invID = inventory.invID, bmQuantity = Convert.ToInt32(invCheckboxes[i].displayNumber), bmCost = invCheckboxes[i].displayNumber * inventory.invPrice });
                            }
                            else
                            {
                                materialToUpdate.bmQuantity = Convert.ToInt32(invCheckboxes[i].displayNumber);
                                materialToUpdate.bmCost = invCheckboxes[i].displayNumber * inventory.invPrice;
                                _context.Update(materialToUpdate);
                            }
                        }
                        else
                        {
                            BidMaterial materialToRemove = bidToUpdate.BidMaterials.SingleOrDefault(b => b.invID == inventory.invID);
                            if (materialToRemove != null)
                            {
                                _context.Remove(materialToRemove);
                            }
                        }
                    }
                }
            }
        }


        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.bidID == id);
        }
    }
}
