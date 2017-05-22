using radio42.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointerEngine { 
    public class Renderer
    {
        Device _ped;
        public Renderer(Device ped)
        {
            this._ped = ped;
        }

        public void ClearPad()
        {
            MidiSysExMessage msg1 = new MidiSysExMessage(false, this._ped._outDevice.Device);
            msg1.CreateBuffer(new Byte[] { 240, 0, 32, 41, 9, 0, 247 });
            if (!this._ped._outDevice.Send(msg1))
            {
                Console.WriteLine("Error sending system-exclusive message!");
            }
            _ped.SendControlChange(176, 0, 0);
        }

        public void DrawPixel(int x, int y, int color, bool turnoff)
        {
                this._ped.SendMsg(1, Helper.GetButtons()[x,y], !turnoff ? (byte) 1 : (byte) 0, Convert.ToByte(color));
        }

        public void DrawLine(int startx, int starty, int stopx, int stopy, int color, bool turnoff)
        {
            byte[,] btns = Helper.GetButtons();
            List<Helper.Position2D> path = new List<Helper.Position2D>();
            List<int> coordsx = new List<int>();
            List<int> coordsy = new List<int>();
            int currx = startx;
            int curry = starty;
            bool donex = false;
            bool doney = false;
            for (int y = 0; y < 9; y++)
            {
                    coordsy.Add(curry);
                    if (stopy > y)
                    {
                        curry++;
                    }
                    else if (stopy < y)
                    {
                        curry--;
                    }
                    else if (stopy == y)
                    {
                        doney = true;
                    }
                } 
            
            for (int x = 0; x < 9; x++)
            {
                    coordsx.Add(currx);
                    if (stopx > x)
                    {
                        currx++;
                    }
                    else if (stopx < x)
                    {
                        currx--;
                    }
                    else if (stopx == x)
                    {
                        donex = true;
                    }
            }
            for(int c = 0; c < coordsx.Count; c++)
            {
                path.Add(new Helper.Position2D(coordsx[c], coordsy[c]));
            }
            for (int i = 0; i < path.Count; i++)
            {
                if (path[i].x > 7 || path[i].y > 7 || path[i].x < 0 || path[i].y < 0)
                {

                }
                else
                {
                    Console.WriteLine(path[i].x + " - " + path[i].y);
                    DrawPixel(path[i].x, path[i].y, color, turnoff);
                }
            }
        }

        public void DrawScrollingText(string text)
        {
            MidiSysExMessage msg1 = new MidiSysExMessage(false, this._ped._outDevice.Device);
            byte[] textbytes = Encoding.ASCII.GetBytes(text);
            byte[] start = new byte[] { 240, 0, 32, 41, 9, 124 };
            byte[] end = new byte[] { 247 };
            msg1.CreateBuffer(Helper.Combine(start, textbytes, end));
            if (!this._ped._outDevice.Send(msg1))
            {
                Console.WriteLine("Error sending system-exclusive message!");
            }

        }


        public void DrawSquare(int width, int height, int startx, int starty, int color, bool hollow) {
            if (hollow)
            {
                for (int x = startx; x <= width+startx; x++)
                {
                    for (int y = starty; y <= height+starty; y++)
                    {
                        this._ped.SendMsg(1, Helper.GetButtons()[x, y], 1, Convert.ToByte(color));
                    }
                }
                for (int x = startx + 1; x <= width - 1+startx; x++)
                {
                    for (int y = starty + 1; y <= height - 1+starty; y++)
                    {
                        this._ped.SendMsg(1, Helper.GetButtons()[x, y], 0, Convert.ToByte(color));
                    }
                }
            } else
            {
                for (int x = startx; x <= width+startx; x++)
                {
                    for (int y = starty; y <= height+starty; y++)
                    {
                        this._ped.SendMsg(1, Helper.GetButtons()[x, y], 1, Convert.ToByte(color));
                    }
                }
            }
        }
    }
}
