using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Suppliers")]
    public class Suppliers
    {
        [Key] public int Id { get; set; }
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống!")] 
        [Display(Name = "Tên nhà cung cấp")] public string Name { get; set; }
        [Display(Name = "Logo")] public string Image { get; set; }
        [Display(Name = "Tên rút gọn")] public string Slug { get; set; }
        [Display(Name = "Sắp xếp")] public int? Order { get; set; }
        [Display(Name = "Tên đầy đủ")] public string Fullname { get; set; }
        [Display(Name = "Số điện thoại")] public string Phone { get; set; }
        [Display(Name = "Email")] public string Email { get; set; }
        [Display(Name = "Liên kết")] public string UrlSite { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống!")]
        [Display(Name = "Mô tả")] public string MetaDesc { get; set; }
        [Display(Name = "Từ khoá")] public string MetaKey { get; set; }
        [Display(Name = "Tạo bởi")] public int CreateBy { get; set; }
        [Display(Name = "Ngày tạo")] public DateTime CreateAt { get; set; }
        [Display(Name = "Cập nhật bởi")] public int UpdateBy { get; set; }
        [Display(Name = "Ngày cập nhật")] public DateTime UpdateAt { get; set; }
        [Display(Name = "Trạng thái")] public int? Status { get; set; }
    }
}