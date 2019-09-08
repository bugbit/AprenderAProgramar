using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprenderAProgramar.Comun
{
    [AttributeUsage(AttributeTargets.Class)]
    class ProgramaAttribute : Attribute
    {
        public string[] Grupo { get; set; }
        public string Nombre { get; set; }
    }
}
