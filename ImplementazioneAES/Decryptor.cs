using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementazioneAES
{
    internal static class Decryptor
    {
        // Sostituisce i byte applicati con la sbox con i valori della invsbox
        // Sezione 5.3.2, Figura 14 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
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

        // Funzione che posta la posizione dei byte verso destra in base alla righa
        // Sezione 5.3.1 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] InvShiftRows(byte[] state)
        {
            int len = state.Length, sideLen = (int)Math.Sqrt(len);

            byte[,] mat = state.To2DArray();

            for (int i = 0; i < sideLen; i++)
            {
                // Calcolo lo shift della parte dello stato che mi interessa (righa i), con offset i
                byte[] tmp1 = new byte[] { state[i + sideLen * 0], state[i + sideLen * 1], state[i + sideLen * 2], state[i + sideLen * 3] };
                byte[] tmp = Utility.ShiftRight(tmp1, i);
                // Copio il risultato nell'output, alla riga corrente
                //Buffer.BlockCopy(tmp, 0, output, row, sideLen);
                mat[0, i] = tmp[0];
                mat[1, i] = tmp[1];
                mat[2, i] = tmp[2];
                mat[3, i] = tmp[3];
            }

            return mat.To1DArray();
        }
        // Moltiplicazione nel campo finito di inverse rijndael
        // Sezione 5.3.3 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
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
                data[c, 0] = (byte)(Utility.GMul(0x0e, stateMatrix[c, 0]) ^ Utility.GMul(0x0b, stateMatrix[c, 1]) ^ Utility.GMul(0x0d, stateMatrix[c, 2]) ^ Utility.GMul(0x09, stateMatrix[c, 3]));
                data[c, 1] = (byte)(Utility.GMul(0x09, stateMatrix[c, 0]) ^ Utility.GMul(0x0e, stateMatrix[c, 1]) ^ Utility.GMul(0x0b, stateMatrix[c, 2]) ^ Utility.GMul(0x0d, stateMatrix[c, 3]));
                data[c, 2] = (byte)(Utility.GMul(0x0d, stateMatrix[c, 0]) ^ Utility.GMul(0x09, stateMatrix[c, 1]) ^ Utility.GMul(0x0e, stateMatrix[c, 2]) ^ Utility.GMul(0x0b, stateMatrix[c, 3]));
                data[c, 3] = (byte)(Utility.GMul(0x0b, stateMatrix[c, 0]) ^ Utility.GMul(0x0d, stateMatrix[c, 1]) ^ Utility.GMul(0x09, stateMatrix[c, 2]) ^ Utility.GMul(0x0e, stateMatrix[c, 3]));
            }

            byte[] output = new byte[len];
            Buffer.BlockCopy(data, 0, output, 0, len);

            return output;
        }
    }
}
