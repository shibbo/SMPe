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
using static SMPe.Helper;

namespace SMPe.fres
{
    class FSKL
    {
        public FSKL(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "FSKL")
                throw new Exception("Invalid FSKL header.");

            mHeaderLength = reader.ReadUInt32();
            mHeaderLength64 = reader.ReadUInt64();
            mBoneDictOffset = reader.ReadUInt64();
            mBoneArrayOffset = reader.ReadUInt64();
            mOFSMtxToBoneList = reader.ReadUInt64();
            mOFSInverseModelMtxList = reader.ReadUInt64();

            reader.ReadBytes(0x18);

            mScalingRotationFlag = reader.ReadInt32();
            mBoneCount = reader.ReadUInt16();
            mNumSmoothMtxs = reader.ReadUInt16();
            mNumRigidMtxs = reader.ReadUInt16();

            reader.ReadBytes(0x6);

            mBones = new List<Bone>();

            for (int i = 0; i < mBoneCount; i++)
            {
                Bone bone = new Bone();
                bone.mNameOffset = reader.ReadInt64();
                reader.ReadBytes(0x20);
                bone.index = reader.ReadUInt16();
                bone.parentIndex = reader.ReadUInt16();
                bone.smoothMtxIndex = reader.ReadInt16();
                bone.rigidMtxIndex = reader.ReadInt16();
                bone.bilboardIndex = reader.ReadUInt16();
                bone.userDataCount = reader.ReadUInt16();
                bone.flags = reader.ReadInt32();

                Vector3f scale = new Vector3f
                {
                    X = reader.ReadSingle(),
                    Y = reader.ReadSingle(),
                    Z = reader.ReadSingle()
                };

                bone.mScale = scale;

                Vector4f rotation = new Vector4f
                {
                    X = reader.ReadSingle(),
                    Y = reader.ReadSingle(),
                    Z = reader.ReadSingle(),
                    H = reader.ReadSingle()
                };

                bone.mRotation = rotation;

                Vector3f position = new Vector3f
                {
                    X = reader.ReadSingle(),
                    Y = reader.ReadSingle(),
                    Z = reader.ReadSingle()
                };

                bone.mTranslation = position;

                bone.mName = reader.ReadStringFromOffset(bone.mNameOffset);

                mBones.Add(bone);
            }
        }

        public struct Bone
        {
            public long mNameOffset;
            public ushort index;
            public ushort parentIndex;
            public short smoothMtxIndex;
            public short rigidMtxIndex;
            public ushort bilboardIndex;
            public ushort userDataCount;
            public int flags;
            public Vector3f mScale;
            public Vector4f mRotation;
            public Vector3f mTranslation;

            public string mName;
        }

        string mMagic;
        uint mHeaderLength;
        ulong mHeaderLength64;
        ulong mBoneDictOffset;
        ulong mBoneArrayOffset;
        ulong mOFSMtxToBoneList;
        ulong mOFSInverseModelMtxList;
        int mScalingRotationFlag;
        ushort mBoneCount;
        ushort mNumSmoothMtxs;
        ushort mNumRigidMtxs;

        public List<Bone> mBones;

    }
}
