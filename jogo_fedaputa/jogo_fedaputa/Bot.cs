using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal static class Bot
    {
        static Random r = new Random();

        public static string NomeBot(int i)
        {
            string[] nomes =
            {
                "bot1",
                "bot2",
                "bot3",
                "bot4",
                "bot5",
                "bot6",
                "bot7",
            };

            return nomes[i];
        }

        public static int PalpiteBot(Jogador bot, int palpiteMinimo)
        {
            int cont = 0;
            for (int i = 0; i < bot.Mao.Count; i++)
            {
                if (bot.Mao[i].Valor > 32)
                {
                    cont++;
                }
            }

            int palpiteFinal = 0;

            if (cont < bot.Mao.Count)
                palpiteFinal = cont + r.Next(0, bot.Mao.Count - cont + 1);
            else
                palpiteFinal = cont;

            if (palpiteFinal < palpiteMinimo)
                return palpiteMinimo;
            else
                return palpiteFinal;
        }

        public static int JogarCartaBot(Jogador bot, Carta maiorCartaAtual)
        {
            if (maiorCartaAtual == null)
                switch (bot.Mao.Count)
                {
                    case 1:
                        return 0;
                    case 2:
                        return r.Next(0, bot.Mao.Count);
                    case 3:
                        return r.Next(0, bot.Mao.Count - 1);
                    case 4:
                        return r.Next(0, bot.Mao.Count - 1);
                    case 5:
                        return r.Next(0, bot.Mao.Count - 2);
                    default:
                        throw new Exception("Erro!");
                }
            else if (bot.Mao.Count == 1)
                return 0;

            for (int i = 0; i < bot.Mao.Count; i++)
            {
                if (bot.Mao[i].Valor > maiorCartaAtual.Valor)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
