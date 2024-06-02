using ReservarionWebAPI.Models;

namespace ReservarionWebAPI.Repositories
{
    public class Repository : IRepository
    {
        private readonly Dictionary<int, Reservation> items;

        public Repository()
        {
            items = new Dictionary<int, Reservation>();
            new List<Reservation>
            {
                new Reservation{Id = 1, StartLocation = "New York", EndLocation = "Beijing", Name="Steven"},
                new Reservation{Id = 2, StartLocation = "New Jersey", EndLocation = "Boston", Name="John"},
                new Reservation{Id = 3, StartLocation = "London", EndLocation = "Paris",Name="Martin"}
            }.ForEach(r => AddReservation(r));
        }

        public Reservation? this[int id] => items.ContainsKey(id) ? items[id] : null;

        public IEnumerable<Reservation> Reservations => items.Values;

        public Reservation AddReservation(Reservation reservation)
        {
            if (reservation.Id == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key)) { key++; }
                reservation.Id = key;
            }
            items[reservation.Id] = reservation;
            return reservation;
        }

        public void DeleteReservation(int id) => items.Remove(id);

        public Reservation UpdateReservation(Reservation reservation) => AddReservation(reservation);
    }
}
