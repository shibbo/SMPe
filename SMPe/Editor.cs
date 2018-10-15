using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mCSV = new CSV(dialog.FileName, 8, true);

                this.Text = String.Format("{0} -- {1}", this.Text, Path.GetFileName(dialog.FileName));

                // this is the part that is hardcoded for maps. yay :D
                foreach (string[] entry in mCSV.mEntries)
                {
                    dataGridView1.Rows.Add(entry[0], entry[1], entry[2], entry[3], entry[4], entry[5], entry[6], entry[7]);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*"
            };

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

        /// <summary>
        /// Checks to see if a value exists in the datagrid.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>True if it exists, false if not.</returns>
        private bool valueExists(string value)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString() == value)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks to see if a value exists within a certain cell in the datagrid.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <param name="cellIdx">The index of the cell.</param>
        /// <returns>True if it exists, false if not.</returns>
        private bool valueExists(string value, int cellIdx)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[cellIdx].Value.ToString() == value)
                    return true;
            }

            return false;
        }

        CSV mCSV;
    }
}
