using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineITStore.Models
{
    public partial class Tbl_SlideImage
    {
        [Key]
        public int SlideId { get; set; }
        public string SlideTitle { get; set; }
        public string SlideUrl { get; set; }
        public string SlideImage { get; set; }

        public string SlideDescription { get; set; }

        public Nullable<bool> IsActive { get; set; }
    }
}