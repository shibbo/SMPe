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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPe
{
    class Helper
    {
        public struct Vector2i
        {
            public int X;
            public int Y;
        }

        public struct Vector3f
        {
            public float X;
            public float Y;
            public float Z;
        }

        public struct Vector4f
        {
            public float X;
            public float Y;
            public float Z;
            public float H;
        }

        public struct Color4
        {
            public byte R;
            public byte G;
            public byte B;
            public byte A;
        }


        public static Dictionary<string, string> mSimpleNodeNames = new Dictionary<string, string>()
        {
            { "PLUS", "Blue Space" },
            { "MINUS", "Red Space" },
            { "LUCKY", "Lucky Space" },
            { "DONT_ENTRY", "Non-Enterable" },
            { "HATENA_1", "Event Space 1" },
            { "HATENA_2", "Event Space 2" },
            { "HATENA_3", "Event Space 3" },
            { "HATENA_4", "Event Space 4" },
            { "START", "Starting Point" },
            { "MARK_PC", "Character Start Point" },
            { "MARK_STAR", "Star Position" },
            { "MARK_STAROBJ", "Toadette Position" },
            { "SUPPORT", "Ally Space" },
            { "HAPPENING", "Unlucky Space" },
            { "ITEM", "Item Space" },
            { "BATTAN", "Whomp" },
            { "DOSSUN", "Thwomp" },
            { "JUGEMU_OBJ", "Lakitu Cloud" },
            { "JUGEMU", "Lakitu Space" },
            { "JOYCON", "Versus Space" },
            { "TREASURE_OBJ", "Treasure Chest" },
            { "SHOP_A", "Shop 1 (Normal)" },
            { "SHOP_B", "Shop 2 (Special)" },
            { "SHOP_C", "Shop 3" },
            { "HARD_SHOP", "Shop (Force Buy)" },
            { "ICECREAM_SHOP", "Ice Cream Shop" },
            { "CLAYPIPE", "Pipe" },
            { "CLAYPIPE_RED", "Red Pipe" },
            { "PRESENT_BOX", "Present Space" },
            { "PRESENT_BOX_OBJ", "Present Object" },
            { "TURNOUT_SWITCH", "Conveyer Belt Space" },
            { "NPC_BOMBHEI", "Bob-omb Space" },
            { "", "Branch" }
        };

        public static Dictionary<string, Color> mNodeTypeToColor = new Dictionary<string, Color>()
        {
            { "PLUS", Color.Blue },
            { "MINUS", Color.Red },
            { "LUCKY", Color.LightGreen },
            { "HATENA_1", Color.DarkOliveGreen },
            { "HATENA_2", Color.DarkOliveGreen },
            { "HATENA_3", Color.DarkOliveGreen },
            { "HATENA_4", Color.DarkOliveGreen },
            { "ITEM", Color.Green },
            { "ITEM_2", Color.Green },
            { "SUPPORT", Color.SlateBlue },
            { "START", Color.DarkGreen },
            { "MARK_PC", Color.DarkViolet },
            { "JOYCON", Color.Orange },
            { "MARK_STAROBJ", Color.Pink },
            { "HAPPENING", Color.DarkRed },
            { "SHOP_A", Color.Silver },
            { "SHOP_B", Color.Silver },
            { "SHOP_C", Color.Silver },
            { "", Color.White }
        };

    }
}
