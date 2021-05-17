using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_ADO.classes
{
    class Categorie : DataTable
    {
        private int idCategorie;
        private string libelleCategorie = null;

        public Categorie(int idCategorie)
        {
            this.idCategorie = idCategorie;
        }
        public Categorie(int idCategorie, string libelleCategorie)
        {
            this.idCategorie = idCategorie;
            this.libelleCategorie = libelleCategorie;
        }

        public override string ToString()
        {
            if (libelleCategorie == null)
            {
                return " \n Catégorie N° : " + idCategorie + "\n";
            }
            else
            {
                return " \n Catégorie N° : " + idCategorie + " \n Libellé : " + libelleCategorie + "\n";
            }
            
        }

    }
}
