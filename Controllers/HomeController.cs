using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Speisekarte.Data;
using Speisekarte.Models;
using System.Diagnostics;

namespace Speisekarte.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Informationen für eine Drop-Down-Liste bereitstellen
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            SelectListItem eintrag0 = new SelectListItem("Very Good", "5");
            selectListItems.Add(eintrag0);
            SelectListItem eintrag = new SelectListItem("Good", "4");
            selectListItems.Add(eintrag);
            SelectListItem eintrag2 = new SelectListItem("Medium", "3");
            selectListItems.Add(eintrag2);
            SelectListItem eintrag3 = new SelectListItem("Not Good", "2");
            selectListItems.Add(eintrag3);
            SelectListItem eintrag4 = new SelectListItem("Awful", "1");
            selectListItems.Add(eintrag4);
            // Die Übergabe erfolgt über das ViewData-Dictionary
            ViewData["Kategorien"] = selectListItems;
            //Wenn != null dann wird view zurückgegeben sonst Problem Darstellungen 
            return _context.Speisen != null ? View(await _context.Speisen.Include(speise => speise.Zutaten).ToListAsync()) :
                Problem("Entity set ApplicationDBContext.Speisen is null");

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

        public IActionResult AddZutatForm(int? id)
        {
            if (id is not null && id > 0)
            {
                var speiseFromDb = _context.Speisen.FirstOrDefault(s => s.Id == id);
                if (speiseFromDb != null)
                {
                    Zutat zutat = new Zutat();
                    zutat.SpeiseId = id;
                    return View(zutat);
                }
            }
            return View();
        }

        public IActionResult addZutat(Zutat zutat)
        {
            if(zutat.SpeiseId != null && zutat.SpeiseId > 0)
            {
                Speise? speiseFromDb = _context.Speisen.FirstOrDefault(x => x.Id == zutat.SpeiseId);
                if(speiseFromDb != null)
                {
                    speiseFromDb.Zutaten.Add(zutat);
                    _context.SaveChanges();
                }

            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CreateSpeiseForm()
        {
            // Informationen für eine Drop-Down-Liste bereitstellen
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            SelectListItem eintrag0 = new SelectListItem("Very Good", "5");
            selectListItems.Add(eintrag0);
            SelectListItem eintrag = new SelectListItem("Good", "4");
            selectListItems.Add(eintrag);
            SelectListItem eintrag2 = new SelectListItem("Medium", "3");
            selectListItems.Add(eintrag2);
            SelectListItem eintrag3 = new SelectListItem("Not Good", "2");
            selectListItems.Add(eintrag3);
            SelectListItem eintrag4 = new SelectListItem("Awful", "1");
            selectListItems.Add(eintrag4);
            // Die Übergabe erfolgt über das ViewData-Dictionary
            ViewData["Kategorien"] = selectListItems;
            //Wenn != null dann wird view zurückgegeben sonst Problem Darstellungen 
            return View();

        }
        public IActionResult CreateSpeise(Speise speise)
        {
            if (speise is not null)
            {
                _context.Speisen.Add(speise);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}