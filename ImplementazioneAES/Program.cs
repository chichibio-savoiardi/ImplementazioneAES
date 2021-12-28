namespace ImplementazioneAES
{
    public static class Program
    {
        public static string HelpStr { get; private set; } =
            "Strumento di criptazione e decriptazione di file con algoritmo AES128.\n" +
            "Operazioni possibili:\n" +
            "  encrypt <file> : criptazione file\n" +
            "  decrypt <file> : decriptazione file\n";
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Troppi pochi argomenti, il programma necessita dell'operazione scelta e di un file.\nEs: '.\\ImplementazioneAES.exe encrypt file.txt'\n" + Program.HelpStr);
                return;
            }

            switch (args[0])
            {
                case "encrypt":
                    Console.WriteLine("TODO, encrypt file " + args[1]);
                    break;
                case "decrypt":
                    Console.WriteLine("TODO, decrypt file " + args[1]);
                    break;
                default:
                    Console.WriteLine("Operazione non supportata.\n" + Program.HelpStr);
                    break;
            }
        }
    }
}