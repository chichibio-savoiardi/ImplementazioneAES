namespace ImplementazioneAES
{
    internal static class Utility
    {
        internal static byte[] ShiftLeft(byte[] arr, int offset)
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
            for (int i = 0; i < offset; i++)
            {
                output = ShiftLeftOne(output);
            }
            return output;
        }

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

        internal static byte GMul(byte a, byte b)
        {
            byte p = 0;

            for (int counter = 0; counter < 8; counter++)
            {
                if ((b & 1) != 0)
                {
                    p ^= a;
                }

                bool hi_bit_set = (a & 0x80) != 0;
                a <<= 1;
                if (hi_bit_set)
                {
                    a ^= 0x1B; /* x^8 + x^4 + x^3 + x + 1 */
                }
                b >>= 1;
            }

            return p;
        }
    }
}