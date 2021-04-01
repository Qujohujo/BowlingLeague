using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(long? teamid, string? teamname, int pageNum = 0) 
        {
            int pageSize = 5;

            //set up the data that the view model 
            return View(new IndexViewModel
            {
                //after information has been passed through the url to the action, create a sql query for team members based on team id
                Bowlers = context.Bowlers
                    .FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamid} OR {teamid} IS NULL")
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    //if no team selected, count all - if there is a team selected, count only for that team
                    TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(b => b.TeamId == teamid).Count())
                },
                TeamName = teamname
            });

            //return View(context.Bowlers
            //    .FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamid} OR {teamid} IS NULL")
            //    .ToList()
            //    );
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
