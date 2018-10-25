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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPe
{
    class Board
    {
        public Board(ref StreamReader reader)
        {
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
