using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementazioneAES;

namespace ImplementazioneAES
{
    internal static class Utility
    {
        //e' la matrice che fornisce le sostituzioni per la funzione SubBytes
        internal static byte[] Sbox { get; } = new byte[256] {
                0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76,
                0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0,
                0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15,
                0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75,
                0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84,
                0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf,
                0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8,
                0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2,
                0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73,
                0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb,
                0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79,
                0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08,
                0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a,
                0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e,
                0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf,
                0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16};
        //e' la matrice che applicata al invSubBytes restituisce i valori del file
        internal static byte[] InvSbox { get; } = new byte[256] {
                0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb,
                0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb,
                0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e,
                0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25,
                0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,
                0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84,
                0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06,
                0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b,
                0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73,
                0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,
                0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b,
                0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4,
                0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f,
                0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef,
                0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,
                0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d};
        private static byte[] Rcon { get; } = new byte[256] {
                0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a,
                0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39,
                0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a,
                0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8,
                0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef,
                0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc,
                0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b,
                0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3,
                0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94,
                0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20,
                0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35,
                0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f,
                0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04,
                0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63,
                0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd,
                0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d};
        //spostamento a sinistra dei byte in base alla variabile times che varia in base alla riga

        // Dato un'array, fornisce una stringa con gli elementi che lo compongono
        internal static string ArrayToString(this byte[] arr, string mod = "")
        {
            string output = "";
            foreach (var item in arr)
            {
                output += item.ToString(mod) + " ";
            }
            return output;
        }

        public static T[] Concat<T>(this T[] x, T[] y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");
            int oldLen = x.Length;
            Array.Resize<T>(ref x, x.Length + y.Length);
            Array.Copy(y, 0, x, oldLen, y.Length);
            return x;
        }

        // ShiftDown, puo essere considerata ShiftLeft se si ordina in colonne invece che righe
        internal static byte[] ShiftLeft(byte[] arr, int times)
        {
            byte[] ShiftLeftOne(byte[] arr)
            {
                int len = arr.Length;
                byte[] output = new byte[len];
                byte tmp = arr[0];
                for (int i = 0; i < len - 1; i++)
                {
                    output[i] = arr[i + 1];
                }
                output[len - 1] = tmp;

                return output;
            }

            byte[] output = (byte[])arr.Clone();
            for (int i = 0; i < times; i++)
            {
                output = ShiftLeftOne(output);
            }
            return output;
        }
        //spostamento a destra dei byte in base alla variabile times che varia in base alla riga 
        internal static byte[] ShiftRight(byte[] arr, int times)
        {
            byte[] ShiftRightOne(byte[] arr)
            {
                int len = arr.Length;
                byte[] output = new byte[len];
                byte tmp = arr[len - 1];
                for (int i = len - 1; i > 0; i--)
                {
                    output[i] = arr[i - 1];
                }
                output[0] = tmp;

                return output;
            }

            byte[] output = (byte[])arr.Clone();
            for (int i = 0; i < times; i++)
            {
                output = ShiftRightOne(output);
            }
            return output;
        }
        //funzione che non permette ad un numero il superamento di 256 altrimenti riparte da zero
        internal static byte GMul(byte a, byte b)
        {
            byte p = 0;

            for (int counter = 0; counter < 8; counter++)
            {
                if ((b & 1) != 0)
                {
                    p ^= a; //Xor tra p e a assegnando i valori a p
                }

                bool hi_bit_set = (a & 0x80) != 0;
                a <<= 1; // shift a sinistra di uno
                if (hi_bit_set)
                {
                    a ^= 0x1B; /* x^8 + x^4 + x^3 + x + 1 */
                }
                b >>= 1;
            }

            return p;
        }

        // Genera 10 chiavi aggiuntive in base a quella data
        // La funzione e' una copia (quasi) di quella descritta nella specifica di AES
        // Sezione 5.2, Figura 11 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[][] KeySchedule(byte[] key)
        {
            int N = 4 * CipherCore.NK, B = (CipherCore.NB * (CipherCore.NR + 1)), i = 0;
            int wordLen = 4;
            byte[][] w = new byte[B][];
            byte[] temp = new byte[wordLen];

            while (i < CipherCore.NK)
            {
                w[i] = new byte[CipherCore.NB];
                Buffer.BlockCopy(key[(4 * i)..(4 * i + 4)], 0, w[i], 0, CipherCore.NB);
                i++;
            }
            i = CipherCore.NK;

            while (i < B)
            {
                Buffer.BlockCopy(w[i - 1], 0, temp, 0, wordLen);

                if ((i % CipherCore.NK) == 0)
                {
                    temp = KeyScheduleCore(temp, i / CipherCore.NK);
                }
                else if ((CipherCore.NK > 6) && ((i % CipherCore.NK) == 4))
                {
                    // Sostituzione con sbox, fatta con for perche' temp e' un array
                    for (int j = 0; j < temp.Length; j++)
                    {
                        temp[i] = Sbox[temp[i]];
                    }
                }
                w[i] = XorArray(w[i - CipherCore.NK], temp);
                i++;
            }

            byte[][] w2 = KeyScheduleNormalize(w);

            return w2;
        }

