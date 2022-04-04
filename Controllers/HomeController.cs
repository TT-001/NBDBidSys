using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NBD_TractionFive.Data;
using NBD_TractionFive.Models;
using NBD_TractionFive.ViewModels;

namespace NBD_TractionFive.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly NBDContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(NBDContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public IActionResult Index()
        {
            var user = _httpContextAccessor.HttpContext.User.Identity.Name;
            var bids = new List<Bid>();

            ViewData["name"] = user;
            ViewData["Filtering"] = "";

            if (User.IsInRole("Designer"))
            {
                bids = _context.Bids.Include(b => b.Designer)
                                    //.Include(b => b.BidState)
                                    .Include(b => b.Project)
                                    .Where(b => b.Designer.eMail.ToUpper().Contains(user.ToUpper()) || b.Designer.empFirst.ToUpper().Contains(user.ToUpper())).ToList();

                return View("IndexDesigner", bids);
            }
            else if (User.IsInRole("Sales"))
            {
                bids = _context.Bids.Include(b => b.SalesAssociate)
                                    //.Include(b => b.BidState)
                                    .Include(b => b.Project)
                                    .ThenInclude(b => b.Client)
                                    .Where(b => b.SalesAssociate.eMail.ToUpper().Contains(user.ToUpper()) || b.Designer.empFirst.ToUpper().Contains(user.ToUpper())).ToList();

                return View("IndexSales", bids);
            }
            else if (User.IsInRole("Management"))
            {
                bids = _context.Bids.Include(b => b.SalesAssociate)
                                    //.Include(b => b.BidState)
                                    .Include(b => b.Project)
                                    .ThenInclude(b => b.Client)
                                    .Where(b => b.BidState == BidState.Waiting_Management).ToList();

                

                return View("IndexManager", bids);
            }
            return View();
                
        }

        public IEnumerable<Bid> GetDate(int month)
        {
            return _context.Bids
                .Where(d => d.bidDate.Month == month);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
