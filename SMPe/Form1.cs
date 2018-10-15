using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SMPe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mCSV = new CSV(dialog.FileName, 8, true);

                // this is the part that is hardcoded for maps. yay :D
                foreach (string[] entry in mCSV.mEntries)
                {
                    dataGridView1.Rows.Add(entry[0], entry[1], entry[2], entry[3], entry[4], entry[5], entry[6], entry[7]);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder lines = new StringBuilder();

                // we append our header from earlier
                lines.Append(mCSV.mHeader + Environment.NewLine);

                bool isEmptyRow = false;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 0; i < mCSV.mNumAttributes; i++)
                    {
                        // first we check to see if the row itself is empty
                        // if it is, we discard it and move on
                        if (isRowEmpty(row))
                        {
                            isEmptyRow = true;
                            continue;
                        }

                        // if a cell's value is NULL, we just add a comma to add the null attribute
                        // if not, we just use the value that is there
                        if (row.Cells[i].Value == null)
                            lines.Append(",");
                        else
                            lines.Append(row.Cells[i].Value.ToString() + ",");
                    }

                    // if this row is not empty, we append a newline for the next row
                    // this is here because if the row is empty, it will create a empty line which annoys me
                    if (!isEmptyRow)
                        lines.Append(Environment.NewLine);
                    else
                        isEmptyRow = false;
                }

                File.WriteAllText(saveDialog.FileName, lines.ToString());
            }
        }
        
        /// <summary>
        /// Returns if a row in a DataGridViewRow is empty.
        /// </summary>
        /// <param name="row">The row to determine if it is empty.</param>
        /// <returns>True is empty, False if not.</returns>
        private bool isRowEmpty(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value != null)
                    return false;
            }

            return true;
        }

        CSV mCSV;
    }
}
