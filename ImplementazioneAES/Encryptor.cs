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
            Buffer.BlockCopy(data, 0, output, 0, len);

            return output;
        }

        internal static byte[] AddRoundKey(byte[] state, byte[] key)
        {
            byte[] output = new byte[state.Length];
            //TODO
            return output;
        }
    }
}
