using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineITStore.Models
{
    public partial class Tbl_Product
    {

        public Tbl_Product()
        {
            this.Tbl_Cart = new HashSet<Tbl_Cart>();
        }
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(100, ErrorMessage = "Minimum 5 and maximum 100 characters are allowed", MinimumLength = 3)]
        public string ProductName { get; set; }
        [Required]
        [Range(1, 50)]
        public int CategoryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsOnSale { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public Nullable<bool> IsFeatured { get; set; }

        [Required]
        [Range(typeof(int), "0", "1000", ErrorMessage = "Invalid Quantity")]
        public Nullable<int> Quantity { get; set; }

        [Required]
        [Range(typeof(decimal), "1", "200000", ErrorMessage = "invalid Price")]
        public Nullable<decimal> Price { get; set; }

        public Nullable<decimal> SaleModifier { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tbl_Cart> Tbl_Cart { get; set; }
        [JsonIgnore]
        public virtual Tbl_Category Tbl_Category { get; set; }
    }
}