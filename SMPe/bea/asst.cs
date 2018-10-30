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

using ZstdNet;
using SMPe.io;
using System.Runtime.InteropServices;

namespace SMPe.bea
{
    class ASST
    {

        public ASST(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "ASST")
                throw new Exception("Asset not valid!");

            mOffset = reader.ReadUInt32();
            mOffset64 = reader.ReadUInt64();
            mUnk1 = reader.ReadUInt16();
            mUnk2 = reader.ReadUInt16();
            mFileSize = reader.ReadInt32();
            mUncompressedSize = reader.ReadInt64();
            mFileOffset = reader.ReadInt64();
            mStringOffset = reader.ReadInt64();

            mCompressedFileData = reader.ReadBytesAt(mFileOffset, mFileSize);
            mFileName = reader.ReadStringFromOffset(mStringOffset);
        }

        string mMagic;
        uint mOffset;
        ulong mOffset64;
        ushort mUnk1;
        ushort mUnk2;
        int mFileSize;
        long mUncompressedSize;
        long mFileOffset;
        long mStringOffset;
        public byte[] mCompressedFileData;
        public string mFileName;
    }
}