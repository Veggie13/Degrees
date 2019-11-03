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

        public delegate void Progress(string message, int part, int amount);

        public void LoadTitles(TextReader titleSource, Progress report = null)
        {
            if (report == null)
            {
                report = (m, p, a) => { };
            }

            var titles = TsvReader.ReadAll(titleSource, Parser.GetTitle, lc => { report("Loading titles", 1, lc); })
                .Where(t => t.titleType == "movie");

            _movies = titles.ToDictionary(t => t.tconst, t => new Movie(t.tconst, t.primaryTitle));
        }

        public void LoadNames(TextReader nameSource, Progress report = null)
        {
            if (report == null)
            {
                report = (m, p, a) => { };
            }

            var names = TsvReader.ReadAll(nameSource, Parser.GetName, lc => { report("Loading names", 2, lc); });

            _actors = names.ToDictionary(n => n.nconst, n => new Actor(n.nconst, n.primaryName));
        }

        public void LoadPrincipals(TextReader principalSource, Progress report = null)
        {
            if (report == null)
            {
                report = (m, p, a) => { };
            }

            var principals = TsvReader.ReadAll(principalSource, Parser.GetPrincipal, lc => { report("Loading principals", 3, lc); })
                .Where(p => _actors.ContainsKey(p.nconst))
                .Where(p => _movies.ContainsKey(p.tconst))
                .Where(p => p.category == "actor" || p.category == "actress");

            foreach (var principal in principals)
            {
                _movies[principal.tconst].Cast.Add(_actors[principal.nconst]);
                _actors[principal.nconst].Credits.Add(_movies[principal.tconst]);
            }
        }

        public void Load(TextReader titleSource, TextReader nameSource, TextReader principalSource, Progress report = null)
        {
            if (report == null)
            {
                report = (m, p, a) => { };
            }

            LoadTitles(titleSource, report);
            LoadNames(nameSource, report);
            LoadPrincipals(principalSource, report);
        }
    }
}
