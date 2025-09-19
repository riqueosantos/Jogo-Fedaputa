using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class Jogador
    {
        private string nome;
        private int posicao = 0;
        private int vidas = 5;
        private bool ehBot = false;
        private Queue<int> ranking;
        private List<Carta> mao;

        public bool EhBot
        {
            get { return ehBot; }
            set { ehBot = value; }
        }

        public int Posicao
        {
            get { return posicao; }
            set { posicao = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public int Vidas
        {
            get { return vidas; }
            set { vidas = value; }
        }

        public List<Carta> Mao
        {
            get { return mao; }
            set { mao = value; }
        }

        public Jogador(string nome)
        {
            this.nome = nome;
            this.ranking = new Queue<int>();
            this.mao = new List<Carta>();
        }

        public void PerderVida(int x)
        {
            this.vidas-=x;
        }

        public void AddMao(Baralho baralho, int numCartas)
        {
            for(int i = 0; i < numCartas; i++)
            {
                this.mao.Add(baralho.RemoverCarta());
            }
            this.mao.Sort((x, y) => x.Valor.CompareTo(y.Valor));
        }

        public Carta RemoverCartaMao(int i)
        {
            Carta tmp = this.mao[i];
            this.mao.RemoveAt(i);

            return tmp;
        }

        public void AddRankingPosicao(int posicaoAtual)
        {
            this.posicao = posicaoAtual;
            this.ranking.Enqueue(posicao);

            if (this.ranking.Count > 5)
            {
                this.ranking.Dequeue();
            }
        }

        public void ExibirVidas()
        {
            if (this.vidas > 0)
            {
                Console.Write("[ ");
                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = 0; i < this.vidas; i++)
                {
                    Console.Write("♥ ");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("]");
                Console.WriteLine(" ");
            }
        }

        public void ExibirJogador()
        {
            Console.Write($"Nome: {this.nome} \tVidas: ");
            ExibirVidas();
            Console.WriteLine("\n");
            ExibirMao();
        }

        public void ExibirMao()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < this.mao.Count; j++)
                {
                    if (this.mao[j].Naipe == 1 || this.mao[j].Naipe == 3)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ResetColor();
                    Console.Write($"{this.mao[j].GetLinhaCarta(i)}\t");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
            for (int j = 0; j < this.mao.Count; j++)
            {
                Console.Write($"   {j+1}   \t");
            }
            Console.WriteLine();
        }

        public bool TemVida()
        {
            if (this.vidas > 0)
                return true;
            else
                return false;
        }
    }
}
