using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Comision : BusinessEntity
    {
        private int anioEspecialidad;
        private string descripcion;
        private int idPlan;

        public int AnioEspecialidad
        {
            get
            {
                return anioEspecialidad;
            }
            set
            {
                anioEspecialidad = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
            }
        }

        public int IdPlan
        {
            get
            {
                return idPlan;
            }
            set
            {
                idPlan = value;
            }
        }
    }
}
