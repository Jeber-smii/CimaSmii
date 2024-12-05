namespace Gestion_de_films.Models.Repositories
{
    public interface IRoomRepository <Room>
    {
        IList<Room> GetAll();

        Room GetById(int id);

        void add(Room s);

        void Edit(Room s);

        void Delete (Room salle);

        int MovieCount(int salleID);
    }
}
