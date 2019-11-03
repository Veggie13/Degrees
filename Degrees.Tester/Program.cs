using System;
using System.IO;
using System.Linq;
using Degrees.Engine;

namespace Degrees.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string titleSourcePath = @"..\..\..\..\title.basics.tsv";
            string nameSourcePath = @"..\..\..\..\name.basics.tsv";
            string principalSourcePath = @"..\..\..\..\title.principals.tsv";

            var db = new Database();

            using (var titleSource = new StreamReader(titleSourcePath))
            using (var nameSource = new StreamReader(nameSourcePath))
            using (var principalSource = new StreamReader(principalSourcePath))
            {
                db.Load(titleSource, nameSource, principalSource, (m, p, a) =>
                {
                    Console.CursorTop = p;
                    Console.CursorLeft = 0;
                    Console.WriteLine("{1}) {0} : {2} lines", m, p, a);
                });
            }

            var actor1 = db.Actors.First(a => a.Name.Contains("Jolie"));
            var actor2 = db.Actors.First(a => a.Name.Contains("Bacon"));

            Console.WriteLine($"Finding shorted path from {actor1.Name} to {actor2.Name}...");

            var degrees = Engine.Engine.FindFewestDegrees(actor1, actor2, (c, d) =>
            {
                Console.CursorTop = 5;
                Console.CursorLeft = 0;
                Console.WriteLine("Checked {0} (depth {1})", c, d);
            });

            Console.WriteLine($"{actor1.Name}");
            int depth = 0;
            foreach (var degree in degrees)
            {
                Console.WriteLine($"{++depth})\tin \"{degree.Movie.Title}\" with {degree.Costar.Name}");
            }
        }
    }
}
