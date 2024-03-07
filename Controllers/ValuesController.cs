using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speisekarte.Data;
using Speisekarte.Models;

namespace Speisekarte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Fill")]
        public void FillData()
        {
            Speise s1 = new Speise{Titel = "Hauptspeise",Notizen = "Meine Notizen",Sterne = 3};
            Zutat z1 = new Zutat{Beschreibung = "Mehl",Einheit = "g",Menge = 200};
            Zutat z2 = new Zutat{ Beschreibung = "Zucker",Einheit = "g", Menge = 100};
            Zutat z3 = new Zutat { Beschreibung = "Öl",Einheit = "ml",Menge = 200};

            s1.Zutaten.Add(z1);
            s1.Zutaten.Add(z2);
            s1.Zutaten.Add(z3);



            Speise s2 = new Speise { Titel = "Schnitzel", Notizen = "Meine Notizen", Sterne = 25 };
            z1 = new Zutat { Beschreibung = "Mehl", Einheit = "g", Menge = 200 };
            z2 = new Zutat { Beschreibung = "Eier", Einheit = "Anzahl", Menge = 3 };
            z3 = new Zutat { Beschreibung = "Öl", Einheit = "ml", Menge = 200 };

            s2.Zutaten.Add(z1);
            s2.Zutaten.Add(z2);
            s2.Zutaten.Add(z3);

            _context.Speisen.Add(s1);
            _context.Speisen.Add(s2);
            _context.SaveChanges();
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Speise> GetSpeisen()
        {
            var speisen = _context.Speisen
                .Include(speise => speise.Zutaten)
                .ToList();
            return speisen;
        }

        [HttpGet]
        [Route("GetTop")]
        public List<Top> GetTopSpeisen()
        {
            var speisen = _context.Speisen
                    .OrderByDescending(speise => speise.Sterne)
                    .Take(3)
                    .ToList();

            List<Top> top = new List<Top>();
            foreach(Speise speise in speisen)
            {
                Top instance = new Top();
                instance.Titel = speise.Titel;
                instance.Sterne = speise.Sterne;
                top.Add(instance);

            }
            return top;
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.Speisen == null)
            {
                return NotFound();
            }
            var speise = await _context.Speisen
                .Include(speise => speise.Zutaten)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(speise == null)
            {
                return NotFound();
            }

            foreach(Zutat zutat in speise.Zutaten)
            {
                _context.Zutaten.Remove(zutat);
            }

            _context.Speisen.Remove(speise);
            await _context.SaveChangesAsync();
            return NoContent();



        }
    }
}
