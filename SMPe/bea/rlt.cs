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
    class RLT
    {
        public RLT(ref EndianBinaryReader reader)
        {
            mMagic = reader.ReadString(4);

            if (mMagic != "_RLT")
                throw new Exception("Relocation table header is invalid!");

            mCurLocation = reader.ReadUInt32();
            mNumSections = reader.ReadUInt32();
            reader.ReadUInt32();

            mSections = new List<RLTSection>();

            for (int i = 0; i < mNumSections; i++)
            {
                RLTSection section = new RLTSection();
                section.basePointer = reader.ReadUInt64();
                section.position = reader.ReadUInt32();
                section.size = reader.ReadUInt32();
                section.entryIndex = reader.ReadInt32();
                section.entryCount = reader.ReadInt32();
            }
        }

        struct RLTSection
        {
            public ulong basePointer;
            public uint position;
            public uint size;
            public int entryIndex;
            public int entryCount;
        }

        struct RLTEntry
        {
            public uint position;
            public ushort stuctureCount;
            public byte offsetCount;
            public byte paddingCount;
        }

        string mMagic;
        uint mCurLocation;
        uint mNumSections;
        
        List<RLTSection> mSections;
    }
}