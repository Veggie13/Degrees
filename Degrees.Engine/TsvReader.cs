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
            if (report == null)
            {
                report = (lc) => { };
            }

            string headerLine = source.ReadLine();
            string[] fields = headerLine.Split('\t');
            var schema = Parser.GetSchema(fields);

            int lineCount = 0;
            var blockBuffer = new char[5120 * 1024];
            int blockLength = source.ReadBlock(blockBuffer, 0, blockBuffer.Length);
            string block = "";
            while (0 < blockLength)
            {
                block += new string(blockBuffer, 0, blockLength);
                var blockTask = source.ReadBlockAsync(blockBuffer, 0, blockBuffer.Length);
                using (var reader = new StringReader(block))
                {
                    string line = reader.ReadLine();
                    string nextLine;
                    while ((nextLine = reader.ReadLine()) != null)
                    {
                        fields = line.Split('\t');
                        yield return parser(schema, fields);
                        ++lineCount;

                        line = nextLine;
                    }

                    block = line;
                }

                report(lineCount);
                blockTask.Wait();
                blockLength = blockTask.Result;
            }

            fields = block.Split('\t');
            yield return parser(schema, fields);
        }
    }
}
