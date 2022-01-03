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
            //Start(args);
            Test(args);
        }

        private static void Start(string[] args)
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
                case "help":
                    Console.WriteLine(Program.HelpStr);
                    break;
                default:
                    Console.WriteLine("Operazione non supportata.\n" + Program.HelpStr);
                    break;
            }
        }

        private static void Test(string[] args)
        {
            Console.WriteLine("\nTest SubBytes():\n");
            TestSubBytes(args);
            Console.WriteLine("\nTest ShiftRows():\n");
            TestShiftRows(args);
            Console.WriteLine("\nTest MixColumns():\n");
            TestMixColumns(args);
            Console.WriteLine("\nTest AddRoundKey():\n");
            TestAddRoundKey(args);
        }

        private static void TestSubBytes(string[] args)
        {
            /*var chars = args[0].ToCharArray();
            byte[] bytes = new byte[16];
            for (int i = 0; i < bytes.Length && i < chars.Length; i++)
            {
                bytes[i] = (byte)chars[i];
            }

            foreach (var r in bytes)
            {
                Console.Write(r + " ");
            }

            Console.WriteLine();

            byte[] res = Encryptor.SubBytes(bytes);

            foreach (var r in res)
            {
                Console.Write(r + " ");
            }*/

            byte en = Encryptor.SubBytes(new byte[] { 0x9a })[0];
            byte de = Decryptor.InvSubBytes(new byte[] { en })[0];

            Console.WriteLine($"en : {en}");
            Console.WriteLine($"de : {de}");
            Console.WriteLine($"en == 0xb8 : {en == 0xb8}");
            Console.WriteLine($"de == 0x9a : {de == 0x9a}");
            Console.WriteLine();
        }

        private static void TestShiftRows(string[] args)
        {
            byte[] arr = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };

            Console.WriteLine("Bytes prima:");
            foreach (var a in arr)
            {
                Console.Write(a + " ");
            }

            byte[] res = Encryptor.ShiftRows(arr);

            Console.WriteLine("\nBytes dopo:");
            foreach (var r in res)
            {
                Console.Write(r + " ");
            }

            byte[] inv = Decryptor.ShiftRows(res);

            Console.WriteLine("\nBytes reinvertiti:");
            foreach (var r in inv)
            {
                Console.Write(r + " ");
            }
            Console.WriteLine();
        }

        private static void TestMixColumns(string[] args)
        {
            byte[] arr = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };

            Console.WriteLine("Bytes prima:");
            foreach (var a in arr)
            {
                Console.Write(a + " ");
            }

            byte[] res = Encryptor.MixColumns(arr);

            Console.WriteLine("\nBytes dopo:");
            foreach (var r in res)
            {
                Console.Write(r + " ");
            }

            byte[] inv = Decryptor.InvMixColumns(res);

            Console.WriteLine("\nBytes reinvertiti:");
            foreach (var i in inv)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        private static void TestAddRoundKey(string[] args)
        {
            byte[] arr = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };
            byte[] key = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };

            Console.WriteLine("Key:");
            foreach (var k in key)
            {
                Console.Write(k + " ");
            }
            Console.WriteLine("\nBytes prima:");
            foreach (var a in arr)
            {
                Console.Write(a + " ");
            }

            byte[] res = Encryptor.AddRoundKey(arr, key);

            Console.WriteLine("\nBytes dopo:");
            foreach (var r in res)
            {
                Console.Write(r + " ");
            }

            byte[] inv = Decryptor.InvAddRoundKey(res, key);

            Console.WriteLine("\nBytes reinvertiti:");
            foreach (var i in inv)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }
}