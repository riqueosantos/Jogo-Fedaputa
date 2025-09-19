using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class Celula
    {
        private Carta elemento;
        private Celula prox;

        public Celula Prox
        {
            get { return prox; }
            set { prox = value; }
        }

        public Carta Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }

        public Celula (Carta elemento)
        {
            this.elemento = elemento;
            this.prox = null;
        }

        public Celula()
        {
            this.elemento = null;
            this.prox = null;
        }
    }
}
