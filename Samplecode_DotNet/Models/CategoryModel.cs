using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samplecode_DotNet.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? TypeId { get; set; }
        public long? Rownumber { get; set; }       
    }
}