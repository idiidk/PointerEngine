using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointerEngine
{
    public class Helper
    {
        public static byte[,] GetButtons()
        {
            byte[,] buttons = new byte[8, 8];
            int tempkey = 0;
            for (byte i = 0; (i <= 7); i++)
            {
                buttons[tempkey, 0] = i;
                tempkey++;
            }
            tempkey = 0;
            for (byte i = 16; (i <= 23); i++)
            {
                buttons[tempkey, 1] = i;
                tempkey++;
            }
            tempkey = 0;
            for (byte i = 32; (i <= 39); i++)
            {
                buttons[tempkey, 2] = i;
                tempkey++;
            }
            tempkey = 0;
            for (byte i = 48; (i <= 55); i++)
            {
                buttons[tempkey, 3] = i;
                tempkey++;
            }
            tempkey = 0;
            for (byte i = 64; (i <= 71); i++)
            {
                buttons[tempkey, 4] = i;
                tempkey++;
            }
            tempkey = 0;
            for (byte i = 80; (i <= 87); i++)
            {
                buttons[tempkey, 5] = i;
                tempkey++;
            }
            tempkey = 0;
            for (byte i = 96; (i <= 103); i++)
            {
                buttons[tempkey, 6] = i;
                tempkey++;
            }
            tempkey = 0;
            for (byte i = 112; (i <= 119); i++)
            {
                buttons[tempkey, 7] = i;
                tempkey++;
            }
            return buttons;
        }

        public static byte[] Combine(byte[] first, byte[] second, byte[] third)
        {
            byte[] ret = new byte[first.Length + second.Length + third.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            Buffer.BlockCopy(third, 0, ret, first.Length + second.Length,
                             third.Length);
            return ret;
        }

        public static byte[] GetControlButtons()
        {
            byte[] buttons = new byte[8];
            buttons[0] = 8;
            buttons[1] = 24;
            buttons[2] = 40;
            buttons[3] = 56;
            buttons[4] = 72;
            buttons[5] = 88;
            buttons[6] = 104;
            buttons[7] = 120;
            return buttons;
        }

        public static Position2D NoteToGridPos(byte note)
        {
            byte[,] grid = GetButtons();
            Position2D holder = new Position2D(0,0);
            for(int xscan = 0; xscan < grid.GetLength(0); xscan++)
            {
                for (int yscan = 0; yscan < grid.GetLength(1); yscan++)
                {
                    if(grid[xscan,yscan] == note)
                    {
                        holder = new Position2D(xscan, yscan);
                    }
                }
            }
            return holder;
        }

        public struct Position2D
        {
            public int x;
            public int y;
            public Position2D(int X, int Y)
            {
                x = X;
                y = Y;
            }
        }

        public enum BasicColors : int {
            FULL_RED = 15,
            HALF_RED = 14,
            DIM_RED = 13,
            FULL_ORANGE_CROSSOVER = 31,
            HALF_ORANGE_CROSSOVER = 30,
            DIM_ORANGE_CROSSOVER = 29,
            DIM_GREEN = 28,
            HALF_GREEN = 50,
            FULL_GREEN = 60,
            FULL_YELLOW = 62,
            FULL_AMBER = 63,
            HALF_AMBER = 48,
            AMBER_RED = 27,
            HALF_YELLOW_RED = 26,
            HALF_GREEN_YELLOW = 49,
            OFF = 12
        }
    }
}
