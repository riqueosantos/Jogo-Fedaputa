using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class Fila
    {
        private Jogador[] array;
        private int primeiro, ultimo;

        public Fila(int tamanho)
        {
            this.array = new Jogador[tamanho + 1];
            this.primeiro = this.ultimo = 0;
        }

        public int Tamanho
        {
            get { return (ultimo - primeiro + array.Length) % array.Length; }
        }

        public void Inserir(Jogador jogador)
        {
            if ((ultimo + 1) % array.Length == primeiro)
            {
                throw new Exception("Erro! Fila cheia");
            }
            array[ultimo] = jogador;
            ultimo = (ultimo + 1) % array.Length;
        }

        public Jogador Remover()
        {
            if (primeiro == ultimo)
            {
                throw new Exception("Erro! Fila vazia");
            }
            Jogador resp = array[primeiro];
            primeiro = (primeiro + 1) % array.Length;
            return resp;
        }

        public bool Vazia()
        {
            if (primeiro == ultimo)
                return true;
            else
                return false;
        }

        public Jogador PrimeiroElemento()
        {
            if (Vazia())
            {
                throw new Exception("Erro!");
            }
            return array[primeiro];
        }

        public Jogador[] ToArray()
        {
            Jogador[] resp = new Jogador[Tamanho];

            for(int i = 0, j = primeiro; i < resp.Length; i++, j = (j + 1) % array.Length)
            {
                resp[i] = array[j];
            }
            return resp;
        }
    }
}
