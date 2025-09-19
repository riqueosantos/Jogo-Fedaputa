using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class CelulaDadosPartida
    {
        private Jogador elemento;
        private CelulaDadosPartida prox;
        private int palpite = 0;
        private int vitorias = 0;
        private int score = 0;

        public CelulaDadosPartida Prox
        {
            get { return prox; }
            set { prox = value; }
        }

        public Jogador Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }

        public int Palpite
        {
            get { return palpite; }
            set { palpite = value; }
        }

        public int Vitorias
        {
            get { return vitorias; }
            set { vitorias = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public CelulaDadosPartida(Jogador elemento)
        {
            this.elemento = elemento;
            this.prox = null;
        }

        public CelulaDadosPartida()
        {
            this.elemento = null;
            this.prox = null;
        }
    }
}
