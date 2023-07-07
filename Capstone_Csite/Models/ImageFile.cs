using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone_Csite.Models
{
    public class ImageFile
    {
        public List<HttpPostedFileBase> files { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string desc { get; set; }
        public byte[] Data { get; set; }
    }
}