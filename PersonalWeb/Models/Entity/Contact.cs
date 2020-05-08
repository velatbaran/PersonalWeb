using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalWeb.Models.Entity
{
    [Table("Contact")]
    public class Contact : MyEntityBase
    {
        [DisplayName("Adres"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string Adressess { get; set; }

        [DisplayName("Telefon"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string Phone { get; set; }

        [DisplayName("E-Mail"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string Email { get; set; }

        [DisplayName("Instagram")]
        public string Instagram { get; set; }

        [DisplayName("Twitter")]
        public string Twitter { get; set; }

        [DisplayName("Telegram")]
        public string Telegram { get; set; }

        [DisplayName("Youtube")]
        public string Youtube { get; set; }

        [DisplayName("Facebook")]
        public string Facebook { get; set; }
    }
}