using System;
using System.Collections.Generic;

namespace FearServer.Repositories
{
    public class InMemoryUserRepository
    {
        private readonly HashSet<string> _users;

        public InMemoryUserRepository()
        {
            _users = new HashSet<string>();
        }

        public bool Exist(string pseudo)
        {
            return _users.Contains(pseudo);
        }

        public void Add(string pseudo)
        {
            _users.Add(pseudo);
        }
    }
}
