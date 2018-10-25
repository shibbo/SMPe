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
    public class BEA
    {
        public BEA(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "SCNE")
                throw new Exception("Invalid BEA header.");

            mVersion = reader.ReadUInt32();
            
            reader.ReadUInt32();
            
            mBOM = reader.ReadUInt16();
            mAlignment = reader.ReadByte();
            mTargetAddrSize = reader.ReadByte();
            
            reader.ReadUInt32();
            reader.ReadUInt16();
            
            mBlockOffset = reader.ReadUInt16();
            mRelocTableOffset = reader.ReadUInt32();
            mEOF = reader.ReadUInt32();
            mFileCount = reader.ReadUInt64();
            mFileInfoOffset = reader.ReadUInt64();
            mFileDictOffset = reader.ReadInt64();
            mUnk1 = reader.ReadUInt64();
            mNameOffset = reader.ReadInt64();
            mBlockOffset2 = reader.ReadInt64();

            mName = reader.ReadStringFromOffset(mNameOffset);

            reader.BaseStream.Position = mFileDictOffset;

            mDictionary = new DICT(ref reader);
            
            // right after the dictionary are the assets
            // we store the assets by their key, which is in the dictionary
            mAssets = new Dictionary<string, ASST>();

            for (ulong i = 0; i < mFileCount; i++)
                mAssets.Add(mDictionary.GetKeyFromIndex((int)i), new ASST(ref reader));

            // right after the assets is the string table
            // this honestly isn't required for reading but whatever
            mStringTable = new STR(ref reader);
            
        }

        public byte[] GetAssetDataByKey(string key)
        {
            ASST asset;

            if (mAssets.TryGetValue(key, out asset))
                return asset.mFileData;
            else
                return null;
        }

        string mMagic;
        uint mVersion;
        ushort mBOM;
        byte mAlignment;
        byte mTargetAddrSize;
        ushort mBlockOffset;
        uint mRelocTableOffset;
        uint mEOF;
        ulong mFileCount;
        ulong mFileInfoOffset;
        long mFileDictOffset;
        ulong mUnk1;
        long mNameOffset;
        long mBlockOffset2;

        DICT mDictionary;
        Dictionary<string, ASST> mAssets;
        STR mStringTable;

        string mName;
    }
}