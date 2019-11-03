using System;
using System.Collections.Generic;
using System.Text;

namespace Degrees.Engine
{
    public class Actor
    {
        public Actor(string id, string name)
        {
            ID = id;
            Name = name;
            Credits = new HashSet<Movie>();
        }

        public string ID { get; private set; }
        public string Name { get; private set; }
        public ISet<Movie> Credits { get; private set; }

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
            return $"Actor: {Name}";
        }
    }
}
