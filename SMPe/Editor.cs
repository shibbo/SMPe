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
            g.ScaleTransform(12f, 12f);
            g.TranslateTransform(60.0f, 30.0f);
            g.Clear(Color.Black);

            Pen pen = null;

            if (isNodeSelected)
            {
                SpaceNode selectedNode = (SpaceNode)treeView1.SelectedNode.Tag;

                if (selectedNode == null)
                    Console.WriteLine("Node is null, yay");
                else
                {
                    mSelectionPoint = GetPointFromNodeID(selectedNode.mNodeID, true);

                    SolidBrush b = new SolidBrush(Color.FromArgb(128, Color.LightYellow));
                    e.Graphics.FillEllipse(b, mSelectionPoint.X - 1, mSelectionPoint.Y - 1.5f, 3, 3);
                }

            }

            int curSpace = 0;

            // decide the color of the space (lol)
            foreach (string key in nodePositions.Keys)
            {
                if (curSpace == 0)
                {
                    curSpace++;
                    continue;
                }

                // some hooks dont have spaces
                if (key == "hook_group")
                    continue;

                SpaceNode node = GetSpaceFromKey(key);

                if (node == null)
                {
                    pen = new Pen(Color.LightCyan);
                    DrawSpace(pen, nodePositions[key], g);
                    continue;
                }

                switch (node.mSpaceType)
                {
                    case "PLUS":
                        pen = new Pen(Color.Blue);
                        DrawSpace(pen, nodePositions[key], g);
                        break;
                    case "MINUS":
                        pen = new Pen(Color.Red);
                        DrawSpace(pen, nodePositions[key], g);
                        break;
                    case "HAPPENING":
                        pen = new Pen(Color.DarkRed);
                        DrawSpace(pen, nodePositions[key], g);
                        break;
                    case "START":
                        pen = new Pen(Color.Green);
                        DrawSpace(pen, nodePositions[key], g);
                        break;
                    case "JOYCON":
                        pen = new Pen(Color.Orange);
                        DrawSpace(pen, nodePositions[key], g);
                        break;
                    case "ITEM":
                        pen = new Pen(Color.LimeGreen);
                        DrawSpace(pen, nodePositions[key], g);
                        break;
                    case "SUPPORT":
                        pen = new Pen(Color.DarkOliveGreen);
                        DrawSpace(pen, nodePositions[key], g);
                        break;
                    default:
                        pen = new Pen(Color.Gold);
                        DrawSpace(pen, nodePositions[key], g);
                        break;
                }

                DrawLinesToNextNodes(node, g);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // todo
        }

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

            nodePositions = new Dictionary<string, PointF>();

            foreach (FSKL.Bone bone in mBfres.mSkeleton.mBones)
            {
                PointF point = new PointF(bone.mTranslation.X, bone.mTranslation.Z);
                nodePositions.Add(bone.mName, point);

                SpaceNode node = GetSpaceFromKey(bone.mName);

                if (node != null)
                {
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
        private void DrawLinesToNextNodes(SpaceNode node, Graphics g)
        {
            Pen myPen = new Pen(Color.FromArgb(128, Color.Cyan));
            myPen.Width = 0.2f;

            SpaceNode curNode;

            for (int i = 0; i < node.mNumNextNodes; i++)
            {
                // get our space from the id
                curNode = GetSpaceFromID(node.mNextNodes[i]);
                // draw the line
                g.DrawLine(myPen, GetPointFromNodeID(node.mNodeID, true), GetPointFromNodeID(curNode.mNodeID, true));
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
                return nodePositions["hook_" + nodeID.PadLeft(3, '0')];
            else
                return nodePositions[nodeID];
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

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SpaceNode node = (SpaceNode)e.Node.Tag;
            try
            {
                statusStrip.Text = String.Format("Selected Node: '{0}' Next: ('{1}' '{2}' '{3}' '{4}') Attributes: ('{5}' '{6}' '{7}')", node.mNodeID, node.mNextNodes[0], node.mNextNodes[1], node.mNextNodes[2], node.mNextNodes[3], node.mSpaceType, node.mAttr2, node.mAttr3);
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
                node = (SpaceNode)treeView1.Nodes[treeView1.SelectedNode.Index - 1].Tag;
            else
                node = (SpaceNode)treeView1.Nodes[treeView1.SelectedNode.Index + 1].Tag;

            statusStrip.Text = String.Format("Selected Node: '{0}' Next: ('{1}' '{2}' '{3}' '{4}') Attributes: ('{5}' '{6}' '{7}')", node.mNodeID, node.mNextNodes[0], node.mNextNodes[1], node.mNextNodes[2], node.mNextNodes[3], node.mSpaceType, node.mAttr2, node.mAttr3);
            panel1.Invalidate();
        }

        Dictionary<string, PointF> nodePositions;

        PointF mSelectionPoint;

        BFRES mBfres;
        Board mBoard;

        bool drawFlag = false;
        bool isNodeSelected = false;
    }
}