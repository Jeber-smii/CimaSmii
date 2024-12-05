using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gestion_de_films.Models
{
    public class Movie
    {
        public int MovieID { get; set; }

   
        public string Title { get; set; }

        public string Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public DateTime ShowTime { get; set; }
        public string Subject { get; set; }

        public String Hero {  get; set; }

        public int RoomID { get; set; }
        public Room room { get; set; }

        public string MovieDuration { get; set; }
        public int NbPlacesDispo {  get; set; }
        public string Poster { get; set; }

    }
}
