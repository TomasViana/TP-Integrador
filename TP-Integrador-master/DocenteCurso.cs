using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DocenteCurso : BusinessEntity
    {
        private TipoCargo cargo;
        private int idCurso;
        private int idDocente;

        public enum TipoCargo
        {
            Auxiliar,
            Profesor
        }
        public TipoCargo Cargo
        {
            get
            {
                return cargo;
            }
            set
            {
                cargo = value;
            }
        }

        public int IdCurso
        {
            get
            {
                return idCurso;
            }
            set
            {
                idCurso = value;
            }
        }

        public int IdDocente
        {
            get
            {
                return idDocente;
            }
            set
            {
                idDocente = value;
            }
        }
    }
}
