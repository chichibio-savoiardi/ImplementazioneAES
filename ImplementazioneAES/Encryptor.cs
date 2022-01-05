using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementazioneAES;

namespace ImplementazioneAES
{
    internal static class Encryptor
    {
        //sostituisce i byte del file con i valori della sbox
        // Sezione 5.1.1 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] SubBytes(byte[] state)
        {
            byte[] output = (byte[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                // Il valore dello stato è passato attraverso la sbox, e sostituito con un valore specifico
                output[i] = Utility.Sbox[state[i]];
            }
            return output;
        }
        //funzione che posta la posizione dei byte verso sinistra a partire dalla seconda riga
        // Sezione 5.1.2 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] ShiftRows(byte[] state)
        {
            int len = state.Length, sideLen = (int)Math.Sqrt(len);
            byte[] output = new byte[len];

            for (int i = 0; i < sideLen; i++)
            {
                // In modo da trattare l'array come se fosse una matrice
                int row = i * sideLen;
                // Calcolo lo shift della parte dello stato che mi interessa (righa i), con offset i
                byte[] tmp = Utility.ShiftLeft(state[row..(row + sideLen)], i);
                // Copio il risultato nell'output, alla riga corrente
                Buffer.BlockCopy(tmp, 0, output, row, sideLen);
            }

            return output;
        }
        //moltiplicazione nel campo finito di rijndael
        // Sezione 5.1.3 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] MixColumns(byte[] state)
        {
            // Lunghezze comuni
            int len = state.Length, sideLen = (int)Math.Sqrt(len);
            // Preparazione dati.
            // `data` contiene i dati della trasformazione, che verranno ricopiati e convertiti in array1d in `output` alla fine
            byte[,] data = new byte[sideLen, sideLen];
            byte[,] stateMatrix = new byte[sideLen, sideLen];
            Buffer.BlockCopy(state, 0, stateMatrix, 0, len);

            for (int c = 0; c < 4; c++)
            {
                data[0, c] = (byte)(Utility.GMul(0x02, stateMatrix[0, c]) ^ Utility.GMul(0x03, stateMatrix[1, c]) ^ stateMatrix[2, c] ^ stateMatrix[3, c]);
                data[1, c] = (byte)(stateMatrix[0, c] ^ Utility.GMul(0x02, stateMatrix[1, c]) ^ Utility.GMul(0x03, stateMatrix[2, c]) ^ stateMatrix[3, c]);
                data[2, c] = (byte)(stateMatrix[0, c] ^ stateMatrix[1, c] ^ Utility.GMul(0x02, stateMatrix[2, c]) ^ Utility.GMul(0x03, stateMatrix[3, c]));
                data[3, c] = (byte)(Utility.GMul(0x03, stateMatrix[0, c]) ^ stateMatrix[1, c] ^ stateMatrix[2, c] ^ Utility.GMul(0x02, stateMatrix[3, c]));
            }

            byte[] output = new byte[len];
            Buffer.BlockCopy(data, 0, output, 0, len); //copia il contenuto di data in output

            return output;
        }

        // Esegue lo XOR tra lo `state` e la chiave espansa
        // Sezione 5.1.4 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] AddRoundKey(byte[] state, byte[] key, int round)
        {
            // Preparazione dati
            // `l` e' descritto nella specifica di AES
            // `sideLen` e' il la lunghezza del lato dello `state` se fosse interpretato come matrice
            int l = round * CipherCore.NB, sideLen = (int)Math.Sqrt(state.Length);
            byte[] output = new byte[state.Length];
            byte[][] xkey = Utility.KeySchedule(key);
            for (int i = 0; i < sideLen; i++)
            {
                // In modo da trattare l'array come se fosse una matrice
                int row = i * sideLen;
                // Xor dello state colonna per colonna
                byte[] tmp = Utility.XorArray(state[row..(row + sideLen)], xkey[l + i]);
                // Copio il risultato nell'output, alla riga corrente
                Buffer.BlockCopy(tmp, 0, output, row, sideLen);
            }
            return output;
        }
    }
}
