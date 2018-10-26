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
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SMPe.fres;
using SMPe.io;
using System.Collections.Generic;
using SMPe.bea;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using static SMPe.Helper;

namespace SMPe
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }

        public Editor(string where)
        {
            InitializeComponent();

            double testX = 14.2f;
            double testY = 13.2f;

            float testA = 14.2f;
            float testB = 13.2f;

            Console.WriteLine(testX == testA);
            Console.WriteLine(testY == testB);

            PerformOpen(where);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog beadialog = new OpenFileDialog
            {
                Filter = "BEA Archive (*.bea)|*.bea|All files (*.*)|*.*"
            };

            if (beadialog.ShowDialog() == DialogResult.OK)
                PerformOpen(beadialog.FileName);
            else
                return;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!drawFlag)
                return;

            Graphics g = e.Graphics;
            g.SetClip(e.ClipRectangle);
            g.ScaleTransform(14f, 14f);
            g.TranslateTransform(55.0f, 30.0f);
            g.Clear(Color.Black);

            Pen pen = null;

            // got a node selected?
            if (isNodeSelected)
            {
                // get our node
                SpaceNode selectedNode = (SpaceNode)treeView1.SelectedNode.Tag;

                // somehow its null?
                if (selectedNode == null)
                    Console.WriteLine("Node is null, yay");
                else
                {
                    // get our point based on the selected node
                    mSelectionPoint = GetPointFromNodeID(selectedNode.mNodeID, true);

                    // draw a semi-transparent circle
                    SolidBrush b = new SolidBrush(Color.FromArgb(128, Color.LightYellow));
                    e.Graphics.FillEllipse(b, mSelectionPoint.X - 1, mSelectionPoint.Y - 1.5f, 3, 3);

                    // draw the lines to our next nodes
                    DrawLinesToNextNodes(selectedNode, e.Graphics, true);
                }
            }

            int curSpace = 0;

            // decide the color of the space (lol)
            foreach (string key in mNodes.Keys)
            {
                /// skip the first node since it's never an actual space, just a header
                if (curSpace == 0)
                {
                    curSpace++;
                    continue;
                }

                // some hooks dont have spaces
                if (key == "hook_group")
                    continue;

                SpaceNode node = mNodes[key];

                // no node? ignore it
                if (node == null)
                    continue;

                PointF pos = new PointF(node.mPosX, isSidewaysView == true ? node.mPosY : node.mPosZ);

                if (Helper.mNodeTypeToColor.ContainsKey(node.mSpaceType))
                {
                    pen = new Pen(mNodeTypeToColor[node.mSpaceType]);
                    DrawSpace(pen, pos, g);
                }
                else
                    DrawSpace(new Pen(Color.Gold), pos, g);

                // draw the actual lines to the next nodes
                DrawLinesToNextNodes(node, g, false);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isNodeSelected)
            {
                if (e.Button == MouseButtons.Middle)
                {
                    // convert our position to the new position
                    double curPosX = Math.Round((e.X / 14.0f) - 55.0f, 1);
                    double curPosY = Math.Round((e.Y / 14.0f) - 30.0f, 1);

                    SpaceNode node = (SpaceNode)treeView1.SelectedNode.Tag;
                    node.mPosX = (float)curPosX;
                    node.mPosZ = (float)curPosY;

                    panel1.Invalidate();
                }
            }


            // since we scale the entire thing up by 14x on startup, and we moved it...
            // we need to convert our mouse click position to what the coords are and round to 1 decmial place
            double posX = Math.Round((e.X / 14.0f) - 55.0f, 1);
            double posY = Math.Round((e.Y / 14.0f) - 30.0f, 1);

            // index to see which node we land on if we get a hit
            int curIndex = 0;

            foreach (SpaceNode node in mNodes.Values)
            {
                // nodes that don't have a position get thrown out
                if (node == null)
                    continue;

                /*
                 * Here is how the selection really works:
                 * The values for node positions is REALLY small.
                 * Due to this, it's nearly impossible to hit a node position dead on.
                 * So we do some bounding by 0.7f, which is the width of each box.
                 * Then we check if it is in the bounds, and if it is, that's our node.
                 */
                double upperX = node.mPosX + 0.7f;
                double lowerX = node.mPosX - 0.7f;

                double upperY = node.mPosZ + 0.7f;
                double lowerY = node.mPosZ - 0.7f;

                if (posX > lowerX && posX < upperX)
                {
                    if (posY > lowerY && posY < upperY)
                    {
                        // make this node our selected node
                        treeView1.SelectedNode = treeView1.Nodes[curIndex];
                        // load into PropertyGrid
                        spaceInfoGrid.SelectedObject = node;
                        isNodeSelected = true;

                        panel1.Invalidate();
                    }
                }

                curIndex++;
            }
        }

        /// <summary>
        /// Sets the window name.
        /// </summary>
        /// <param name="what">The new window name, attached to the application name and version.</param>
        public void SetWindowName(string what)
        {
            this.Text = String.Format("SMPe v0.1 -- {0}", what);
        }

        private void PerformOpen(string where)
        {
            treeView1.Nodes.Clear();
            isNodeSelected = false;

            EndianBinaryReader reader = new EndianBinaryReader(File.Open(where, FileMode.Open));

            BEA bea = new BEA(ref reader);
            string fileName = Path.GetFileName(where);
            string assetNumber = fileName.Split('_', '.')[1];
            byte[] bfresArray = bea.GetAssetDataByKey(String.Format("mainmode/bds001_{0}/model/bds001_{0}_hook_mass.fmdb", assetNumber));

            if (bfresArray == null)
            {
                MessageBox.Show("The archive selected does not contain hooks for spaces.");
                return;
            }

            EndianBinaryReader bfresReader = new EndianBinaryReader(bfresArray);
            mBfres = new BFRES(ref bfresReader);

            byte[] csvArray = bea.GetAssetDataByKey(String.Format("mainmode/bds001_{0}/csv/bds001_{0}_map.csv", assetNumber));

            if (csvArray == null)
            {
                MessageBox.Show("The archive selected does not contain the CSV file for space attributes.");
                return;
            }

            StreamReader txtReader = new StreamReader(new MemoryStream(csvArray), Encoding.GetEncoding(932));
            mBoard = new Board(ref txtReader);

            mNodes = new Dictionary<string, SpaceNode>();

            foreach (FSKL.Bone bone in mBfres.mSkeleton.mBones)
            {
                SpaceNode node = GetSpaceFromKey(bone.mName);
                mNodes.Add(bone.mName, GetSpaceFromKey(bone.mName));

                if (node != null)
                {
                    node.SetPosition(bone.mTranslation);

                    string nodeName;

                    try
                    {
                        nodeName = Helper.mSimpleNodeNames[node.mSpaceType];
                    }
                    catch
                    {
                        nodeName = node.mSpaceType;
                    }

                    TreeNode tnode = new TreeNode(nodeName)
                    {
                        Tag = node
                    };

                    treeView1.Nodes.Add(tnode);
                }
            }

            drawFlag = true;
            statusStrip.Text = "File successfully loaded!";
            panel1.Invalidate();
        }

        /// <summary>
        /// Draws a space image at a specified position.
        /// </summary>
        /// <param name="spaceType">The string representing the space type.</param>
        /// <param name="point">The point to draw at.</param>
        /// <param name="g">Graphics instance to draw with.</param>
        private void DrawSpaceImage(string spaceType, PointF point, Graphics g)
        {
            Image imag = Image.FromFile(String.Format("img/{0}.png", spaceType));
            g.DrawImage(imag, point);
        }

        /// <summary>
        /// Draws a space based on a position given.
        /// </summary>
        /// <param name="pen">The pen to use. This can change the color of the space drawn.</param>
        /// <param name="point">The X and Y coordinate to draw at.</param>
        /// <param name="g">Graphics instance to use.</param>
        private void DrawSpace(Pen pen, PointF point, Graphics g)
        {
            PointF point2 = PointF.Add(point, new SizeF(1, 0));
            g.DrawLine(pen, point, point2);
        }

        /// <summary>
        /// Draws a line from one node to another.
        /// </summary>
        /// <param name="node">The node to draw the lines from.</param>
        /// <param name="g">Graphics supplied from the draw source.</param>
        private void DrawLinesToNextNodes(SpaceNode node, Graphics g, bool isForSelection)
        {
            Pen pen;
            // a highlighted line will be transparent and thicker
            if (isForSelection)
            {
                pen = new Pen(Color.FromArgb(128, Color.OrangeRed));
                pen.Width = 0.7f;
            }
            else
            {
                pen = new Pen(Color.FromArgb(128, Color.Cyan));
                pen.Width = 0.2f;
            }

            SpaceNode curNode;

            for (int i = 0; i < node.mNumNextNodes; i++)
            {
                // get our space from the id
                curNode = GetSpaceFromID(node.mNextNodes[i]);

                if (curNode == null)
                    continue;

                // draw the line
                g.DrawLine(pen, GetPointFromNodeID(node.mNodeID, true), GetPointFromNodeID(curNode.mNodeID, true));
            }
        }

        /// <summary>
        /// Gets the location of a node from a Node ID.
        /// </summary>
        /// <param name="nodeID">Node ID to get the position for.</param>
        /// <param name="isCSVNode">Is a node from the CSV file.</param>
        /// <returns></returns>
        private PointF GetPointFromNodeID(string nodeID, bool isCSVNode)
        {
            if (isCSVNode)
            {
                SpaceNode node = mNodes["hook_" + nodeID.PadLeft(3, '0')];
                return new PointF(node.mPosX, isSidewaysView == true ? node.mPosY : node.mPosZ);
            }
            else
            {
                SpaceNode node = mNodes[nodeID];
                return new PointF(node.mPosX, isSidewaysView == true ? node.mPosY : node.mPosZ);
            }
        }

        /// <summary>
        /// Returns a node based on a given ID.
        /// </summary>
        /// <param name="nodeID">The ID to search for.</param>
        /// <returns>The space with the given node id. null if not found.</returns>
        private SpaceNode GetSpaceFromID(string nodeID)
        {
            foreach (SpaceNode node in mBoard.mSpaces)
            {
                if (node.mNodeID == nodeID)
                    return node;
            }

            return null;
        }

        /// <summary>
        /// Returns a SpaceNode instance based on a given key.
        /// </summary>
        /// <param name="key">The node name to check for in the board space instances.</param>
        /// <returns>A SpaceNode that matches the given key. Returns null if not found.</returns>
        private SpaceNode GetSpaceFromKey(string key)
        {
            // two known keys that don't have SpaceNode instances
            if (key == "arrow" || key.Contains("line"))
                return null;

            string[] split = key.Split('_');

            foreach (SpaceNode node in mBoard.mSpaces)
            {
                string checkedStr = "";

                // the bone names are hook_XXX, while the node IDs here are 1, 2, 10, etc
                // so we need to pad the left with 0s to properly match the bone id

                checkedStr = node.mNodeID.PadLeft(3, '0');

                if (checkedStr == split[1])
                    return node;
            }

            return null;
        }

        /// <summary>
        /// Checks to see if a name is taken it the node dictionary.
        /// </summary>
        /// <param name="name">Name to check.</param>
        /// <returns>True if taken, false if not.</returns>
        private bool IsNameTaken(string name)
        {
            return mNodes.ContainsKey(name);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SpaceNode node = (SpaceNode)e.Node.Tag;
            try
            {
                spaceInfoGrid.SelectedObject = node;
                isNodeSelected = true;
                panel1.Invalidate();
            }
            catch
            {
                Console.WriteLine("whoops");
            }
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            // the node is changed AFTER this is ran, soooo...
            SpaceNode node;
            if (e.KeyData == Keys.Up)
            {
                try
                {
                    // get the node before because it's going up one node
                    node = (SpaceNode)treeView1.Nodes[treeView1.SelectedNode.Index - 1].Tag;
                    spaceInfoGrid.SelectedObject = node;
                }
                catch
                {
                    // look a useless catch
                }
            }
            else
            {
                try
                {
                    // get the node after because it's going down one node
                    node = (SpaceNode)treeView1.Nodes[treeView1.SelectedNode.Index + 1].Tag;
                    spaceInfoGrid.SelectedObject = node;
                }
                catch
                {
                    // yet another useless catch
                }
            }

            panel1.Invalidate();
        }

        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isSidewaysView = false;
            panel1.Invalidate();
        }

        private void sideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isSidewaysView = true;
            panel1.Invalidate();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // used for debugging
            //double posX = Math.Round((e.X / 14.0f) - 55.0f, 1);
            //double posY = Math.Round((e.Y / 14.0f) - 30.0f, 1);

            //statusStrip.Text = String.Format("X: {0} Y: {1}", posX, posY);
        }

        private void deleteSpaceButton_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("You need to have a space selected to delete it!");
                return;
            }

            // we need to check if there are nodes connected to this node
            // if there are, we need to set the node that connects with 
            // the one being deleted to no longer point to it
            SpaceNode theNode = (SpaceNode)treeView1.SelectedNode.Tag;
            string nodeID = theNode.mNodeID;

            foreach (SpaceNode node in mNodes.Values)
            {
                // ignore if our current node shows up here or if the node is null
                if (node == null || node.mNodeID == nodeID)
                    continue;

                for (int i = 0; i < 4; i++)
                {
                    // is our ID we are deleting in this node?
                    // if it is, we clear it and decrement the next node count
                    if (node.mNextNodes[i] == nodeID)
                    {
                        node.mNumNextNodes--;
                        node.mNextNodes[i] = "";
                    }
                }
            }

            // remove the node from the treeview
            treeView1.Nodes.Remove(treeView1.SelectedNode);
            // remove the node from the node dictionary
            mNodes.Remove("hook_" + nodeID.PadLeft(3, '0'));
            // update status label
            statusStrip.Text = "Successfully removed node!";
            // refresh panel
            panel1.Invalidate();
        }

        Dictionary<string, SpaceNode> mNodes;

        PointF mSelectionPoint;

        BFRES mBfres;
        Board mBoard;

        bool drawFlag = false;
        bool isNodeSelected = false;
        bool isSidewaysView = false;
    }
}