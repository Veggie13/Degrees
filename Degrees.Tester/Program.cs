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
                db.Load(titleSource, nameSource, principalSource);
            }

            var actor1 = db.Actors.First(a => a.Name.Contains("Jolie"));
            var actor2 = db.Actors.First(a => a.Name.Contains("Bacon"));

            var degrees = Engine.Engine.FindFewestDegrees(actor1, actor2);
        }
    }
}
