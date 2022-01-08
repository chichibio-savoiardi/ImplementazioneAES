using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementazioneAES
{
    internal class Tests
    {
        internal static void Test()
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
            Console.WriteLine("\nTest Cipher:\n");
            TestCipher();
            TestCipher1();
            Console.WriteLine("\nTest Encrypt:\n");
            TestEncrypt();
        }

        private static void TestSubBytes()
        {
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
            byte[] arr = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };

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
            byte[] arr = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };

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

            byte[] res = Encryptor.AddRoundKey(arr, key);

            Console.WriteLine("\nBytes dopo:");
            foreach (var r in res)
            {
                Console.Write(r + " ");
            }

            byte[] inv = Encryptor.AddRoundKey(res, key);

            Console.WriteLine("\nBytes reinvertiti:");
            foreach (var i in inv)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        private static void TestKeySchedule()
        {
            byte[] arr = new byte[16] { 0x2b, 0x7e, 0x15, 0x16, 0x28, 0xae, 0xd2, 0xa6, 0xab, 0xf7, 0x15, 0x88, 0x09, 0xcf, 0x4f, 0x3c };
            Console.WriteLine("\nBytes prima:");
            foreach (var a in arr)
            {
                Console.Write(a.ToString() + " ");
            }

            byte[][] res = Utility.KeySchedule(arr);

            Console.WriteLine("\nBytes dopo:");
            foreach (var r in res)
            {
                foreach (var i in r)
                {
                    Console.Write(i.ToString() + " ");
                }
                Console.WriteLine();
            }
        }

        private static void TestCipher()
        {
            byte[] arr = new byte[16] { 0x32, 0x43, 0xf6, 0xa8, 0x88, 0x5a, 0x30, 0x8d, 0x31, 0x31, 0x98, 0xa2, 0xe0, 0x37, 0x07, 0x34 };
            byte[] key = new byte[16] { 0x2b, 0x7e, 0x15, 0x16, 0x28, 0xae, 0xd2, 0xa6, 0xab, 0xf7, 0x15, 0x88, 0x09, 0xcf, 0x4f, 0x3c };
            byte[][] xkey = Utility.KeySchedule(key);
            Console.WriteLine(arr.ArrayToString());
            byte[] res = CipherCore.Cipher(arr, xkey);
            Console.WriteLine(res.ArrayToString());
            byte[] inv = CipherCore.InvCipher(res, xkey);
            Console.WriteLine(inv.ArrayToString());
        }
        private static void TestCipher1()
        {
            byte[] arr = new byte[16] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
            byte[] key = new byte[16] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f };
            byte[][] xkey = Utility.KeySchedule(key);
            Console.WriteLine(arr.ArrayToString());
            byte[] res = CipherCore.Cipher(arr, xkey);
            Console.WriteLine(res.ArrayToString());
            byte[] inv = CipherCore.InvCipher(res, xkey);
            Console.WriteLine(inv.ArrayToString());
        }

        private static void TestEncrypt()
        {
            string str = "my name is giovanni giorgio";
            byte[] key = new byte[16] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f };
            byte[] cipherText = CipherCore.Encrypt(CipherCore.ChoosenEncoding.GetBytes(str), key);
            byte[] clearText = CipherCore.Decrypt(cipherText, key);
            Console.WriteLine($"key : {key.ArrayToString()}");
            Console.WriteLine($"str : {str}");
            Console.WriteLine($"cipherText : {CipherCore.ChoosenEncoding.GetString(cipherText)}");
            Console.WriteLine($"clearText : {CipherCore.ChoosenEncoding.GetString(clearText)}");
        }
    }
}
