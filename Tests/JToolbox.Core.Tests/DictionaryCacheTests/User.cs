using JToolbox.Core.Abstraction;
using System;

namespace JToolbox.Core.Tests.DictionaryCacheTests
{
    internal class User : IKey
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime ModificationDate { get; set; }

        public string Name { get; set; }

        public User Clone() => MemberwiseClone() as User;
    }
}