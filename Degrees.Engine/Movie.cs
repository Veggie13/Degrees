using System;
using System.Collections.Generic;
using System.Text;

namespace Degrees.Engine
{
    public class Movie
    {
        public Movie(string id, string title)
        {
            ID = id;
            Title = title;
            Cast = new HashSet<Actor>();
        }

        public string ID { get; private set; }
        public string Title { get; private set; }
        public ISet<Actor> Cast { get; private set; }

        public override bool Equals(object obj)
        {
            return obj == this;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override string ToString()
        {
            return $"Movie: \"{Title}\"";
        }
    }
}