        // trasforma l'array da w[44][4] a w2[11][16]
        // in modo che ogni righa della matrice corrisponda a una chiave completa invece che solo 1/4
        internal static byte[][] KeyScheduleNormalize(byte[][] w)
        {
            int len = w.Length, oldLen = 4, newLen = 16, newRows = len / oldLen;
            byte[][] output = new byte[newRows][];

            for (int i = 0; i < newRows; i++)
            {
                int row = i * oldLen;
                byte[] temp = new byte[newLen];
                byte[] cluster = new byte[0];
                for (int j = 0; j < oldLen; j++)
                {
                    int jrow = j * oldLen;
                    cluster = cluster.Concat(w[row + j]);
                }
                Buffer.BlockCopy(cluster, 0, temp, 0, newLen);
                output[i] = temp;
            }

            return output;
        }

        // Nello specifico la linea seguente, illustrata a Sezione 5.2, Figura 11
        // temp = SubWord(RotWord(temp)) xor Rcon[i/Nk]
        // E' implementata nella sottostante funzione
        // Sezione 5.2, Figura 11 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        private static byte[] KeyScheduleCore(byte[] key, int i_rcon)
        {
            byte[] output = (byte[])key[0..4].Clone();
            output = ShiftLeft(output, 1);
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Sbox[output[i]];
            }
            output[0] ^= Rcon[i_rcon];
            return output;
        }

        private static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        // Funzione che prende un'array di byte ed esegue l'operazione XOR (^) sui singoli elementi
        internal static byte[] XorArray(byte[] left, byte[] right)
        {
            int smallest = Math.Min(left.Length, right.Length);
            byte[] output = new byte[smallest];

            for (int i = 0; i < smallest; i++)
            {
                output[i] = (byte)(left[i] ^ right[i]);
            }

            return output;
        }

        // divide l'input in blocchi di lunghezza arrLen
        internal static byte[][] StringToByteMatrix(string input, int arrLen)
        {
            byte[] bytes = CipherCore.ChoosenEncoding.GetBytes(input);
            int len;

            if (bytes.Length % arrLen == 0)
            {
                len = bytes.Length / arrLen;
            }
            else
            {
                int newLen = bytes.Length + (arrLen - (bytes.Length % arrLen));
                Array.Resize(ref bytes, newLen);
                len = bytes.Length / arrLen;
            }

            byte[][] output = new byte[len][];

            for (int i = 0; i < len; i++)
            {
                int currPos = i * arrLen;
                output[i] = new byte[arrLen];
                Array.Copy(bytes, currPos, output[i], 0, arrLen);
            }

            return output;
        }

        // trasforma dei blocchi di byte in una stringa
        internal static string ByteMatrixToString(byte[][] bytes)
        {
            string output = "";
            foreach (var arr in bytes)
            {
                output += CipherCore.ChoosenEncoding.GetString(arr);
            }
            return output;
        }

        // trasforma un'array di byte in una matrice 2D di byte
        internal static byte[][] ByteArrayToMatrix(byte[] input, int arrLen)
        {
            byte[] bytes = (byte[])input.Clone();
            int len;

            if (bytes.Length % arrLen == 0)
            {
                len = bytes.Length / arrLen;
            }
            else
            {
                int newLen = bytes.Length + (arrLen - (bytes.Length % arrLen));
                Array.Resize(ref bytes, newLen);
                len = bytes.Length / arrLen;
            }

            byte[][] output = new byte[len][];

            for (int i = 0; i < len; i++)
            {
                int currPos = i * arrLen;
                output[i] = new byte[arrLen];
                Array.Copy(bytes, currPos, output[i], 0, arrLen);
            }

            return output;
        }

        // trasforma una matrice 2D di byte in un'array di byte
        internal static byte[] ByteMatrixToArray(byte[][] bytes)
        {
            byte[] output = new byte[bytes.Length * bytes[0].Length];
            foreach (var arr in bytes)
            {
                output = output.Concat(arr);
            }
            return output;
        }

        internal static T[,] To2DArray<T>(this T[] arr)
        {
            int sideLen = (int)Math.Sqrt(arr.Length);
            T[,] mat = new T[sideLen, sideLen];

            Buffer.BlockCopy(arr, 0, mat, 0, arr.Length);

            return mat;
        }

        internal static T[] To1DArray<T>(this T[,] mat)
        {
            T[] arr = new T[mat.GetLength(0) * mat.GetLength(1)];
            int i = 0;

            foreach (var item in mat)
            {
                arr[i++] = item;
            }

            return arr;
        }
    }
}
