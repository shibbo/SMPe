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
    class FMDL
    {
        public FMDL(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "FMDL")
                throw new Exception("Invalid FMDL header.");

            mHeaderLength = reader.ReadUInt32();
            mHeaderLength64 = reader.ReadUInt64();
            mNameOffset = reader.ReadInt64();

            mEndOfStringTable = reader.ReadUInt64();
            mSkeletonOffset = reader.ReadUInt64();
            mVertArrayOffset = reader.ReadUInt64();
            mShapeOffset = reader.ReadUInt64();
            mShapeDict = reader.ReadUInt64();
            mMaterialOffset = reader.ReadUInt64();
            mMaterialDict = reader.ReadUInt64();
            mUserDataOffset = reader.ReadUInt64();

            reader.ReadBytes(0x10);

            mVertArrayCount = reader.ReadUInt16();
            mShapeCount = reader.ReadUInt16();
            mMatCount = reader.ReadUInt16();
            mUserDataCount = reader.ReadUInt16();
            mNumVerts = reader.ReadUInt32();
            reader.ReadUInt32();

            mName = reader.ReadStringFromOffset(mNameOffset);

            mResData = new IResData
            {
                unk = reader.ReadUInt32(),
                size = reader.ReadUInt32(),
                bufferOffset = reader.ReadInt64()
            };

            reader.ReadBytes(0x10);

            mModelDict = new ModelDict
            {
                size = reader.ReadUInt32(),
                nodeCount = reader.ReadUInt32(),

                nodes = new List<DictNode>()
            };

            for (int i = 0; i < mModelDict.nodeCount + 1; i++)
            {
                DictNode node = new DictNode
                {
                    key = reader.ReadUInt32(),
                    leftIndex = reader.ReadUInt16(),
                    rightIndex = reader.ReadUInt16(),
                    nameOffset = reader.ReadInt64()
                };

                node.name = reader.ReadStringFromOffset(node.nameOffset);

                mModelDict.nodes.Add(node);
            }
        }

        struct IResData
        {
            public uint unk;
            public uint size;
            public long bufferOffset;
        }

        struct ModelDict
        {
            public uint size;
            public uint nodeCount; // this does not include the root node

            public List<DictNode> nodes;
        }

        struct DictNode
        {
            public uint key;
            public ushort leftIndex;
            public ushort rightIndex;
            public long nameOffset;

            public string name;
        }

        string mMagic;
        uint mHeaderLength;
        ulong mHeaderLength64;
        long mNameOffset;
        ulong mEndOfStringTable;
        ulong mSkeletonOffset;
        ulong mVertArrayOffset;
        ulong mShapeOffset;
        ulong mShapeDict;
        ulong mMaterialOffset;
        ulong mMaterialDict;
        ulong mUserDataOffset;
        public ushort mVertArrayCount;
        public ushort mShapeCount;
        public ushort mMatCount;
        ushort mUserDataCount;
        uint mNumVerts;

        string mName;

        IResData mResData;
        ModelDict mModelDict;
    }
}
