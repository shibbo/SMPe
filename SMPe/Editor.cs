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

                TreeNode tnode = new TreeNode(bone.mName)
                {
                    Tag = node
                };

                treeView1.Nodes.Add(tnode);
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
            g.ScaleTransform(6f, 6f);
            g.TranslateTransform(60.0f, 30.0f);
            g.Clear(Color.Black);

            Pen pen = null;

            int curSpace = 0;
            
            // decide the color of the space (lol)
            foreach(string key in nodePositions.Keys)
            {
                if (curSpace == 0)
                {
                    curSpace++;
                    continue;
                }

                if (key == "hook_group")
                    continue;

                SpaceNode node = GetSpaceFromKey(key);

                if (node == null)
                {
                    DrawSpace(pen, nodePositions[key], g);
                    continue;
                }

                Console.WriteLine(node.mSpaceType);

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
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // todo
        }

        private void DrawSpace(Pen pen, PointF point, Graphics g)
        {
            PointF point2 = PointF.Add(point, new SizeF(1, 0));
            g.DrawLine(pen, point, point2);
        }

        private SpaceNode GetSpaceFromKey(string key)
        {
            string[] split = key.Split('_');

            foreach(SpaceNode node in mBoard.mSpaces)
            {
                string checkedStr = "";

                // only 3 possible combinations lol
                int lenStr = node.mNodeID.Length;

                // the bone names are hook_XXX, while the node IDs here are 1, 2, 10, etc
                // so we need to pad the left with 0s to properly match the bone id

                checkedStr = node.mNodeID.PadLeft(3, '0');

                Console.WriteLine("Checked {0}", checkedStr);

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
            }
            catch
            {
                Console.WriteLine("whoops");
            }
        }

        Dictionary<string, PointF> nodePositions;

        BFRES mBfres;
        Board mBoard;

        bool drawFlag = false;
    }
}
