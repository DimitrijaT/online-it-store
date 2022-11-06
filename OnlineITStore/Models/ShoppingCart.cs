using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineITStore.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }

        public virtual ICollection<Tbl_Category> ProductItems { get; set; }


        //Foreign Key
        public string UserId { get; set; }


    }
}