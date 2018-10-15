using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPe
{
    class CSV
    {
        /// <summary>
        /// Constructor for a CSV.
        /// </summary>
        /// <param name="src">The path to the CSV.</param>
        /// <param name="numAttributes">The number of attributes per cell in the CSV.</param>
        /// <param name="saveHeader">A boolean to determine if we need to read a header.</param>
        public CSV(string src, int numAttributes, bool saveHeader)
        {
            mNumAttributes = numAttributes;

            // the game uses shift-jis encoding for a lot of stuff, sooo...
            StreamReader reader = new StreamReader(src, Encoding.GetEncoding(932));

            string line;
            byte curLine = 0;
            mEntries = new List<string[]>();

            while ((line = reader.ReadLine()) != null)
            {
                // the CSV files in this game are inconsistant
                // some files have a header that describes the cell type, but some don't
                // so we have the coder decide if we should save the header or not
                // and lucky for us, the header is always the first line
                if (curLine == 0 && saveHeader)
                {
                    mHeader = line;
                    curLine++;
                    continue;
                }

                // now we split our line into each attribute, as they are seperated with a comma
                mEntries.Add(line.Split(','));
            }
        }

        public List<string[]> mEntries;
        public string mHeader;
        public int mNumAttributes;
    }
}
