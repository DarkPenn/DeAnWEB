using DeAnWEB.Models.ViewModel;
using DeAnWEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeAnWEB.Models.ViewModel
{
    public class CheckoutVM //Lưu thông tin form Checkout
    {
        public List<CartItem> CartItems { get; set; }
        public int CustomerID { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        public System.DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Tổng giá trị")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Trạng thái thanh toán")]
        public string PaymentStatus { get; set; }

        [Required]
        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        [Required]
        [Display(Name = "Phương thức giao hàng")]
        public string ShippingMethod { get; set; }

        [Required]
        [Display(Name = "Địa chỉ giao hàng")]
        public string ShippingAddress { get; set; }
        public string City { get; set; }

        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Tên người nhận")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        //Các thuộc tính khác của đơn hàng
        public List<OrderDetail> OrderDetails { get; set; }
    }
}