using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SMPe.io;

namespace SMPe.fres
{
    class BFRES
    {
        /* 
         * This is a VERY stripped BFRES file.
         * I only include the stuff relating to node bones.
         */
        public BFRES(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);
            mSignature = reader.ReadUInt32();
            mVersion = reader.ReadUInt32();
            mBOM = reader.ReadUInt16();
            mAlignment = reader.ReadByte();
            mTargetOffset = reader.ReadByte();
            mFileNameOffset = reader.ReadUInt32();
            mFlags = reader.ReadUInt16();
            mBlockOffset = reader.ReadUInt16();
            mRelocationTableOffset = reader.ReadUInt32();
            mBfresSize = reader.ReadUInt32();
            mFileNameSizeOffset = reader.ReadInt64();
            mModelOffset = reader.ReadUInt64();
            mModelDict = reader.ReadUInt64();

            // other sections we dont care about
            for (int i = 0; i < 10; i++)
                reader.ReadUInt64();

            mUnk1 = reader.ReadUInt64();
            mUnk2 = reader.ReadUInt64();

            // external stuff
            for (int i = 0; i < 3; i++)
                reader.ReadUInt64();

            mStringTableOffset = reader.ReadUInt64();
            mUnk3 = reader.ReadUInt32();
            mModelCount = reader.ReadUInt16();

            // other counts
            for (int i = 0; i < 3; i++)
                reader.ReadUInt16();

            // padding
            reader.ReadBytes(0xC);

            mFileName = reader.ReadStringFromOffset(mFileNameSizeOffset);

            /* Model */
            mModel = new FMDL(ref reader);

            mVerticies = new List<FVTX>();

            for (int i = 0; i < mModel.mVertArrayCount; i++)
                mVerticies.Add(new FVTX(ref reader));

            mMaterials = new List<FMAT>();

            for (int i = 0; i < mModel.mMatCount; i++)
                mMaterials.Add(new FMAT(ref reader));

            mShapes = new List<FSHP>();

            for (int i = 0; i < mModel.mShapeCount; i++)
                mShapes.Add(new FSHP(ref reader));

            mSkeleton = new FSKL(ref reader);
        }

        string mMagic;
        uint mSignature;
        uint mVersion;
        ushort mBOM;
        byte mAlignment;
        byte mTargetOffset;
        uint mFileNameOffset;
        ushort mFlags;
        ushort mBlockOffset;
        uint mRelocationTableOffset;
        uint mBfresSize;
        long mFileNameSizeOffset;
        ulong mModelOffset;
        ulong mModelDict;

        ulong mUnk1;
        ulong mUnk2;

        ulong mStringTableOffset;
        uint mUnk3;
        ushort mModelCount;

        FMDL mModel;
        List<FVTX> mVerticies;
        List<FMAT> mMaterials;
        List<FSHP> mShapes;
        public FSKL mSkeleton;

        /* generated */
        string mFileName;
    }
}
