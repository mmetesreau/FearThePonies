using System.Collections.Generic;
using System.Linq;

namespace FearServer.Models
{
    public class Score 
    {
        public string Pseudo { get; set; }
        public int Step { get; set; }
    }

    public class DashboardViewModel
    {
        public List<Score> Scores { get; set; }

        public DashboardViewModel(List<KeyValuePair<string, int>> scores)
        {
            Scores = scores
                        .Select(s => new Score { Pseudo = s.Key, Step = s.Value })
                        .ToList();
        }

    }
}
