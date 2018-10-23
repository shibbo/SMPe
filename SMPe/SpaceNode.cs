using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPe
{
    class SpaceNode
    {
        public SpaceNode(string line)
        {
            string[] theLine = line.Split(',');

            mNextNodes = new string[4];

            mNodeID = theLine[0];

            mNextNodes[0] = theLine[1];
            mNextNodes[1] = theLine[2];
            mNextNodes[2] = theLine[3];
            mNextNodes[3] = theLine[4];

            for (int i = 0; i < 4; i++)
                if (mNextNodes[i] != "")
                    mNumNextNodes++;

            mSpaceType = theLine[5];
            mAttr2 = theLine[6];
            mAttr3 = theLine[7];
        }

        public string mNodeID;
        public string[] mNextNodes;
        public string mSpaceType;
        public string mAttr2;
        public string mAttr3;

        public byte mNumNextNodes;
    }
}
