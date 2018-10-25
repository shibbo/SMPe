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
using SMPe.io;

namespace SMPe.bea
{
    class STR
    {
        public STR(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "_STR")
                throw new Exception("String table header is invalid!");

            reader.ReadUInt32();

            mSectionSize = reader.ReadUInt64();
            mStrCount = reader.ReadUInt64();

            mStrs = new Dictionary<string, short>();

            for (ulong i = 0; i < mStrCount; i++)
            {
                // each string here is prefixed with a length
                ushort len = reader.ReadUInt16();
                mStrs.Add(reader.ReadString(len), (short)len);
                reader.ReadByte(); // null terminator (nice)

                // congrats. sometimes, there are TWO trailing zeros after a string!
                // good work
                if (reader.ReadByte() == 0)
                    continue;
                else
                    reader.BaseStream.Position -= 1;

                Console.WriteLine("Added {0}", mStrs.Last());
            }

            reader.ReadUInt16(); // padding
        }

        string mMagic;
        ulong mSectionSize;
        ulong mStrCount;

        Dictionary<string, short> mStrs;
    }
}