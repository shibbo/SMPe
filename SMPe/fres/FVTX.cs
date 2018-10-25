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

using SMPe.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPe.fres
{
    class FVTX
    {
        public FVTX(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "FVTX")
                throw new Exception("Invalid FSKL header.");

            reader.ReadBytes(0xC);

            mVertAttriArrayOffset = reader.ReadUInt64();
            mVertAttriDictOffset = reader.ReadUInt64();
            mUnk1 = reader.ReadUInt64();
            mUnk2 = reader.ReadUInt64();
            mUnk3 = reader.ReadUInt64();
            mVertBufferSizeOffset = reader.ReadUInt64();
            mVertStrideSizeOffset = reader.ReadUInt64();
            mVertBufferArrayOffset = reader.ReadUInt64();
            mBufferOffset = reader.ReadInt32();
            mVertAttribCount = reader.ReadByte();
            mVertBufferCount = reader.ReadByte();
            mIndex = reader.ReadUInt16();
            mCount = reader.ReadUInt32();
            mSkinWeightInfluence = reader.ReadUInt32();
        }

        string mMagic;
        ulong mVertAttriArrayOffset;
        ulong mVertAttriDictOffset;
        ulong mUnk1;
        ulong mUnk2;
        ulong mUnk3;
        ulong mVertBufferSizeOffset;
        ulong mVertStrideSizeOffset;
        ulong mVertBufferArrayOffset;
        int mBufferOffset;
        byte mVertAttribCount;
        byte mVertBufferCount;
        ushort mIndex;
        uint mCount;
        uint mSkinWeightInfluence;
    }
}
