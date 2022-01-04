using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementazioneAES
{
    internal static class Decryptor
    {
        //sostituisce i byte applicati con la sbox con i valori della invsbox 
        internal static byte[] InvSubBytes(byte[] state)
        {
            byte[] after = (byte[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                // Il valore dello stato è passato attraverso la invSbox, e sostituito con un valore specifico
                after[i] = Utility.InvSbox[state[i]];
            }
            return after;
        }

        //
        //funzione che posta la posizione dei byte verso destra a partire dalla seconda riga per riordinare la matrice 
        internal static byte[] InvShiftRows(byte[] state)
        {
            int len = state.Length, sideLen = (int)Math.Sqrt(len);
            byte[] output = new byte[len];

            for (int i = 0; i < sideLen; i++)
            {
                // In modo da trattare l'array come se fosse una matrice
                int row = i * sideLen;
                // Calcolo lo shift della parte dello stato che mi interessa (righa i), con offset i
                byte[] tmp = Utility.ShiftRight(state[row..(row + sideLen)], i);
                // Copio il risultato nell'output, alla riga corrente
                Buffer.BlockCopy(tmp, 0, output, row, sideLen);
            }

            return output;
        }

        internal static byte[] InvMixColumns(byte[] state)
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
                data[0, c] = (byte)(Utility.GMul(0x0e, stateMatrix[0, c]) ^ Utility.GMul(0x0b, stateMatrix[1, c]) ^ Utility.GMul(0x0d, stateMatrix[2, c]) ^ Utility.GMul(0x09, stateMatrix[3, c]));
                data[1, c] = (byte)(Utility.GMul(0x09, stateMatrix[0, c]) ^ Utility.GMul(0x0e, stateMatrix[1, c]) ^ Utility.GMul(0x0b, stateMatrix[2, c]) ^ Utility.GMul(0x0d, stateMatrix[3, c]));
                data[2, c] = (byte)(Utility.GMul(0x0d, stateMatrix[0, c]) ^ Utility.GMul(0x09, stateMatrix[1, c]) ^ Utility.GMul(0x0e, stateMatrix[2, c]) ^ Utility.GMul(0x0b, stateMatrix[3, c]));
                data[3, c] = (byte)(Utility.GMul(0x0b, stateMatrix[0, c]) ^ Utility.GMul(0x0d, stateMatrix[1, c]) ^ Utility.GMul(0x09, stateMatrix[2, c]) ^ Utility.GMul(0x0e, stateMatrix[3, c]));
            }

            byte[] output = new byte[len];
            Buffer.BlockCopy(data, 0, output, 0, len);

            return output;
        }
        internal static byte[] InvAddRoundKey(byte[] state, byte[] key)
        {
            //TODO
            return state;
        }
    }
}
