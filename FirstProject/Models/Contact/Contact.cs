using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstProject.Models.Contacts
{
    public class Contact
    {
        [Key]
        public int Id { set; get; }

        [Required(ErrorMessage = "Phải nhập {0}")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [Display(Name ="Họ và tên")]
        public string FullName { set; get; }

        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Phải là địa chỉ email")]
        [Display(Name = "Địa chỉ email")]
        public string Email { set; get; }

        public DateTime? DateSent { set; get; }
        [Display(Name = "Nội dung tin nhắn")]
        public string? Message { set; get; }
        
        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Phải là số điện thoại")]
        public string? PhoneNumber { set; get; }

    }

}
