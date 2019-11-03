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
            public int Depth { get { return (Previous == null) ? 1 : (Previous.Depth + 1); } }
        }

        public delegate void Progress(int checkedCount, int currentDepth);

        public static List<Degree> FindFewestDegrees(Actor actor1, Actor actor2, Progress report = null)
        {
            if (actor1.Credits.Count > actor2.Credits.Count)
            {
                var reverse = FindFewestDegrees(actor2, actor1, report);
                if (!reverse.Any())
                {
                    return reverse;
                }
                reverse.Reverse();
                var result = new List<Degree>();
                for (int i = 1; i < reverse.Count; i++)
                {
                    result.Add(new Degree()
                    {
                        Movie = reverse[i - 1].Movie,
                        Costar = reverse[i].Costar
                    });
                }
                result.Add(new Degree()
                {
                    Movie = reverse[reverse.Count - 1].Movie,
                    Costar = actor2
                });
                return result;
            }

            if (report == null)
            {
                report = (c, d) => { };
            }

            var visitedActors = new HashSet<Actor> { actor1 };
            var visitedMovies = new HashSet<Movie>(actor1.Credits);

            var search = new Queue<SearchDegree>(actor1.Credits.SelectMany(m => m.Cast.Except(visitedActors).Select(a => new SearchDegree() { Movie = m, Costar = a })));
            var earlyFind = search.FirstOrDefault(d => d.Costar == actor2);
            if (earlyFind != null)
            {
                return new List<Degree> { earlyFind };
            }

            int checkedCount = 0;
            while (search.TryDequeue(out SearchDegree currentDegree))
            {
                report(++checkedCount, currentDegree.Depth);

                foreach (var movie in currentDegree.Costar.Credits.Except(visitedMovies))
                {
                    foreach (var costar in movie.Cast.Except(visitedActors))
                    {
                        var degree = new SearchDegree()
                        {
                            Movie = movie,
                            Costar = costar,
                            Previous = currentDegree
                        };
                        if (costar == actor2)
                        {
                            var stack = new Stack<Degree>();
                            while (degree != null)
                            {
                                stack.Push(degree);
                                degree = degree.Previous;
                            }
                            return stack.ToList();
                        }

                        search.Enqueue(degree);
                    }
                    visitedActors.UnionWith(movie.Cast);
                }
                visitedMovies.UnionWith(currentDegree.Costar.Credits);
            }

            return new List<Degree>();
        }
    }
}
