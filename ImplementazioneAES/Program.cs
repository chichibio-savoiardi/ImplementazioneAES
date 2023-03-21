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
        public static string[] HelpStr { get; private set; } = new string[] {
@"Strumento di criptazione e decriptazione di file con algoritmo AES128.
Utilizzo : .\ImplementazioneAES.exe [COMMAND] [FILENAME] [PASSWD]
Esempio : .\ImplementazioneAES.exe encrypt file.txt abc123'
Comandi possibili:
  encrypt [FILENAME] [PASSWD] : criptazione file
  decrypt [FILENAME] [PASSWD] : decriptazione file
  help [PAGE] : manuale, pagine da 0 a 5",
@"Indice:
0 : Pagina principale
1 : Indice (questa)
2 : Considerazioni
3 : Dinamica
4 : File
5 : Password",
@"Considerazioni:
Il programma è un compito per scuola, quindi non è sicuro, è fatto solo per scopi dimostrativi.
Il programma funziona bene quando sia il file che il terminale sono codificati con UTF-8.
Il programma cripta e decripta esclusivamente con AES128.
Si consiglia di pulire la console con 'cls' dopo l'utilizzo, per nascondere la password usata.",
@"Dinamica:
Encrypt : File -> Chiave -> File Cifrato.
Decrypt : File Cifrato -> Chiave -> File.
Una volta avviata la criptazione o decriptazione, il file viene sottomesso alla procedura con la chiave.
Si consiglia di non utilizzare il file mentre il programma lavora.
Dopo la criptazione verrà restituito un file che finisce per '.aes'.
Dopo la decriptazione l'estensione '.aes' verrà tolta.",
@"File:
L'algortitmo non può decriptare file criptati con AES192 o AES256.
Non ci sono altre particolari restrizioni sul tipo di file da criptare/decriptare.",
@"Password:
La password è una parola, che viene poi hashata con MD5 per creare la chiave da 128 bit necessaria per l'algoritmo."};

        public static void Main(string[] args)
        {
            Start(args);
            //Tests.Test();
        }

        private static void Start(string[] args)
        {
            if (args.Length < 1)
            {
                GiveHelp("Scrivi un comando.");
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
                    if (args.Length < 2)
                        args = args.Concat(new string[] { "0" });
                    GiveHelp("", int.Parse(args[1]));
                    break;
                case "test":
                    Tests.Test();
                    break;
                default:
                    GiveHelp("Operazione non supportata");
                    break;
            }

            Console.WriteLine("OK");
        }

        private static void StartEncryption(string[] args)
        {
            if (args.Length < 3)
                GiveHelp("Troppi pochi argomenti, il comando necessita un file e una password.");

            byte[] file = File.ReadAllBytes(args[1]);
            // Prendo l'hash MD5 della chiave
            byte[] key = MD5.Create().ComputeHash(CipherCore.ChoosenEncoding.GetBytes(args[2]));
            byte[] cipherText = CipherCore.Encrypt(file, key);
            File.WriteAllBytes(args[1] + ".aes", cipherText);
        }

        private static void StartDecryption(string[] args)
        {
            if (args.Length < 3)
                GiveHelp("Troppi pochi argomenti, il comando necessita un file e una password.");

            byte[] file = File.ReadAllBytes(args[1]);
            // Prendo l'hash MD5 della chiave
            byte[] key = MD5.Create().ComputeHash(CipherCore.ChoosenEncoding.GetBytes(args[2]));
            byte[] cipherText = CipherCore.Decrypt(file, key);
            string newName = args[1].Replace(".aes", "");
            File.WriteAllBytes(newName, cipherText);
        }

        private static void GiveHelp(string msg = "", int page = 0)
        {
            if (page >= HelpStr.Length || page < 0)
            {
                Console.WriteLine("Pagina non esistente.");
                page = 0;
            }
            Console.WriteLine(HelpStr[page]);
            Console.WriteLine("---");
            Console.WriteLine(msg);
            Environment.Exit(0);
        }
    }
}