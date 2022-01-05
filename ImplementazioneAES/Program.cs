/**
 * 
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
            "Operazioni possibili:\n" +
            "  encrypt <file> : criptazione file\n" +
            "  decrypt <file> : decriptazione file\n";

        public static void Main(string[] args)
        {
            //Start(args);
            Test();
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
                    Console.WriteLine(HelpStr);
                    break;
                default:
                    Console.WriteLine("Operazione non supportata.\n" + HelpStr);
                    break;
            }
        }

        private static void Test()
        {
            Console.WriteLine("\nTest SubBytes():\n");
            TestSubBytes();
            Console.WriteLine("\nTest ShiftRows():\n");
            TestShiftRows();
            Console.WriteLine("\nTest MixColumns():\n");
            TestMixColumns();
            Console.WriteLine("\nTest AddRoundKey():\n");
            TestAddRoundKey();
            Console.WriteLine("\nTest KeySchedule():\n");
            TestKeySchedule();
            Console.WriteLine("\nTest ToByteArray():\n");
            TestToByteArray();
            Console.WriteLine("\nTest Enryption/Decryption():\n");
            TestEncrypt();
        }

        private static void TestSubBytes()
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

        private static void TestShiftRows()
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

            byte[] inv = Decryptor.InvShiftRows(res);

            Console.WriteLine("\nBytes reinvertiti:");
            foreach (var r in inv)
            {
                Console.Write(r + " ");
            }
            Console.WriteLine();
        }

        private static void TestMixColumns()
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
            foreach (var r in inv)
            {
                Console.Write(r + " ");
            }
            Console.WriteLine();
        }

        private static void TestAddRoundKey()
        {
            byte[] arr = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
            byte[] key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f };

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

            byte[] res = Encryptor.AddRoundKey(arr, key, 0);

            Console.WriteLine("\nBytes dopo:");
            foreach (var r in res)
            {
                Console.Write(r + " ");
            }

            byte[] inv = Encryptor.AddRoundKey(res, key, 0);

            Console.WriteLine("\nBytes reinvertiti:");
            foreach (var i in inv)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        private static void TestKeySchedule()
        {
            byte[] arr = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };
            Console.WriteLine("\nBytes prima:");
            foreach (var a in arr)
            {
                Console.Write(a + " ");
            }

            byte[][] res = Utility.KeySchedule(arr);

            Console.WriteLine("\nBytes dopo:");
            foreach (var r in res)
            {
                foreach (var i in r)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }
        }

        private static void TestToByteArray()
        {
            string a = "abcdefghijklmnopqrstuvwxyz123456789";

            Console.WriteLine(a);

            byte[][] res = CipherCore.StringToByteMatrix(a, 16);

            foreach (var arr in res)
            {
                foreach (var item in arr)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }

            string inv = CipherCore.ByteMatrixToString(res);

            Console.WriteLine(inv);
        }

        private static void TestEncrypt()
        {
            byte[] key = new byte[16] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f };
            string keystr = Encoding.Unicode.GetString(key);
            string str = "my name is giovanni giorgio, but everybody calls me giorgio";
            string cipherText = CipherCore.Encrypt(str, keystr);
            string clearText = CipherCore.Decrypt(cipherText, keystr);
            Console.WriteLine($"text : {str}");
            Console.WriteLine($"key : {keystr}");
            Console.WriteLine($"ciphertext : {cipherText}");
            Console.WriteLine($"cleartext : {clearText}");

        }
    }
}