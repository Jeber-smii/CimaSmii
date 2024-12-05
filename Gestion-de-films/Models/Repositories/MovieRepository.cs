using Microsoft.EntityFrameworkCore;

namespace Gestion_de_films.Models.Repositories
{
    public class MovieRepository : IMovieRepository<Movie>
    {
        readonly MovieContext context;
        public MovieRepository( MovieContext context )
        {
            this.context = context;
        }

        public IList<Movie> GetAll()
        {
            return context.Movies.OrderBy(x=>x.Title).Include(x => x.room).ToList();
        }

        public Movie GetById(int id)
        {
            return context.Movies.Where(x => x.MovieID == id).Include(x => x.room).SingleOrDefault();
        }

        public void Add(Movie film)
        {
            context.Movies.Add(film);
            context.SaveChanges();
        }
        public void Edit(Movie movie)
        {
            Movie m = context.Movies.Find(movie.MovieID);
            if (m != null)
            {
                m.Title = movie.Title;
                m.Subject = movie.Subject;
                m.Genre = movie.Genre;
                m.Hero = movie.Hero;
                m.RoomID = movie.RoomID;
                m.ShowTime = movie.ShowTime;
                m.ReleaseDate = movie.ReleaseDate;
                m.MovieDuration = movie.MovieDuration;
                m.NbPlacesDispo=movie.NbPlacesDispo;
                m.Poster= movie.Poster;
                
                context.SaveChanges();
            }
        }
        public void Delete(Movie movie)
        {
            Movie m = context.Movies.Find(movie.MovieID);
            if (m != null)
            {
                context.Movies.Remove(m);
                context.SaveChanges() ;
            }
        }
        public IList<Movie> GetMovieByRoomID(int? roomid)
        {
            return context.Movies.Where(f => 
            f.RoomID.Equals(roomid))
                .OrderBy(m => m.Title)
                .Include(mv => mv.room).ToList();
        }
        public IList<Movie> FindByTitle(string title)
        {
            return context.Movies.Where(m =>
            m.Title.Contains(title))
                .Include(mv => mv.room).ToList();
        }

    }
}
