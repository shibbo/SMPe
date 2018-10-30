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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMPe.Helper;

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

        public void SetPosition(Vector3f pos)
        {
            mPosX = pos.X;
            mPosY = pos.Y;
            mPosZ = pos.Z;
        }

        public string mNodeID;
        public string[] mNextNodes;
        public string mSpaceType;
        public string mAttr2;
        public string mAttr3;

        [Category("Space Attributes")]
        [DisplayName("Node ID")]
        [Description("ID of the current node. Use this to connect other nodes.")]
        public string NodeID
        {
            get { return mNodeID; }
            set { mNodeID = value; }
        }

        [Category("Next Nodes")]
        [DisplayName("Next Node 0")]
        [Description("A node that can be accessed from the current node.")]
        public string NextNode0
        {
            get { return mNextNodes[0]; }
            set { mNextNodes[0] = value; }
        }

        [Category("Next Nodes")]
        [DisplayName("Next Node 1")]
        [Description("A node that can be accessed from the current node.")]
        public string NextNode1
        {
            get { return mNextNodes[1]; }
            set { mNextNodes[1] = value; }
        }

        [Category("Next Nodes")]
        [DisplayName("Next Node 2")]
        [Description("A node that can be accessed from the current node.")]
        public string NextNode2
        {
            get { return mNextNodes[2]; }
            set { mNextNodes[2] = value; }
        }

        [Category("Next Nodes")]
        [DisplayName("Next Node 3")]
        [Description("A node that can be accessed from the current node.")]
        public string NextNode3
        {
            get { return mNextNodes[3]; }
            set { mNextNodes[3] = value; }
        }

        [Category("Space Attributes")]
        [DisplayName("Space Type")]
        [Description("Defines the space type.")]
        public string SpaceType
        {
            get { return mSpaceType; }
            set { mSpaceType = value; }
        }

        [Category("Space Attributes")]
        [DisplayName("Setting 1")]
        [Description("Space-specific setting.")]
        public string NodeAttr1
        {
            get { return mAttr2; }
            set { mAttr2 = value; }
        }

        [Category("Space Attributes")]
        [DisplayName("Setting 2")]
        [Description("Space-specific setting.")]
        public string NodeAttr2
        {
            get { return mAttr3; }
            set { mAttr3 = value; }
        }

        [Category("Space Location")]
        [DisplayName("X")]
        [Description("The X position of the node.")]
        public float PositionX
        {
            get { return mPosX; }
            set { mPosX = value; }
        }

        [Category("Space Location")]
        [DisplayName("Y")]
        [Description("The Y position of the node.")]
        public float PositionY
        {
            get { return mPosY; }
            set { mPosY = value; }
        }

        [Category("Space Location")]
        [DisplayName("Z")]
        [Description("The Z position of the node.")]
        public float PositionZ
        {
            get { return mPosZ; }
            set { mPosZ = value; }
        }

        public byte mNumNextNodes;
        public float mPosX;
        public float mPosY;
        public float mPosZ;
    }
}
