using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Degrees.Engine
{
    public class Database
    {
        private Dictionary<string, Movie> _movies;
        private Dictionary<string, Actor> _actors;

        public IEnumerable<Movie> Movies { get { return _movies.Values; } }
        public IEnumerable<Actor> Actors { get { return _actors.Values; } }

        public void Load(TextReader titleSource, TextReader nameSource, TextReader principalSource)
        {
            var titles = TsvReader.ReadAll(titleSource, Parser.GetTitle)
                .Where(t => t.titleType == "movie");

            var names = TsvReader.ReadAll(nameSource, Parser.GetName);

            _movies = titles.ToDictionary(t => t.tconst, t => new Movie(t.tconst, t.primaryTitle));
            _actors = names.ToDictionary(n => n.nconst, n => new Actor(n.nconst, n.primaryName));

            var principals = TsvReader.ReadAll(principalSource, Parser.GetPrincipal)
                .Where(p => _actors.ContainsKey(p.nconst))
                .Where(p => _movies.ContainsKey(p.tconst))
                .Where(p => p.category == "actor" || p.category == "actress");

            foreach (var principal in principals)
            {
                _movies[principal.tconst].Cast.Add(_actors[principal.nconst]);
                _actors[principal.nconst].Credits.Add(_movies[principal.tconst]);
            }
        }
    }
}
