using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.DAL.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; } // ckeditor olacak veya baska bir editor

        public string MainImgPath { get; set; }

        public string Tags { get; set; } // teknoloji, haber, spor
    }
}
