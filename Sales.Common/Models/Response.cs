using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Common.Models
{
    public class Response
    {
        public bool IsSucces { get; set; } //si la respuesta fue exitosa o no
        public string Message { get; set; }// el mensaje de respuesta

        public object Result { get; set; }//para que devuelva cualquier objeto
    }

}
