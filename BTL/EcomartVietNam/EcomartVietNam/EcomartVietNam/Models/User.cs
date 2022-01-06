namespace EcomartVietNam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Orders = new HashSet<Order>();
            Orders1 = new HashSet<Order>();
        }

        [Key]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email không đúng định dạng")]
        public string email { get; set; }

        [StringLength(11,ErrorMessage ="Số điện thoại không đúng định dạng")]
        [MaxLength(11,ErrorMessage ="Số điện thoại không đúng định dạng")]
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        public string phone_number { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [StringLength(200)]
        [DataType(DataType.Password)]
        [UIHint("Password")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự.")]
        public string password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Mật khẩu nhập lại không được để trống!")]
        [Compare("password", ErrorMessage = "Mật khẩu không khớp.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự.")]
        public string confirm_password { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Tên không được để trống!")]
        public string full_name { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Địa chỉ không được để trống!")]
        public string address { get; set; }
        [UIHint("Role")]
        
        public int role { get; set; }
        [UIHint("Active")]
        public bool is_active { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders1 { get; set; }
    }
}
