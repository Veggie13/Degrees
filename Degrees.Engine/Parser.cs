using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Degrees.Engine
{
    static class Parser
    {
        public static Dictionary<string, int> GetSchema(string[] fields)
        {
            return fields.Select((f, i) => new { Index = i, Field = f })
                .ToDictionary(a => a.Field, a => a.Index);
        }

        public static TitleRecord GetTitle(IReadOnlyDictionary<string, int> schema, string[] fields)
        {
            return new TitleRecord()
            {
                tconst = fields[schema["tconst"]],
                titleType = fields[schema["titleType"]],
                primaryTitle = fields[schema["primaryTitle"]]
            };
        }

        public static NameRecord GetName(IReadOnlyDictionary<string, int> schema, string[] fields)
        {
            return new NameRecord()
            {
                nconst = fields[schema["nconst"]],
                primaryName = fields[schema["primaryName"]]
            };
        }
        private static Regex _regex = new Regex(@"\[""(.*)""\]");

        public static PrincipalRecord GetPrincipal(IReadOnlyDictionary<string, int> schema, string[] fields)
        {
            return new PrincipalRecord()
            {
                tconst = fields[schema["tconst"]],
                nconst = fields[schema["nconst"]],
                category = fields[schema["category"]],
                characters = _regex.Match(fields[schema["characters"]]).Value
            };
        }
    }
}
