using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class Pilha
    {
        private Celula topo;

        public Pilha()
        {
            this.topo = null;
        }

        public bool Vazia()
        {
            if (topo == null)
                return true;
            else
                return false;
        }

        public void Empilhar(Carta elemento)
        {
            Celula celula = new Celula(elemento);
            celula.Prox = topo;
            topo = celula;
        }

        public Carta Desempilhar()
        {
            if (Vazia())
                throw new Exception("Erro: Pilha vazia!");

            Celula celulaTmp = topo;
            topo = topo.Prox;
            celulaTmp.Prox = null;
            return celulaTmp.Elemento;
        }

        public int Tamanho()
        {
            int tamanho = 0;
            for(Celula i = topo; i != null; i = i.Prox)
            {
                tamanho++;
            }
            return tamanho;
        }
    }
}
