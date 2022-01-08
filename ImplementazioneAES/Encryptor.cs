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
        // Sostituisce i byte del file con i valori della sbox
        // Sezione 5.1.1, Figura 7 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
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

        // Funzione che sposta la posizione dei byte verso sinistra in base alla riga
        // Sezione 5.1.2 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] ShiftRows(byte[] state)
        {
            int len = state.Length, sideLen = (int)Math.Sqrt(len);
            //byte[] output = new byte[len];

            byte[,] mat = state.To2DArray();

            for (int i = 0; i < sideLen; i++)
            {
                // Calcolo lo shift della parte dello stato che mi interessa (righa i), con offset i
                byte[] tmp1 = new byte[] { state[i + sideLen * 0], state[i + sideLen * 1], state[i + sideLen * 2], state[i + sideLen * 3] };
                byte[] tmp = Utility.ShiftLeft(tmp1, i);
                // Copio il risultato nell'output, alla riga corrente
                //Buffer.BlockCopy(tmp, 0, output, row, sideLen);
                mat[0, i] = tmp[0];
                mat[1, i] = tmp[1];
                mat[2, i] = tmp[2];
                mat[3, i] = tmp[3];
            }

            return mat.To1DArray();
        }
        // Moltiplicazione dello state nel campo finito di rijndael
        // Sezione 5.1.3 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] MixColumns(byte[] state)
        {
            // Lunghezze comuni
            int len = state.Length, sideLen = (int)Math.Sqrt(len);
            // Preparazione dati.
            // `data` contiene i dati della trasformazione, che verranno ricopiati e convertiti in un array1d in `output` alla fine
            byte[,] data = new byte[sideLen, sideLen];
            byte[,] stateMatrix = new byte[sideLen, sideLen];
            Buffer.BlockCopy(state, 0, stateMatrix, 0, len);

            for (int c = 0; c < sideLen; c++)
            {
                data[c, 0] = (byte)(Utility.GMul(0x02, stateMatrix[c, 0]) ^ Utility.GMul(0x03, stateMatrix[c, 1]) ^ stateMatrix[c, 2] ^ stateMatrix[c, 3]);
                data[c, 1] = (byte)(stateMatrix[c, 0] ^ Utility.GMul(0x02, stateMatrix[c, 1]) ^ Utility.GMul(0x03, stateMatrix[c, 2]) ^ stateMatrix[c, 3]);
                data[c, 2] = (byte)(stateMatrix[c, 0] ^ stateMatrix[c, 1] ^ Utility.GMul(0x02, stateMatrix[c, 2]) ^ Utility.GMul(0x03, stateMatrix[c, 3]));
                data[c, 3] = (byte)(Utility.GMul(0x03, stateMatrix[c, 0]) ^ stateMatrix[c, 1] ^ stateMatrix[c, 2] ^ Utility.GMul(0x02, stateMatrix[c, 3]));
            }

            byte[] output = new byte[len];
            Buffer.BlockCopy(data, 0, output, 0, len);

            return output;
        }

        // Esegue lo XOR tra lo `state` e la chiave espansa
        // Sezione 5.1.4 / 5.3.4 https://nvlpubs.nist.gov/nistpubs/fips/nist.fips.197.pdf (cope)
        internal static byte[] AddRoundKey(byte[] state, byte[] key)
        {
            return Utility.XorArray(state, key);
        }
    }
}
