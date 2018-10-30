/*
    © 2018 - shibboleet
    SMPe is free software: you can redistribute it and/or modify it under
    the terms of the GNU General Public License as published by the Free
    Software Foundation, either version 3 of the License, or (at your option)
    any later version.
    SMPe is distributed in the hope that it will be useful, but WITHOUT ANY 
    WARRANTY; See the GNU General Public License for more details.
    You should have received a copy of the GNU General Public License along 
    with SMPe. If not, see http://www.gnu.org/licenses/.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/* Unfinished */

namespace SMPe.io
{
    public class EndianBinaryReader : BinaryReader
    {
        public EndianBinaryReader(Stream s) : base(s) { }
        public EndianBinaryReader(Stream s, Encoding e) : base(s, e) { }
        public EndianBinaryReader(byte[] input) : base(new MemoryStream(input)) { }

        public enum Endianess
        {
            Little = 0,
            Big = 1
        }

        public void SetEndianess(Endianess e)
        {
            mEndianess = e;
        }

        public string ReadString(int length)
        {
            return Encoding.Default.GetString(ReadBytes(length));
        }


        public string ReadStringFromOffset(long where)
        {
            long curPos = this.BaseStream.Position;
            this.BaseStream.Position = where;

            // these strings have a short to determine how long it is
            ushort length = ReadUInt16();
            string ret = Encoding.Default.GetString(ReadBytes(length));
            this.BaseStream.Position = curPos;
            return ret;
        }

        /*public void ReadStringEntry(long where, ref CLB1.CLBStringEntry entry)
        {
            long curPos = this.BaseStream.Position;
            this.BaseStream.Position += where;

            entry.unk1 = ReadUInt32();

            // these strings have a byte to determine how long it is
            byte length = ReadByte();
            entry.name = Encoding.Default.GetString(ReadBytes(length));
            this.BaseStream.Position = curPos;
        }*/

        public byte[] ReadBytesAt(long where, int numBytes)
        {
            long curPos = this.BaseStream.Position;
            this.BaseStream.Position = where;
            byte[] ret = ReadBytes(numBytes);
            this.BaseStream.Position = curPos;
            return ret;
        }

        Endianess mEndianess;
    }
}