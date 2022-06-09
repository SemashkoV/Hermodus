using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MyBlog.Data
{
    public class ShippingDetail
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public string Cart { get; set; }
        [DisplayName("Published")]
        public bool Publish { get; set; }
        [DisplayName("Create Time:")]
        [Column(TypeName = "DateTime2")]
        public DateTime Create_time { get; set; }
    }
}