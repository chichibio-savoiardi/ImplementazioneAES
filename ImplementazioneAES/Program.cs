/** ---
 * Implementazione AES in C#
 * Autori: Iuri Antico, Alessio Giacobini
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ImplementazioneAES
{
    public static class Program
    {
        public static string HelpStr { get; private set; } =
            "Strumento di criptazione e decriptazione di file con algoritmo AES128.\n" +
            "Utilizzo : .\\ImplementazioneAES.exe [COMMAND] [FILENAME] [PASSWD]\n" +
            "Esempio : .\\ImplementazioneAES.exe encrypt file.txt abc123'\n" +
            "Comandi possibili:\n" +
            "  encrypt [FILENAME] [PASSWD] : criptazione file\n" +
            "  decrypt [FILENAME] [PASSWD] : decriptazione file\n" +
            "NOTA BENE : Il programma funziona bene solo quando il file e il terminale utilizzano UTF-8";

        public static void Main(string[] args)
        {
            //args = new string[] { "encrypt", "./test.txt", "hobbit" };
            //Start(args);
            Tests.Test();
        }

        private static void Start(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Troppi pochi argomenti, il programma necessita dell'operazione scelta, di un file e di una password.\nEs: '.\\ImplementazioneAES.exe encrypt file.txt abc123'\n" + Program.HelpStr);
                return;
            }

            Console.WriteLine("Wait...");

            switch (args[0])
            {
                case "encrypt":
                    StartEncryption(args);
                    break;
                case "decrypt":
                    StartDecryption(args);
                    break;
                case "help":
                    Console.WriteLine(HelpStr);
                    break;
                default:
                    Console.WriteLine("Operazione non supportata.\n" + HelpStr);
                    break;
            }

            Console.WriteLine("OK");
        }

        private static void StartEncryption(string[] args)
        {
            byte[] file = File.ReadAllBytes(args[1]);
            // Prendo l'hash MD5 della chiave
            byte[] key = MD5.Create().ComputeHash(CipherCore.ChoosenEncoding.GetBytes(args[2]));
            byte[] cipherText = CipherCore.Encrypt(file, key);
            File.WriteAllBytes(args[1] + ".aes", cipherText);
        }

        private static void StartDecryption(string[] args)
        {
            byte[] file = File.ReadAllBytes(args[1]);
            // Prendo l'hash MD5 della chiave
            byte[] key = MD5.Create().ComputeHash(CipherCore.ChoosenEncoding.GetBytes(args[2]));
            byte[] cipherText = CipherCore.Decrypt(file, key);
            File.WriteAllBytes(args[1][0..(args[1].Length - 4)], cipherText);
        }
    }
}