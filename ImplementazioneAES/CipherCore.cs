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
            byte[][] xkey = Utility.KeySchedule(bkey);
            byte[][] bytes = Utility.StringToByteMatrix(input, 16);
            // L'input e' criptato con la chiave
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Cipher(bytes[i], xkey);
            }

            // L'input è riconvertito in stringa
            string output = Utility.ByteMatrixToString(bytes);
            return output;
        }

        // esegue la criptazione su un blocco `input`
        // Sezione 5.1, Figura 5
        // Per maggiori informazioni: https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] Cipher(byte[] input, byte[][] key)
        {
            byte[] output = (byte[])input.Clone();

            output = Encryptor.AddRoundKey(output, key[0]);

            for (int i = 1; i < NR; i++)
            {
                output = Encryptor.SubBytes(output);
                output = Encryptor.ShiftRows(output);
                output = Encryptor.MixColumns(output);
                output = Encryptor.AddRoundKey(output, key[i]);
            }
            
            output = Encryptor.SubBytes(output);
            output = Encryptor.ShiftRows(output);
            output = Encryptor.AddRoundKey(output, key[NR]);

            return output;
        }

        // chiama `InvCipher()` su tutti i blocchi dell'input
        internal static string Decrypt(string input, string key)
        {
            // equivalente alla funzione `Encrypt()`, ma al posto di una chiamata a `Cipher` c'e' una chiamata a `InvCipher()`
            byte[] bkey = Encoding.Unicode.GetBytes(key);
            byte[][] xkey = Utility.KeySchedule(bkey);
            byte[][] bytes = Utility.StringToByteMatrix(input, 16);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = InvCipher(bytes[i], xkey);
            }

            string output = Utility.ByteMatrixToString(bytes);
            return output;
        }

        // esegue la decriptazione su un blocco `input`
        // Sezione 5.3, Figura 12 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] InvCipher(byte[] input, byte[][] key)
        {
            byte[] output = (byte[])input.Clone();

            output = Encryptor.AddRoundKey(output, key[0]);

            for (int i = 1; i < NR; i++)
            {
                output = Decryptor.InvShiftRows(output);
                output = Decryptor.InvSubBytes(output);
                output = Encryptor.AddRoundKey(output, key[i]);
                output = Decryptor.InvMixColumns(output);
            }
            
            output = Decryptor.InvShiftRows(output);
            output = Decryptor.InvSubBytes(output);
            output = Encryptor.AddRoundKey(output, key[NR]);

            return output;
        }
    }
}
