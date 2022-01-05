using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ImplementazioneAES
{
    internal static class CipherCore
    {
        internal static int NK { get; private set; } = 4;
        internal static int NB { get; private set; } = 4;
        internal static int NR { get; private set; } = 10;

        // chiama `Cipher()` su tutti i blocchi dell'input
        internal static string Encrypt(string input, string key)
        {
            // La chiave e l'input sono trasformati in un array di byte
            byte[] bkey = Encoding.Unicode.GetBytes(key);
            byte[][] bytes = StringToByteMatrix(input, 16);
            // L'input e' criptato con la chiave
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Cipher(bytes[i], bkey);
            }

            // L'input è riconvertito in stringa
            string output = ByteMatrixToString(bytes);
            return output;
        }

        // esegue la criptazione su un blocco `input`
        // Sezione 5.1, Figura 5
        // Per maggiori informazioni: https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        private static byte[] Cipher(byte[] input, byte[] key)
        {
            byte[] output = (byte[])input.Clone();

            output = Encryptor.AddRoundKey(output, key, 0);

            for (int i = 1; i < NR; i++)
            {
                output = Encryptor.SubBytes(output);
                output = Encryptor.ShiftRows(output);
                output = Encryptor.MixColumns(output);
                output = Encryptor.AddRoundKey(output, key, i);
            }

            output = Encryptor.SubBytes(output);
            output = Encryptor.ShiftRows(output);
            output = Encryptor.AddRoundKey(output, key, NR);

            return output;
        }

        // chiama `InvCipher()` su tutti i blocchi dell'input
        internal static string Decrypt(string input, string key)
        {
            // equivalente alla funzione `Encrypt()`, ma al posto di una chiamata a `Cipher` c'e' una chiamata a `InvCipher()`
            byte[] bkey = Encoding.Unicode.GetBytes(key);
            byte[][] bytes = StringToByteMatrix(input, 16);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = InvCipher(bytes[i], bkey);
            }

            string output = ByteMatrixToString(bytes);
            return output;
        }

        // esegue la decriptazione su un blocco `input`
        // Sezione 5.3, Figura 12 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        private static byte[] InvCipher(byte[] input, byte[] key)
        {
            byte[] output = (byte[])input.Clone();

            output = Encryptor.AddRoundKey(output, key, 0);

            for (int i = 1; i < NR; i++)
            {
                output = Decryptor.InvShiftRows(output);
                output = Decryptor.InvSubBytes(output);
                output = Encryptor.AddRoundKey(output, key, i);
                output = Decryptor.InvMixColumns(output);
            }

            output = Decryptor.InvShiftRows(output);
            output = Decryptor.InvSubBytes(output);
            output = Encryptor.AddRoundKey(output, key, NR);

            return output;
        }

        // divide l'input in blocchi di lunghezza arrLen
        internal static byte[][] StringToByteMatrix(string input, int arrLen)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(input);
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
                output += Encoding.Unicode.GetString(arr);
            }
            return output;
        }
    }
}
