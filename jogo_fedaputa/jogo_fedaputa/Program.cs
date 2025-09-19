using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Log.IniciarNovoLog();
            int numJogadores = 0;
            int opcao = 0;

            Log.ExibirImagem();

            Console.WriteLine("\n--- Pressione enter para iniciar o jogo ---");
            Console.ReadKey();
            Console.Clear();

            do
            {
                Console.Write($"Digite 1 para jogar com amigos ou 2 para jogar sozinho: ");
                string entrada = Console.ReadLine();

                bool ehNumero = int.TryParse(entrada, out opcao);

                if (!ehNumero || opcao < 1 || opcao > 2)
                {
                    Console.WriteLine("\n--- Entrada inválida. Por favor, digite 1 ou 2 ---\n");
                    opcao = 0;
                }
            } while (opcao == 0);

            Console.Clear();

            do
            {
                Console.Write($"Digite o número de jogadores da partida (2 à 8): ");
                string entrada = Console.ReadLine();

                bool ehNumero = int.TryParse(entrada, out numJogadores);

                if(!ehNumero || numJogadores < 2 || numJogadores > 8)
                {
                    Console.WriteLine("\n--- Entrada inválida. Por favor, digite um número entre 2 e 8 ---\n");
                    numJogadores = 0;
                }
            } while (numJogadores == 0);

            Console.Clear();

            Log.Registrar($"Número de jogadores: {numJogadores}");
            Jogo game = new Jogo(numJogadores, opcao);
            game.Jogar();
            Console.ReadLine();
        }
    }
}
