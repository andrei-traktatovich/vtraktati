using System;

namespace VTraktate.Domain
{
    public class LegalForm : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}