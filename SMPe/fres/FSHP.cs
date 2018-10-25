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
    class FSHP
    {
        public FSHP(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "FSHP")
                throw new Exception("Invalid FSHP header.");

            reader.ReadBytes(0xC);

            mNameOffset = reader.ReadInt64();

            mVertOffset = reader.ReadUInt64();
            mOFSMeshList = reader.ReadUInt64();
            mOFSSkinBoneIndexList = reader.ReadUInt64();

            reader.ReadBytes(0x10);

            mBoundingBoxOffset = reader.ReadUInt64();
            mRadiusOffset = reader.ReadUInt64();

            reader.ReadBytes(0x8);

            mShapeFlags = reader.ReadInt32();
            mIndex = reader.ReadUInt16();
            mMatIndex = reader.ReadUInt16();
            mBoneIndex = reader.ReadUInt16();
            mIndex2 = reader.ReadUInt16();
            mNumSkinBoneIndex = reader.ReadUInt16();
            mVertSkinCount = reader.ReadByte();
            mNumMeshes = reader.ReadByte();
            mVisibleGroupCount = reader.ReadUInt32();
            mVisibleGroupIndex = reader.ReadUInt16();
            mSkelIndexArrayOffset = reader.ReadUInt16();

            mName = reader.ReadStringFromOffset(mNameOffset);
        }

        string mMagic;
        long mNameOffset;
        ulong mVertOffset;
        ulong mOFSMeshList;
        ulong mOFSSkinBoneIndexList;

        ulong mBoundingBoxOffset;
        ulong mRadiusOffset;
        int mShapeFlags;
        ushort mIndex;
        ushort mMatIndex;
        ushort mBoneIndex;
        ushort mIndex2;
        ushort mNumSkinBoneIndex;
        byte mVertSkinCount;
        byte mNumMeshes;
        uint mVisibleGroupCount;
        ushort mVisibleGroupIndex;
        ushort mSkelIndexArrayOffset;

        string mName;
    }
}
