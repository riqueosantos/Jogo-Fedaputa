using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class Jogo
    {
        private List<Jogador> jogadores;
        private Jogador primeiroJogador;
        private Baralho baralho;
        private int cartasNaMao = 5;
        private bool direcaoDescendo = true;

        public Jogo(int numJogadores, int opcao)
        {
            this.jogadores = new List<Jogador>(numJogadores);
            this.baralho = new Baralho();

            if (opcao == 2)
            {
                AdicionarJogador();
                for (int i = 0; i < numJogadores - 1; i++)
                {
                    Jogador bot = new Jogador(Bot.NomeBot(i));
                    bot.EhBot = true;
                    jogadores.Add(bot);
                }
                Console.Clear();
            }
            else
            {
                for (int i = 0; i < numJogadores; i++)
                {
                    Console.Write($"{i + 1}° Jogador - ");
                    AdicionarJogador();
                    Console.Clear();
                }
            }
        }

        public void AdicionarJogador()
        {
            string nome = string.Empty;
            bool nomeUnico = false;

            while(!nomeUnico)
            {
                Console.Write("Digite o nome do jogador: ");
                nome = Console.ReadLine().ToUpper();
                if(NomeUnico(nome))
                {
                    nomeUnico = true;
                }
                else
                {
                    Console.WriteLine("\n--- Nome já existente, tente novamente. ---\n");
                }

            }
            Jogador jogador = new Jogador(nome);
            jogadores.Add(jogador);
        }

        public void AtualizarContadorDeCartas()
        {
            if (direcaoDescendo)
            {
                cartasNaMao--;
                if (cartasNaMao == 1)
                {
                    direcaoDescendo = false;
                }
            }
            else
            {
                cartasNaMao++;
                if (cartasNaMao == 5)
                {
                    direcaoDescendo = true;
                }
            }
        }

        public void ExibirGanhador()
        {
            for (int i = 0; i < jogadores.Count; i++)
            {
                if (jogadores[i].TemVida() || jogadores[i].Posicao == 1)
                {
                    Console.Clear();
                    Console.WriteLine($"O vencedor foi: {jogadores[i].Nome} --- Vidas restantes: {jogadores[i].Vidas}");
                    Log.Registrar($"O vencedor foi: {jogadores[i].Nome} -- Vidas restantes: {jogadores[i].Vidas}");
                    break;
                }
            }
        }

        public bool NomeUnico(string nome)
        {
            if (jogadores.Count == 0)
                return true;

            for (int i = 0; i < jogadores.Count; i++)
            {
                if (jogadores[i].Nome.Equals(nome))
                    return false;
            }
            return true;
        }

        public void JogadorDaRodada()
        {
            if (primeiroJogador == null)
            {
                this.primeiroJogador = jogadores[0];
            }
            else
            {
                int i = jogadores.IndexOf(primeiroJogador);
                do
                {
                    i = (i + 1) % jogadores.Count;
                    this.primeiroJogador = jogadores[i];
                }while(!primeiroJogador.TemVida());
            }
        }

        public bool JogadoresVivos()
        {
            int cont = 0;

            for (int i = 0; i < jogadores.Count; i++)
            {
                if (jogadores[i].TemVida())
                    cont++;

                if (cont > 1)
                    return true;
            }
            return false;
        }

        public void DistribuirBaralho(Baralho baralho)
        {
            foreach(var jogador in jogadores)
            {
                if(jogador.TemVida())
                    jogador.AddMao(baralho, cartasNaMao);
            }
        }

        public void Jogar()
        {
            Log.Registrar("Baralho criado -- 40 cartas");
            do
            {
                JogadorDaRodada();
                baralho.Embaralhar();
                Log.Registrar("Baralho embaralhado");
                DistribuirBaralho(baralho);
                Log.Registrar($"Cartas distribuídas para os jogadores -- {cartasNaMao} por jogador");
                Partida partida = new Partida(jogadores.Count);
                partida.DefinirRodada(jogadores, primeiroJogador);
                partida.DarPalpites(cartasNaMao);
                partida.JogarPartida(cartasNaMao,baralho);
                AtualizarContadorDeCartas();
            } while (JogadoresVivos());
            ExibirGanhador();
            Log.Registrar("Jogo encerrado");
        }
    }
}