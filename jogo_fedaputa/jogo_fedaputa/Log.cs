using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_fedaputa
{
    public static class Log
    {
        private static string nomeArquivoLog = "log.txt";

        public static void Registrar(string mensagem)
        {
            try
            {
                StreamWriter sw = new StreamWriter(nomeArquivoLog, true, Encoding.UTF8);
                {
                    sw.WriteLine($"{DateTime.Now}: {mensagem}");
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public static void IniciarNovoLog()
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(nomeArquivoLog, true, Encoding.UTF8);
                {
                    streamWriter.WriteLine("=======================================================================================");
                    streamWriter.WriteLine($"--- Novo jogo iniciado: {DateTime.Now} ---");
                    streamWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public static void ExibirImagem()
        {
            try
            {
                StreamReader caminho = new StreamReader("arte.txt");
                {
                    string linha;
                    while((linha = caminho.ReadLine()) != null )
                    {
                        Console.WriteLine(linha);
                    }
                    caminho.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
