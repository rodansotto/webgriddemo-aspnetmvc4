using System;
using System.Collections.Generic;

namespace MyMvc4App.Models
{
    public partial class BuildVersion
    {
        public byte SystemInformationID { get; set; }
        public string Database_Version { get; set; }
        public System.DateTime VersionDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
