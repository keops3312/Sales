

namespace Sales.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }

        public string ImageFullPath
        {


            get
            {

                if (string.IsNullOrEmpty(this.ImagePath))
                {

                    /* return null;*/
                    return "noImage"; /*el nombre de la iamgen cargada en drawable de android /ios*/
                }
                /* return http://192.168.1.79:16005/{this.ImagePath.Substring(1)} la ruta del backend pero como ya vamos con la api la cambiamos por la API*/

                return $"http://192.168.1.79:16094/{this.ImagePath.Substring(2)}";
            }


        }
    }

}
