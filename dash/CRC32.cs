using System;
using System.IO;

namespace CRC
{
    public class CRC32
    {
        public static UInt32 DefaultPolynomial = 0xedb88320;
        public static UInt32 DefaultSeed = 0xffffffff;
        private static UInt32[] DefaultTable;

        /// <summary>
        /// This function computes a hash from the given file
        /// </summary>
        /// <param name="file">Path to file</param>
        /// <returns>Hash from file</returns>
        public static UInt32 ComputeHash(string file)
        {
            byte[] buffer;
            using (FileStream fs = File.Open(file, FileMode.Open))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
            }
            if (buffer != null)
                return ~CalculateHash(InitializeTable(DefaultPolynomial), DefaultSeed, buffer, 0, buffer.Length);
            else
                return 0;
        }

        /// <summary>
        /// This function computes a hash from the given buffer
        /// </summary>
        /// <param name="buffer">Buffer to create hash from</param>
        /// <returns>Hash from Buffer</returns>
        public static UInt32 ComputeHash(byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(DefaultPolynomial), DefaultSeed, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Function to Initialize table on first use.
        /// </summary>
        /// <param name="polynomial">Polynominal to create table</param>
        /// <returns>Table with values</returns>
        private static UInt32[] InitializeTable(UInt32 polynomial)
        {
            if ((polynomial == DefaultPolynomial) && (DefaultTable != null))
                return DefaultTable;

            var createTable = new UInt32[256];
            for (int i = 0; i < 256; i++)
            {
                var entry = (UInt32)i;
                for (int j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry = entry >> 1;
                createTable[i] = entry;
            }

            if (polynomial == DefaultPolynomial)
                DefaultTable = createTable;

            return createTable;
        }

        /// <summary>
        /// Function to calculate hash from desired data
        /// </summary>
        /// <param name="table">Table to use</param>
        /// <param name="seed">Seed to use</param>
        /// <param name="buffer">Data for hashing</param>
        /// <param name="start">Start if offset required</param>
        /// <param name="size">Maximum size of buffer</param>
        /// <returns>Calculated hash</returns>
        private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, byte[] buffer, int start, int size)
        {
            UInt32 crc = seed;
            for (int i = start; i < size; i++)
                unchecked
                {
                    crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
                }
            return crc;
        }
    }
}