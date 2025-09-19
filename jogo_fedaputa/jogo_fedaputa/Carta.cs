using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class Carta
    {
        private int numero;
        private int naipe;
        private int valor;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public int Naipe
        {
            get { return naipe; }
            set { naipe = value; }
        }

        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public Carta(int numero, int naipe)
        {
            this.numero = numero;
            this.naipe = naipe;
            this.valor = CalcularValor();
        }

        private int CalcularValor()
        {
            switch (numero)
            {
                case 1:
                    if (naipe == 2)
                        return int.MaxValue - 2;
                    else
                        return 28 + naipe;
                case 2:
                    return 32 + naipe;
                case 3:
                    return 36 + naipe;
                case 4:
                    if (naipe == 4)
                        return int.MaxValue;
                    else
                        return 0 + naipe;
                case 5:
                    return 4 + naipe;
                case 6:
                    return 8 + naipe;
                case 7:
                    if (naipe == 3)
                        return int.MaxValue - 1;
                    else if (naipe == 1)
                        return int.MaxValue - 3;
                    else
                        return 12 + naipe;
                case 10:
                    return 16 + naipe;
                case 11:
                    return 20 + naipe;
                case 12:
                    return 24 + naipe;
                default:
                    throw new ArgumentException("Número da carta inválido.");
            }
        }

        public override string ToString()
        {
            return $"{GetNomeNumero()} {GetSimboloNaipe()}";
        }

        public string GetNomeNumero()
        {
            switch (numero)
            {
                case 1: 
                    return "A";
                case 10: 
                    return "Q";
                case 11: 
                    return "J";
                case 12: 
                    return "K";
                default: 
                    return numero.ToString();
            }
        }

        public string GetSimboloNaipe()
        {
            switch (naipe)
            {
                case 1: 
                    return "♦";
                case 2: 
                    return "♠";
                case 3: 
                    return "♥";
                case 4: 
                    return "♣";
                default: 
                    throw new Exception("Erro!");
            }
        }

        public string GetLinhaCarta(int i)
        {
            string nome = GetNomeNumero();
            string naipeSimbolo = GetSimboloNaipe();
            string[] carta = {
                "┌─────┐",
                $"│{nome,-2} {naipeSimbolo} │",
                "│     │",
                $"│ {naipeSimbolo} {nome,2}│",
                "└─────┘"
            };
            return carta[i];
        }
    }
}
