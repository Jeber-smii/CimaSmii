using Microsoft.EntityFrameworkCore;

namespace Gestion_de_films.Models.Repositories
{
    public class RoomRepository : IRoomRepository <Room>
    {
        readonly MovieContext context;
        public RoomRepository( MovieContext context )
        {
            this.context = context;
        }
        public IList<Room> GetAll()
        {
            return context.Rooms.OrderBy(s => s.Name).ToList();
        }

        public Room GetById(int id)
        {
            return context.Rooms.Find(id);
        }

        public void add(Room s)
        {
            context.Rooms.Add(s);
            context.SaveChanges();
        }
        public void Edit(Room s)
        {
            Room room = context.Rooms.Find(s.RoomID);
            if (room != null)
            {
                room.Name = s.Name;
                room.NbPlaces = s.NbPlaces;
                context.SaveChanges();
            }
        }
        public void Delete(Room salle)
        {
            Room s = context.Rooms.Find(salle.RoomID);
            if(s != null)
            {
                context.Rooms.Remove(s);
                context.SaveChanges();                                                                       
            }

        }

        public int MovieCount(int roomid)
        {
            return context.Movies.Where(f => f.RoomID == roomid).Count();
        }


    }
}
