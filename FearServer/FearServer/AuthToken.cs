using System;
using System.Linq;

namespace FearServer
{
    class AuthToken
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int NoiseLength = 10;

        public static string Parse(string authToken)
        {
            if (string.IsNullOrEmpty(authToken) || authToken.Length <= NoiseLength)
                return string.Empty;

            var cypheredPseudo = authToken.Substring(NoiseLength, authToken.Length - NoiseLength);

            return Decypher(cypheredPseudo);
        }

        public static string Generate(string pseudo)
        {
            if (string.IsNullOrEmpty(pseudo))
                throw new ArgumentException("Pseudo can not be null or empty");

            var random = new Random();
            var noise = new string(Enumerable.Repeat(chars, NoiseLength).Select(s => s[random.Next(s.Length)]).ToArray());

            return $"{noise}{Cypher(pseudo)}";
        }
        private static string Decypher(string cypheredPseudo)
        {
            return cypheredPseudo.Aggregate(string.Empty, (s, i) => s += (char)(i + 1));
        }

        private static string Cypher(string pseudo)
        {
            return pseudo.Aggregate(string.Empty, (s, i) => s += (char)(i - 1));
        }
    }
}
