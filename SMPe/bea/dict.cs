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
    class DICT
    {
        public DICT(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "_DIC")
                throw new Exception("Invalid DICT.");

            mNumNodes = reader.ReadInt32();

            mNodes = new List<DictNode>();

            for (int i = 0; i < mNumNodes + 1; i++)
            {
                DictNode node = new DictNode();
                node.reference = reader.ReadUInt32();
                node.indexLeft = reader.ReadUInt16();
                node.indexRight = reader.ReadUInt16();
                node.keyOffset = reader.ReadInt64();

                node.key = reader.ReadStringFromOffset(node.keyOffset);

                if (i == 0)
                    continue;

                mNodes.Add(node);
            }
        }

        public string GetKeyFromIndex(int idx)
        {
            return mNodes[idx].key;
        }

        string mMagic;
        int mNumNodes;

        List<DictNode> mNodes;
    }

    struct DictNode
    {
        public uint reference;
        public ushort indexLeft;
        public ushort indexRight;
        public long keyOffset;

        public string key;
    }
}