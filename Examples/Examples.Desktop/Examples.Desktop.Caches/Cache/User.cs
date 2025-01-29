using JToolbox.Core.Abstraction;
using System;

namespace Examples.Desktop.Caches.Cache
{
    internal class User : IKey
    {
        public int Id { get; set; }

        public bool IsUpdated { get; set; }

        public DateTime ModificationDate { get; set; }

        public string Name { get; set; }

        public User Clone() => MemberwiseClone() as User;
    }
}