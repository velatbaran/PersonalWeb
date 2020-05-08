using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalWeb.Models.Entity
{
    [Table("Services")]
    public class Services : MyEntityBase
    {
        [DisplayName("Resim"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string ImageURL { get; set; }

        [DisplayName("Başlık"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string Title { get; set; }

        [DisplayName("Metin"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Text { get; set; }

        public virtual List<ProjectsDone> ProjectsDones { get; set; }
        public Services()
        {
            ProjectsDones = new List<ProjectsDone>();
        }
    }
}