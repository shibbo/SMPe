using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPe
{
    class Board
    {
        public Board(string src)
        {
            StreamReader reader = new StreamReader(src, Encoding.GetEncoding(932));

            string line;
            byte curLine = 0;

            mSpaces = new List<SpaceNode>();

            while ((line = reader.ReadLine()) != null)
            {
                if (curLine == 0)
                {
                    curLine++;
                    continue;
                }

                mSpaces.Add(new SpaceNode(line));
            }
        }

        public List<SpaceNode> mSpaces;
    }
}
