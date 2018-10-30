using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMPe.Properties;
using SMPe.io;
using SMPe.bea;
using System.IO;

namespace SMPe
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            bool res = false;

            if (Settings.Default.folderPath == "")
                res = DoFirstSetup();

            if (!res)
                return;
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            GetSMPFolder();
        }

        private bool DoFirstSetup()
        {
           if (!GetSMPFolder())
                return false;

            return true;
        }

        private bool GetSMPFolder()
        {
            MessageBox.Show("You will be asked to select a folder.\nSelect the folder that contains your Super Mario Party dump (Archive folder should be present).");

            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!Filesystem.DoesFolderExist(dialog.SelectedPath + "/Archive"))
                {
                    MessageBox.Show("Invalid folder, does not contain Archive!");
                    treeView1.Enabled = false;
                    return false;
                }
                else
                {
                    Settings.Default.folderPath = dialog.SelectedPath;
                    loadingLabel.Text = "New folder assigned!";
                    treeView1.Enabled = true;
                    return true;
                }
            }
            else
            {
                treeView1.Enabled = false;
                return false;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Editor editor = new Editor(String.Format("{0}/Archive/bds001_{1}.nxonnx32.bea", Settings.Default.folderPath, treeView1.SelectedNode.Tag));
            editor.SetWindowName(treeView1.SelectedNode.Text);
            editor.ShowDialog();
        }
    }
}
