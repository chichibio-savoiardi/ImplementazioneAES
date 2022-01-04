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
            list.Add(ShiftRight(state[4..8], 1));
            list.Add(ShiftRight(state[8..12], 2));
            list.Add(ShiftRight(state[12..16], 3));

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

        private static byte[] ShiftRight(byte[] arr, int times)
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

        internal static byte[] InvMixColumns(byte[] state)
        {
            //TODO
            return state;
        }

        internal static byte[] InvAddRoundKey(byte[] state, byte[] key)
        {
            //TODO
            return state;
        }
    }
}
