using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Model
{
    [Table("Categories")]
    public class Categories
    {
        [Key] public int Id { get; set; }

        [Required (ErrorMessage = "Tên loại không được để trống!")]
        [Display (Name="Tên loại SP")] 
        public string Name { get; set; }

        [Display(Name = "Tên rút gọn")] 
        public string Slug { get; set; }

        [Display(Name = "Cấp cha")] 
        public int? ParentID { get; set; }

        [Display(Name = "Sắp xếp")] 
        public int? Order { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống!")] 
        [Display(Name = "Mô tả")] 
        public string MetaDesc { get; set; }

        [Required(ErrorMessage = "Từ khoá không được để trống!")] 
        [Display(Name = "Từ khoá")] 
        public string MetaKey { get; set; }

        [Required(ErrorMessage = "Ngày tạo không được để trống!")] 
        [Display(Name = "Ngày tạo")] 
        public DateTime CreateAt { get; set; }

        [Required(ErrorMessage = "Người tạo không được để trống!")] 
        [Display(Name = "Tạo bởi")] 
        public int CreateBy { get; set; }

        [Required(ErrorMessage = "Ngày cập nhật không được để trống!")] 
        [Display(Name = "Ngày cập nhật")] 
        public DateTime UpdateAt { get; set; }

        [Required(ErrorMessage = "Người cập nhật không được để trống!")] 
        [Display(Name = "Cập nhật bởi")] 
        public int UpdateBy { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống!")] 
        [Display(Name = "Trạng thái")] 
        public int Status { get; set; }
    }
}

