using Microsoft.Extensions.Primitives;

namespace MovieTimeStreaming.Models;

public class WatchHistory
{
    [Key]
    public int ID { get; set; }
    public string UserId { get; set; }
    public string MediaId { get; set; }
    public double StopTime { get; set; } = 0;
    public bool Watched { get; set; } = false;
    public DateTime LastWatchDate { get; set; }
}