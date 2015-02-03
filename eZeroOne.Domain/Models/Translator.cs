using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZeroOne.Domain
{
    public class Translator
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Landline { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
        public string ImageName { get; set; }
    }
}
