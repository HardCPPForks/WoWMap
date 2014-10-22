﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WoWMap
{
    public static class Helpers
    {
        public static IEnumerable<string> SplitStrings(byte[] data)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '\0') // Terminate string
                {
                    if (sb.Length > 1)
                        yield return sb.ToString();
                    sb = new StringBuilder();

                    continue;
                }

                sb.Append((char)data[i]);
            }
        }

        public static string ReadCString(byte[] data)
        {
            var sb = new StringBuilder(0x100);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '\0') // Terminate string
                    if (sb.Length > 1) break;

                sb.Append((char)data[i]);
            }
            return sb.ToString();
        }

        public static string ReadCString(this BinaryReader br)
        {
            var buffer = new List<byte>();
            byte b = 0;
            while ((b = br.ReadByte()) != 0)
                buffer.Add(b);

            if (buffer.Count <= 0)
                return null;

            return Encoding.ASCII.GetString(buffer.ToArray());
        }
    }
}