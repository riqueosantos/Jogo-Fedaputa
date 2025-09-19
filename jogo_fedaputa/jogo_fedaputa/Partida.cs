using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    internal class Partida
    {
        private Fila filaRodada;
        private ResultadosPartida resultados;
        private Carta maiorCartaAtual;
        private Jogador jogadorVencedor;


        public Partida(int tamanhoFila)
        {
            this.filaRodada = new Fila(tamanhoFila);
            this.resultados = new ResultadosPartida();
        }

        public ResultadosPartida Resultados
        {
            get { return resultados; }
            set { resultados = value; }
        }

        public Carta MaiorCartaAtual
        {
            get { return maiorCartaAtual; }
            set { maiorCartaAtual = value; }
        }

        public Jogador JogadorVencedor
        {
            get { return jogadorVencedor; }
            set { jogadorVencedor = value; }
        }

        public void DefinirRodada(List<Jogador> jogadores, Jogador jogadorRodada)
        {
            Log.Registrar("Jogadores da Partida {");
            for (int i = 0; i < jogadores.Count(); i++)
            {
                if (jogadores[i].TemVida())
                {
                    filaRodada.Inserir(jogadores[i]);
                    Log.Registrar($"\t{jogadores[i].Nome}, ");
                    resultados.Inserir(jogadores[i]);
                }
            }
            Log.Registrar("}");
            Log.Registrar($"Primeiro jogador da rodada - {jogadorRodada.Nome}");
            OrdenarFila(jogadorRodada);
        }

        public void OrdenarFila(Jogador primeiroJogador)
        {
            while (filaRodada.PrimeiroElemento() != primeiroJogador)
            {
                Jogador tmp = filaRodada.Remover();
                filaRodada.Inserir(tmp);
            }
        }

        public void DarPalpites(int totalCartas)
        {
            int totalPalpites = 0, minRodadasTatais = totalCartas + 1;
            Jogador[] vetorApoio = filaRodada.ToArray();

            for (int i = 0; i < vetorApoio.Length; i++)
            {
                int palpite = -1, minPalpites = 0;
                if (totalPalpites < minRodadasTatais && i == vetorApoio.Length - 1)
                {
                    minPalpites = minRodadasTatais - totalPalpites;
                }
                else if (totalPalpites == 0 && vetorApoio.Length - i == 2)
                {
                    minPalpites = 1;
                }

                if (!vetorApoio[i].EhBot)
                {
                    do
                    {
                        Console.Clear();
                        vetorApoio[i].ExibirJogador();
                        Console.Write($"\nDigite seu palpite jogador {vetorApoio[i].Nome} ({minPalpites} à {totalCartas}): ");
                        string entrada = Console.ReadLine();

                        bool ehNumero = int.TryParse(entrada, out palpite);

                        if (ehNumero == false || palpite < minPalpites || palpite > totalCartas)
                        {
                            palpite = -1;
                        }
                    } while (palpite == -1);
                    Console.Clear();
                }
                else
                {
                    palpite = Bot.PalpiteBot(vetorApoio[i], minPalpites);
                }
                totalPalpites += palpite;
                resultados.InserirPalpite(vetorApoio[i], palpite);
                Log.Registrar($"Jogador = {vetorApoio[i].Nome} -- Palpite = {palpite}");
            }
        }

        public void MaiorCarta(Carta carta, Jogador jogador)
        {
            if (maiorCartaAtual == null)
            {
                this.maiorCartaAtual = carta;
                this.jogadorVencedor = jogador;
            }
            else if (carta.Valor > maiorCartaAtual.Valor)
            {
                this.maiorCartaAtual = carta;
                this.jogadorVencedor = jogador;
            }
        }

        public void ResetVencedor()
        {
            if (maiorCartaAtual != null)
            {
                this.maiorCartaAtual = null;
                this.jogadorVencedor = null;
            }
        }

        public void JogarRodada(Baralho baralho)
        {
            ResetVencedor();
            Jogador[] vetorApoio = filaRodada.ToArray();
            Dictionary<Jogador, Carta> cartasJogadas = new Dictionary<Jogador, Carta>();

            for (int i = 0; i < vetorApoio.Length; i++)
            {
                int cartaEscolhida = -1;
                bool escolhaValida = false;

                if (!vetorApoio[i].EhBot)
                {
                    do
                    {
                        Console.Clear();
                        ExibirMesa(cartasJogadas);
                        Console.WriteLine();
                        vetorApoio[i].ExibirJogador();
                        Console.Write($"\nJogador {vetorApoio[i].Nome}, escolha uma carta para jogar: ");
                        string entrada = Console.ReadLine();

                        bool ehNumero = int.TryParse(entrada, out cartaEscolhida);

                        if (ehNumero && cartaEscolhida >= 1 && cartaEscolhida <= vetorApoio[i].Mao.Count)
                        {
                            cartaEscolhida--;
                            escolhaValida = true;
                        }
                        else
                        {
                            cartaEscolhida = -1;
                        }
                    } while (!escolhaValida);
                }
                else
                {
                    cartaEscolhida = Bot.JogarCartaBot(vetorApoio[i], maiorCartaAtual);
                }

                Log.Registrar($"Jogador = {vetorApoio[i].Nome} -- Jogou a carta = {vetorApoio[i].Mao[cartaEscolhida].ToString()}");
                Carta tmp = vetorApoio[i].RemoverCartaMao(cartaEscolhida);
                cartasJogadas.Add(vetorApoio[i], tmp);
                baralho.AdicionarCarta(tmp);
                MaiorCarta(cartasJogadas[vetorApoio[i]], vetorApoio[i]);
            }
            Console.Clear();

            if (!OnlyBot())
            {
                ExibirMesa(cartasJogadas);
                Console.WriteLine("\n--- Pressione enter para continuar ---");
                Console.ReadKey();
            }
            OrdenarFila(jogadorVencedor);
            resultados.InserirVitoria(jogadorVencedor);
            Log.Registrar($"Carta vencedora da rodada = {maiorCartaAtual.ToString()} -- Pertence a {jogadorVencedor.Nome}");
        }

        public bool OnlyBot()
        {
            Jogador[] vetorApoio = filaRodada.ToArray();

            foreach(Jogador item in vetorApoio)
            {
                if (!item.EhBot)
                    return false;
            }
            return true;
        }

        public void JogarPartida(int numCartas, Baralho baralho)
        {
            for (int i = 0; i < numCartas; i++)
            {
                JogarRodada(baralho);
            }
            PerderVidas();
            CriarRanking();
        }

        public void CriarRanking()
        {
            List<CelulaDadosPartida> listaOrdenar = new List<CelulaDadosPartida>();
            for (CelulaDadosPartida i = resultados.Primeiro.Prox; i != null; i = i.Prox)
            {
                i.Score = Pontuacao(i.Elemento);
                listaOrdenar.Add(i);
            }

            listaOrdenar.Sort((A, B) => B.Score.CompareTo(A.Score));

            for (int i = 0; i < listaOrdenar.Count; i++)
            {
                listaOrdenar[i].Elemento.AddRankingPosicao(i + 1);
            }
            if(!OnlyBot())
                ExibirRanking(listaOrdenar);
        }

        public void ExibirRanking(List<CelulaDadosPartida> lista)
        {
            Console.Clear();
            Console.WriteLine("--- Ranking da Partida ---");
            Log.Registrar("Ranking da Partida {");
            Console.WriteLine("Rank\tJogador\t\tPontuação");
            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine($"{i+1}°\t{lista[i].Elemento.Nome} --- {lista[i].Score} pontos");
                Log.Registrar($"\t{i + 1}° -- {lista[i].Elemento.Nome} - {lista[i].Score} pontos");
            }
            Log.Registrar("}");
            Console.WriteLine("\n--- Pressione enter para continuar ---");
            Console.ReadKey();
        }

        public int Pontuacao(Jogador jogador)
        {
            if(jogador.Vidas <= 0)
            {
                Log.Registrar($"Jogador {jogador.Nome} não possui vidas restantes");
                return 0 + resultados.ObterVitorias(jogador);
            }
            else
            {
                int vidasPerdidas = VidasPerdidas(jogador);

                switch (vidasPerdidas)
                {
                    case 0:
                        return 36 + resultados.ObterVitorias(jogador);
                    case 1:
                        return 30 + resultados.ObterVitorias(jogador);
                    case 2:
                        return 24 + resultados.ObterVitorias(jogador);
                    case 3:
                        return 18 + resultados.ObterVitorias(jogador);
                    case 4:
                        return 12 + resultados.ObterVitorias(jogador);
                    case 5:
                        return 6 + resultados.ObterVitorias(jogador);
                    default:
                        throw new Exception("Erro: Número de vidas perdidas inválido.");
                }
            }
        }

        public int VidasPerdidas(Jogador jogador)
        {
            int vidasPerdidas = 0;
            CelulaDadosPartida i;

            for (i = resultados.Primeiro.Prox; i.Elemento != jogador; i = i.Prox) ;

            if (i.Palpite > i.Vitorias)
            {
                vidasPerdidas = i.Palpite - i.Vitorias;
            }
            else if (i.Palpite < i.Vitorias)
            {
                vidasPerdidas = i.Vitorias - i.Palpite;
            }

            Log.Registrar($"Jogador = {jogador.Nome} -- Vidas perdidas na partida = {vidasPerdidas}");
            return vidasPerdidas;
        }

        public void PerderVidas()
        {
            CelulaDadosPartida i;
            for (i = resultados.Primeiro.Prox; i != null; i = i.Prox)
            {
                if (i.Palpite > i.Vitorias)
                {
                    i.Elemento.PerderVida(i.Palpite - i.Vitorias);
                }
                else if (i.Palpite < i.Vitorias)
                {
                    i.Elemento.PerderVida(i.Vitorias - i.Palpite);
                }
            }
        }

        public void ExibirMesa(Dictionary<Jogador,Carta> cartasJogadas)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                          MESA DE JOGO                                      ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                                            ║");
            Console.WriteLine("║                                        >>> FAZENDO <<<                                     ║");
            Console.WriteLine("║                                                                                            ║");
            if (maiorCartaAtual != null)
            {
                ExibirCartaCentral(maiorCartaAtual);
                Console.Write("║");
                for(int i = 0, divisor = 93 - jogadorVencedor.Nome.Length; i < divisor; i++)
                {
                    if (i == divisor / 2)
                        Console.Write($"{jogadorVencedor.Nome}");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine("║");
            }
            else
            {
                Console.WriteLine("║                                                                                            ║");
                Console.WriteLine("║                                                                                            ║");
            }
            Console.WriteLine("║                                                                                            ║");
            Console.WriteLine("╟────────────────────────────────────────────────────────────────────────────────────────────╢");
            for(int i = 0; i < 5; i++)
            {
                string caracteres = string.Empty;
                Console.Write("║    ");
                foreach (var carta in cartasJogadas)
                {
                    if (carta.Value.Naipe == 1 || carta.Value.Naipe == 3)
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{carta.Value.GetLinhaCarta(i)}    ");
                    caracteres += $"{carta.Value.GetLinhaCarta(i)}    ";
                    Console.ResetColor();
                }
                if(caracteres.Length < 88)
                {
                    for(int j = 0; j < 88 - caracteres.Length; j++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("║");
            }
            Console.Write("║    ");
            string tmp = string.Empty;
            foreach (var item in cartasJogadas)
            {
                Jogador jogador = item.Key;
                Console.Write($"{jogador.Nome,-11}");
                tmp += $"{jogador.Nome,-11}";
            }
            if (tmp.Length < 88)
            {
                for (int j = 0; j < 88 - tmp.Length; j++)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine("║");
            Console.WriteLine("║                                                                                            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════╝");
        }

        private void ExibirCartaCentral(Carta carta)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"║                                           ");
                if(carta.Naipe == 1 || carta.Naipe == 3)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{carta.GetLinhaCarta(i)}");
                Console.ResetColor();
                Console.Write($"                                          ║");
                Console.WriteLine();
            }
        }
    }
}