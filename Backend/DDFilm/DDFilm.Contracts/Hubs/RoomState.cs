using System.Collections.Concurrent;

namespace DDFilm.Contracts.Hubs
{
    public class RoomState
    {
        public string? LecturerId { get; set; }
        public ConcurrentDictionary<string, bool> Viewers { get; set; } = new ConcurrentDictionary<string, bool>();
    }
}
