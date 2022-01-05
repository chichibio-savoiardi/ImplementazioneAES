using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementazioneAES
{
    internal static class Decryptor
    {
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


        internal static byte[] InvShiftRows(byte[] state)
        {
            int len = state.Length;

            List<byte[]> list = new List<byte[]>(len + 1);

            list.Add(state[0..4]);
            list.Add(Utility.ShiftRight(state[4..8], 1));
            list.Add(Utility.ShiftRight(state[8..12], 2));
            list.Add(Utility.ShiftRight(state[12..16], 3));

            byte[] output = new byte[len];

            int i = 0;
            foreach (var arr in list)
            {
                foreach (var elem in arr)
                {
                    output[i++] = elem;
                }
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
    }
}
