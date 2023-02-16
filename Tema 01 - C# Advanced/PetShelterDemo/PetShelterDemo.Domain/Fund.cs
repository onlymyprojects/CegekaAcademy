using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelterDemo.Domain
{
    public class Fund : INamedEntity
    {
        public string Name { get; }
        public string Description { get; }
        public string Target { get; }

        public Fund(string name, string description, string target)
        {
            Name = name;
            Description = description;
            Target = target;
        }
    }
}
