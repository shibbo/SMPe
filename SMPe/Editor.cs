using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SMPe.fres;
using SMPe.io;
using System.Collections.Generic;

namespace SMPe
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();

            mSimpleNodeNames = new Dictionary<string, string>()
            {
                { "PLUS", "Blue Space" },
                { "MINUS", "Red Space" },
                { "LUCKY", "Lucky Space" },
                { "HATENA_1", "Event Space 1" },
                { "HATENA_2", "Event Space 2" },
                { "HATENA_3", "Event Space 3" },
                { "START", "Starting Point" },
                { "MARK_PC", "Character Start Point" },
                { "MARK_STAR", "Star Position" },
                { "MARK_STAROBJ", "Toadette Position" },
                { "SUPPORT", "Ally Space" },
                { "HAPPENING", "Unlucky Space" },
                { "ITEM", "Item Space" },
                { "BATTAN", "Whomp" },
                { "JUGEMU_OBJ", "Lakitu Cloud" },
                { "JUGEMU", "Lakitu Space" },
                { "JOYCON", "Versus Space" },
                { "TREASURE_OBJ", "Treasure Chest" },
                { "SHOP_A", "Shop 1 (Normal)" },
                { "SHOP_B", "Shop 2 (Special)" },
                { "", "Branch" }
            };
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fresdialog = new OpenFileDialog
            {
                Filter = "FMDB Files (*.fmdb)|*.fmdb|All files (*.*)|*.*"
            };

            if (fresdialog.ShowDialog() == DialogResult.OK)
            {
                EndianBinaryReader reader = new EndianBinaryReader(File.Open(fresdialog.FileName, FileMode.Open));

                mBfres = new BFRES(ref reader);
            }
            else
                return;

            OpenFileDialog csvdialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (csvdialog.ShowDialog() == DialogResult.OK)
                mBoard = new Board(csvdialog.FileName);
            else
                return;

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
                        nodeName = mSimpleNodeNames[node.mSpaceType];
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
                        pen = new Pen(Color.White);
                        DrawSpace(pen, nodePositions[key], g);
                        break;
                    case "START":
                        pen = new Pen(Color.Green);
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

        private void DrawLinesToNextNodes(SpaceNode node, Graphics g)
        {
            Pen myPen = new Pen(Color.FromArgb(128, Color.Cyan));
            myPen.Width = 0.2f;

            SpaceNode curNode;

            for (int i = 0; i < node.mNumNextNodes; i++)
            {
                curNode = GetSpaceFromID(node.mNextNodes[i]);

                g.DrawLine(myPen, GetPointFromNodeID(node.mNodeID, true), GetPointFromNodeID(curNode.mNodeID, true));
            }
        }

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

        Dictionary<string, PointF> nodePositions;

        PointF mSelectionPoint;

        BFRES mBfres;
        Board mBoard;

        bool drawFlag = false;
        bool isNodeSelected = false;

        public Dictionary<string, string> mSimpleNodeNames;
    }
}