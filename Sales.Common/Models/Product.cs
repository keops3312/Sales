

namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage =
            "The field {0} can contain maximun {1} and minimum {2} characters",
            MinimumLength = 3)]
       
        public string Description { get; set; }

        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public Decimal Price{ get; set; }

        [Display(Name ="Is Available")]
        public bool IsAvailable { get; set; }


        [Display(Name = "Publish On")]
        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }


        [Required]
        [StringLength(128)]
        public string UserId { get; set; }


        /*agregando una imagen PARA MVC*/
        [Display(Name ="Image")]
        public string ImagePath { get; set; }

        /*agregado una imagen PARA MVVM*/
        public string ImageFullPath {


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

        //Esto lo hacemos para devolver un resultado directo
        public override string ToString()
        {
            return this.Description;
        }

        /*PARA LA WEBCAM DEL ANDROID*/
        [NotMapped]
        public byte[] ImageArray { get; set; }



    }
}
