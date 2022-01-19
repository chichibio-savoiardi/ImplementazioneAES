using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementazioneAES
{
    internal class Tests
    {
        public static byte[] Arr { get; set; } = new byte[16] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
        public static byte[] Key { get; set; } = new byte[16] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f };
        public static byte[][] Xkey { get; set; } = Utility.KeySchedule(Key);

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
            Console.WriteLine("\nTest ByteMatrixToArray():\n");
            TestMatrixToArray();
            Console.WriteLine("\nTest Cipher:\n");
            TestCipher();
            TestCipher1();
            Console.WriteLine("\nTest Encrypt:\n");
            TestEncrypt();
        }

        private static void TestSubBytes()
        {
            byte[] en = Encryptor.SubBytes(Arr);
            byte[] de = Decryptor.InvSubBytes(en);

            Console.WriteLine($"start : {Arr.ArrayToString()}");
            Console.WriteLine($"enc : {en.ArrayToString()}");
            Console.WriteLine($"dec : {de.ArrayToString()}");
            Console.WriteLine();
        }

        private static void TestShiftRows()
        {
            Console.WriteLine("Bytes prima:");
            foreach (var a in Arr)
            {
                Console.Write(a + " ");
            }

            byte[] res = Encryptor.ShiftRows(Arr);

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
            Console.WriteLine("Bytes prima:");
            foreach (var a in Arr)
            {
                Console.Write(a + " ");
            }

            byte[] res = Encryptor.MixColumns(Arr);

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
            Console.WriteLine("Key:");
            foreach (var k in Key)
            {
                Console.Write(k + " ");
            }
            Console.WriteLine("\nBytes prima:");
            foreach (var a in Arr)
            {
                Console.Write(a + " ");
            }

            byte[] res = Encryptor.AddRoundKey(Arr, Key);

            Console.WriteLine("\nBytes dopo:");
            foreach (var r in res)
            {
                Console.Write(r + " ");
            }

            byte[] inv = Encryptor.AddRoundKey(res, Key);

            Console.WriteLine("\nBytes reinvertiti:");
            foreach (var i in inv)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        private static void TestKeySchedule()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            byte[] arr = new byte[16] { 0x2b, 0x7e, 0x15, 0x16, 0x28, 0xae, 0xd2, 0xa6, 0xab, 0xf7, 0x15, 0x88, 0x09, 0xcf, 0x4f, 0x3c };
            Console.WriteLine("\nBytes prima:");
            Console.WriteLine(arr.ArrayToString());

            byte[][] res = Utility.KeySchedule(arr);

            Console.WriteLine("\nBytes dopo:");
            foreach (var r in res)
            {
                Console.WriteLine(r.ArrayToString());
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime KeySchedule " + elapsedTime);
        }

        private static void TestCipher()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            byte[] arr = new byte[16] { 0x32, 0x43, 0xf6, 0xa8, 0x88, 0x5a, 0x30, 0x8d, 0x31, 0x31, 0x98, 0xa2, 0xe0, 0x37, 0x07, 0x34 };
            byte[] key = new byte[16] { 0x2b, 0x7e, 0x15, 0x16, 0x28, 0xae, 0xd2, 0xa6, 0xab, 0xf7, 0x15, 0x88, 0x09, 0xcf, 0x4f, 0x3c };
            byte[][] xkey = Utility.KeySchedule(key);
            Console.WriteLine(arr.ArrayToString());
            byte[] res = CipherCore.Cipher(arr, xkey);
            Console.WriteLine(res.ArrayToString());
            byte[] inv = CipherCore.InvCipher(res, xkey);
            Console.WriteLine(inv.ArrayToString());

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime TestCipher " + elapsedTime);
        }
        private static void TestCipher1()
        {
            byte[][] xkey = Utility.KeySchedule(Key);
            Console.WriteLine(Arr.ArrayToString());
            byte[] res = CipherCore.Cipher(Arr, xkey);
            Console.WriteLine(res.ArrayToString());
            byte[] inv = CipherCore.InvCipher(res, xkey);
            Console.WriteLine(inv.ArrayToString());
        }

        private static void TestEncrypt()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            string str = "my name is giovanni giorgio";
            byte[] cipherText = CipherCore.Encrypt(CipherCore.ChoosenEncoding.GetBytes(str), Key);
            byte[] clearText = CipherCore.Decrypt(cipherText, Key);
            Console.WriteLine($"key : {Key.ArrayToString()}");
            Console.WriteLine($"str : {str}");
            Console.WriteLine($"cipherText : {CipherCore.ChoosenEncoding.GetString(cipherText)}");
            Console.WriteLine($"clearText : {CipherCore.ChoosenEncoding.GetString(clearText)}");

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime TestCipher1 " + elapsedTime);
        }

        private static void TestMatrixToArray()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var longkey = Utility.ByteMatrixToArray(Xkey);
            var shortkeys = Utility.ByteArrayToMatrix(longkey, 16);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime MatrixToArray " + elapsedTime);
        }
    }
}
