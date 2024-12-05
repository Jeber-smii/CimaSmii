namespace Gestion_de_films.Models.Repositories
{
    public interface IMovieRepository <Movie>
    {
        IList<Movie> GetAll();

        Movie GetById(int id);

        void Add(Movie movie);

        void Edit(Movie movie);

        void Delete(Movie movie);

        IList<Movie> GetMovieByRoomID(int? roomid);
        IList<Movie> FindByTitle(string title);
    }
}
