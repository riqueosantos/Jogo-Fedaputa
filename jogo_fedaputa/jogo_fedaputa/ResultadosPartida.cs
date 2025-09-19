using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class ResultadosPartida
    {
        private CelulaDadosPartida primeiro;

        public CelulaDadosPartida Primeiro
        {
            get { return primeiro; }
            set { primeiro = value; }
        }

        public ResultadosPartida()
        {
            this.primeiro = new CelulaDadosPartida();
        }

        public void Inserir(Jogador jogador)
        {
            if(primeiro.Prox == null)
            {
                primeiro.Prox = new CelulaDadosPartida(jogador);
            }
            else
            {
                CelulaDadosPartida i;
                for (i = primeiro.Prox; i.Prox != null; i = i.Prox);
                i.Prox = new CelulaDadosPartida(jogador);

            }
        }

        public void InserirPalpite(Jogador jogador, int palpite)
        {
            CelulaDadosPartida i;
            for (i = primeiro.Prox; i.Elemento != jogador; i = i.Prox) ;
            i.Palpite = palpite;
        }

        public void InserirVitoria(Jogador jogador)
        {
            CelulaDadosPartida i;
            for (i = primeiro.Prox; i.Elemento != jogador; i = i.Prox) ;
            i.Vitorias+=1;
        }

        public void InserirScore(Jogador jogador, int score)
        {
            CelulaDadosPartida i, tmp = primeiro;
            for (i = primeiro.Prox; i.Elemento != jogador; i = i.Prox,tmp = tmp.Prox) ;
            i.Score = score;
        }

        public int ObterVitorias(Jogador jogador)
        {
            CelulaDadosPartida i;
            for (i = primeiro.Prox; i.Elemento != jogador; i = i.Prox) ;
            return i.Vitorias;
        }
    }
}
