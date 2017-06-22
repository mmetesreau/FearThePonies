using System.Collections.Generic;
using System.Linq;

namespace FearServer.Repositories
{
    public class InMemoryScoreRepository
    {
        private readonly Dictionary<string,int> _scores;

        public InMemoryScoreRepository()
        {
            _scores = new Dictionary<string,int>();
        }

        public void Add(string pseudo, int score)
        {
            if (!_scores.ContainsKey(pseudo))
                _scores.Add(pseudo, score);
        }

        public void Update(string pseudo, int score)
        {
            if (_scores.ContainsKey(pseudo))
                 _scores[pseudo] = score;
        }

        public List<KeyValuePair<string, int>> GetAll()
        {
            return _scores.ToList();
        }
    }
}
