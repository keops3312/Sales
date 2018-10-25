using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Common.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage =
        "The field {0} can contain maximun {1} and minimum {2} characters",
            MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }


        [Display(Name = "Is Avalilable")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Publish On")]
        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }

        [Display(Name ="Image")]
        public string ImagePath
        {
            get
                {
                    if (string.IsNullOrEmpty(this.ImagePath))
                    {
                         return "noproduct";//aqui iria la imagen por defaul cuanod no tiene iamgen cargada el producto
                    }
                return $"http://salesbackend.azurewebsites.net/{this.ImagePath.Substring(1)}"; //para quitarle la berbulilla


                }

        }


        public string ImageFullPath { get; set; }

        public override string ToString()
        {
            return this.Description;
        }

    }
}
