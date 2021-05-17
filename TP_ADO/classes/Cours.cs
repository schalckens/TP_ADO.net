using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_ADO.classes
{
    class Cours
    {
        private string codeCours;
        private string libelleCours;
        private int nbJours;
        private Categorie laCategorie;

        public Cours(string codeCours, string libelleCours, int nbJours, Categorie laCategorie)
        {
            this.codeCours = codeCours;
            this.libelleCours = libelleCours;
            this.nbJours = nbJours;
            this.laCategorie = laCategorie;
        }

        public override string ToString()
        {
            return " \n Code Cours : " + codeCours + " \n Libellé : " + libelleCours + " \n Durée : " + nbJours + " jours" + laCategorie + "\n"; 

        }
    }
}
