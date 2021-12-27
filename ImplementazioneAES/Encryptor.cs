using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementazioneAES;

namespace ImplementazioneAES
{
    /// <summary>
    /// Esegue la crittografia di una matrice di byte
    /// </summary>
    internal class Encryptor
    {
        private byte[,] state;
        private byte[,] key;
        public Encryptor(byte[,] stateIn, byte[,] keyIn)
        {
            state = stateIn;
            key = keyIn;
        }

        public byte[,] Encrypt()
        {

            return state;
        }

        private byte[,] SubBytes(byte[,] state)
        {
            //TODO
            return state;
        }

        private byte[,] ShiftRows(byte[,] state)
        {
            //TODO
            return state;
        }

        private byte[,] MixColumns(byte[,] state)
        {
            //TODO
            return state;
        }

        private byte[,] AddRoundKey(byte[,] state, byte[,] key)
        {
            //TODO
            return state;
        }
    }
}
