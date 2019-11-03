using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degrees.Engine
{
    public static class Engine
    {
        private class SearchDegree : Degree
        {
            public SearchDegree Previous { get; set; }
        }

        public static List<Degree> FindFewestDegrees(Actor actor1, Actor actor2)
        {
            var visitedActors = new HashSet<Actor> { actor1 };
            var visitedMovies = new HashSet<Movie>(actor1.Credits);

            var search = new Queue<SearchDegree>(actor1.Credits.SelectMany(m => m.Cast.Except(visitedActors).Select(a => new SearchDegree() { Movie = m, Costar = a })));

            while (search.TryDequeue(out SearchDegree currentDegree))
            {
                if (currentDegree.Costar == actor2)
                {
                    var stack = new Stack<Degree>();
                    var degree = currentDegree;
                    while (degree != null)
                    {
                        stack.Push(degree);
                        degree = degree.Previous;
                    }
                    return stack.ToList();
                }

                visitedActors.Add(currentDegree.Costar);
                foreach (var degree in currentDegree.Costar.Credits.Except(visitedMovies)
                    .SelectMany(m => m.Cast.Except(visitedActors).Select(a => new SearchDegree() { Movie = m, Costar = a, Previous = currentDegree })))
                {
                    search.Enqueue(degree);
                }
            }

            return new List<Degree>();
        }
    }
}
