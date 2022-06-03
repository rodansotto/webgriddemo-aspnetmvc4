using System;
using System.Collections.Generic;

namespace MyMvc4App.Models
{
    public partial class vProductAndDescription
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductModel { get; set; }
        public string Culture { get; set; }
        public string Description { get; set; }
    }
}
