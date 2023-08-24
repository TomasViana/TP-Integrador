using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ModuloUsuario : BusinessEntity
    {
        private int idUsuario;
        private int idModulo;
        private bool permiteAlta;
        private bool permiteBaja;
        private bool permiteModificacion;
        private bool permiteConsulta;

        public int IdUsuario
        {
            get
            {
                return idUsuario;
            }
            set
            {
                idUsuario = value;
            }
        }

        public int IdModulo
        {
            get
            {
                return idModulo;
            }
            set
            {
                idModulo = value;
            }
        }

        public bool PermiteAlta
        {
            get
            {
                return permiteAlta;
            }
            set
            {
                permiteAlta = value;
            }
        }

        public bool PermiteBaja
        {
            get
            {
                return permiteBaja;
            }
            set
            {
                permiteBaja = value;
            }
        }

        public bool PermiteModificacion
        {
            get
            {
                return permiteModificacion;
            }
            set
            {
                permiteModificacion = value;
            }
        }

        public bool PermiteConsulta
        {
            get
            {
                return permiteConsulta;
            }
            set
            {
                permiteConsulta = value;
            }
        }
    }
}
