using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE1._01
{
    internal class Ennemi
    {
        private static readonly string ETAT_VIVANT = "vivant";
        private static readonly string ETAT_MORT = "mort";
        public static readonly string TYPE_NORMAL = "normal";
        public static readonly string TYPE_LENT = "lent";
        public static readonly string TYPE_RAPIDE = "rapide";
        private static readonly double VITESSE_NORMAL = 10;
        private static readonly double VITESSE_LENT = 10;
        private static readonly double VITESSE_RAPIDE = 10;
        private string lettre;
        private string type;
        private double vitesse;
        private string etat;

        public Ennemi(string lettre, string type)
        {
            this.Lettre = lettre;
            this.Type = type;
            this.Etat = ETAT_VIVANT;
        }

        public string Lettre
        {
            get
            {
                return this.lettre;
            }

            set
            {
                this.lettre = value;
            }
        }

        public string Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
            }
        }

        public double Vitesse
        {
            get
            {
                return this.vitesse;
            }

            set
            {
                this.vitesse = value;
            }
        }

        public string Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }
    }
}
