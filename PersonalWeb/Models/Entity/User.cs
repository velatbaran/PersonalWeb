using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalWeb.Models.Entity
{
    [Table("User")]
    public class User : MyEntityBase
    {
        [DisplayName("Ad"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(25, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır")]
        public string Name { get; set; }

        [DisplayName("Soyad"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string Surname { get; set; }

        [DisplayName("Unvan"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string Degree { get; set; }

        [DisplayName("E-Mail"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır"),
            DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır"),
            DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Github"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string Github { get; set; }

        [DisplayName("Resim")]
        public string ImageURL { get; set; }

    }
}