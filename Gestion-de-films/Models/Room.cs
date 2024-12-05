using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gestion_de_films.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public int NbPlaces { get; set; }

        public ICollection<Movie> Movies { get; set; }

    }
}
