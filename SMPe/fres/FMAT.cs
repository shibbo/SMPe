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
    class FMAT
    {
        public FMAT(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "FMAT")
                throw new Exception("Invalid FMAT header.");

            mHeaderLength = reader.ReadInt32();
            mHeaderLength64 = reader.ReadUInt64();
            mNameOffset = reader.ReadInt64();
            mRenderInfoOffset = reader.ReadUInt64();
            mRenderInfoDictOffset = reader.ReadUInt64();
            mShaderAssignOffset = reader.ReadUInt64();
            mUnk1 = reader.ReadUInt64();
            mTexRefArrayOffset = reader.ReadUInt64();
            mUnk2 = reader.ReadUInt64();
            mSamplerListOffset = reader.ReadUInt64();
            mSamplerDictOffset = reader.ReadUInt64();
            mShaderParamArrayOffset = reader.ReadUInt64();
            mShaderParamDictOffset = reader.ReadUInt64();
            mSourceParamDataOffset = reader.ReadUInt64();
            mUserDataOffset = reader.ReadUInt64();
            mUserDataDict = reader.ReadUInt64();
            mVolatileFlagOffset = reader.ReadUInt64();
            mUserOffset = reader.ReadUInt64();
            mSamplerSlotOffset = reader.ReadUInt64();
            mTextureSlotOffset = reader.ReadUInt64();
            mMatFlags = reader.ReadInt32();
            mSectIndex = reader.ReadUInt16();
            mRenderInfoCount = reader.ReadUInt16();
            mTexRefCount = reader.ReadByte();
            mSamplerCount = reader.ReadByte();
            mShaderParamCount = reader.ReadUInt16();
            mShaderParamVolatileCount = reader.ReadUInt16();
            mSourceParamDataSize = reader.ReadUInt16();
            mRawParamDataSize = reader.ReadUInt16();
            mUserDataCount = reader.ReadUInt16();

            mName = reader.ReadStringFromOffset(mNameOffset);

            reader.ReadBytes(0x4);
        }

        string mMagic;
        int mHeaderLength;
        ulong mHeaderLength64;
        long mNameOffset;
        ulong mRenderInfoOffset;
        ulong mRenderInfoDictOffset;
        ulong mShaderAssignOffset;
        ulong mUnk1;
        ulong mTexRefArrayOffset;
        ulong mUnk2;
        ulong mSamplerListOffset;
        ulong mSamplerDictOffset;
        ulong mShaderParamArrayOffset;
        ulong mShaderParamDictOffset;
        ulong mSourceParamDataOffset;
        ulong mUserDataOffset;
        ulong mUserDataDict;
        ulong mVolatileFlagOffset;
        ulong mUserOffset;
        ulong mSamplerSlotOffset;
        ulong mTextureSlotOffset;
        int mMatFlags;
        ushort mSectIndex;
        ushort mRenderInfoCount;
        byte mTexRefCount;
        byte mSamplerCount;
        ushort mShaderParamCount;
        ushort mShaderParamVolatileCount;
        ushort mSourceParamDataSize;
        ushort mRawParamDataSize;
        ushort mUserDataCount;

        string mName;
    }
}
