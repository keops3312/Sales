

namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
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

        /*agregando una imagen*/
        [Display(Name ="Image")]
        public string ImagePath { get; set; }

        //Esto lo hacemos para devolver un resultado directo
        public override string ToString()
        {
            return this.Description;
        }


    }
}
