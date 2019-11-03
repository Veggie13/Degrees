using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Degrees.Engine
{
    static class TsvReader
    {
        public delegate T Parse<T>(IReadOnlyDictionary<string, int> schema, string[] fields);
        public static IEnumerable<T> ReadAll<T>(TextReader source, Parse<T> parser)
        {
            string line = source.ReadLine();
            string[] fields = line.Split('\t');
            var schema = Parser.GetSchema(fields);

            line = source.ReadLine();
            while (line != null)
            {
                var readTask = source.ReadLineAsync();
                fields = line.Split('\t');
                yield return parser(schema, fields);
                readTask.Wait();
                line = readTask.Result;
            }
        }
    }
}
