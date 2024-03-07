using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Speisekarte.Models
{
    public class Zutat
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Beschreibung { get; set; } = string.Empty;

        //Eingabefeld weiß über min, max Bescheid 
        [MaxLength(5), MinLength(2, ErrorMessage="Minimum Length must be 2")]
        public string Einheit { get; set; } = string.Empty;

        public decimal Menge { get; set; }

        //Verlinkung zu Elternelement 

        [JsonIgnore]
        public Speise? Speise { get; set; }

        //Foreign Key soll nicht serialisiert werden
        [JsonIgnore]
        public int? SpeiseId { get; set; }

        public override string ToString()
        {
            return $"{Menge} {Einheit} {Beschreibung}";
        }

    }
}
