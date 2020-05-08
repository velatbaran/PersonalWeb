using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalWeb.Models.Entity
{
    [Table("About")]
    public class About : MyEntityBase
    {
        [DisplayName("Resim")]
        public string ImageURL { get; set; }

        [DisplayName("Metin"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Text { get; set; }
    }
}