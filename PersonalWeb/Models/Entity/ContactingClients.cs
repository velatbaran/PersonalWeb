using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalWeb.Models.Entity
{
    [Table("ContactingClients")]
    public class ContactingClients : MyEntityBase
    {
        [DisplayName("Ad"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string Name { get; set; }

        [DisplayName("E-Mail"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır"),
    DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Telefon")]
        public string Phone { get; set; }

        [DisplayName("Mesaj"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Message { get; set; }
    }
}