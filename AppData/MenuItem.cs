using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
    public class MenuItem
    {
        public int ID { get; set; }        
        public string Item { get; set; }        
        public string? Url { get; set; }
        public string? TenController { get; set; }
        public bool TrangThai { get; set; }
        public int OrderIndex { get; set; }
       /* public int? ParentID { get; set; }*/
        /*public bool IsActive { get; set; } = true; // Trạng thái hoạt động
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo*/


        /*   public MenuItem Parent { get; set; }      // Mục menu cha
           public ICollection<MenuItem> Children { get; set; } = new List<MenuItem>(); // Danh sách các mục con*/
    }

}
