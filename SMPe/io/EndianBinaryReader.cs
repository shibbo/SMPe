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

        Endianess mEndianess;
    }
}