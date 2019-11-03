using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Degrees.Engine
{
    static class TsvReader
    {
        public delegate void Progress(int lineCount);
        public delegate T Parse<T>(IReadOnlyDictionary<string, int> schema, string[] fields);
        public static IEnumerable<T> ReadAll<T>(TextReader source, Parse<T> parser, Progress report = null)
        {
            string line = source.ReadLine();
            string[] fields = line.Split('\t');
            var schema = Parser.GetSchema(fields);

            int lineCount = 0;
            line = source.ReadLine();
            while (line != null)
            {
                fields = line.Split('\t');
                yield return parser(schema, fields);
                report(++lineCount);
                line = source.ReadLine();
            }
        }
    }
}
