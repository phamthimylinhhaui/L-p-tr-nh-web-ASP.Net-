namespace BaiTapNhom6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_detail = new HashSet<Order_detail>();
            Order_detail1 = new HashSet<Order_detail>();
        }

        [Key]
        public int product_id { get; set; }

        public int category_id { get; set; }

        [Required]
        [StringLength(100)]
        public string product_name { get; set; }

        [Required]
        [StringLength(100)]
        public string product_height { get; set; }

        [Required]
        [StringLength(100)]
        public string product_type { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string product_description { get; set; }

        public decimal product_price { get; set; }

        [StringLength(50)]
        public string product_image { get; set; }

        public int? product_amount { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual Category Category { get; set; }

        public virtual Category Category1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_detail> Order_detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_detail> Order_detail1 { get; set; }
    }
}
