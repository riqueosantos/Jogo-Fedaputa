using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class Baralho
    {
        private Pilha baralho;

        public Baralho()
        {
            this.baralho = new Pilha();
            PreencherBaralho();
        }

        private void PreencherBaralho()
        {
            int[] numero = { 1, 2, 3, 4, 5, 6, 7, 10, 11, 12 };

            for (int i = 1; i <= 4; i++)
            {
                for (int j = 0; j < numero.Length; j++)
                {
                    Carta cartaTmp = new Carta(numero[j], i);
                    baralho.Empilhar(cartaTmp);
                }
            }
        }

        public Carta[] ToArray()
        {
            if (baralho.Vazia())
                throw new Exception("Erro: Baralho vazio!");

            Carta[] baralhoTmp = new Carta[baralho.Tamanho()];

            for(int i = 0; i < baralhoTmp.Length; i++)
            {
                baralhoTmp[i] = baralho.Desempilhar();
            }
            return baralhoTmp;
        }

        public void Embaralhar()
        {
            Random r = new Random();
            Carta[] baralhoTmp = ToArray();


            for (int i = baralhoTmp.Length - 1; i > 0; i--)
            {
                int j = r.Next(i + 1);
                Carta tmp = baralhoTmp[i];
                baralhoTmp[i] = baralhoTmp[j];
                baralhoTmp[j] = tmp;
            }

            foreach (var carta in baralhoTmp)
            {
                baralho.Empilhar(carta);
            }
        }

        public Carta RemoverCarta()
        {
            if (baralho.Vazia())
            {
                throw new Exception("Erro: Baralho vazio!");
            }
            else
            {
                return baralho.Desempilhar();
            }
        }

        public void AdicionarCarta(Carta carta)
        {
            baralho.Empilhar(carta);
        }
    }
}
