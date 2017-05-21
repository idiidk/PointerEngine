using System;
namespace PointerEngine { 
    public class PointerEngineRenderer
    {
        PointerEngineDevice _ped;
        public PointerEngineRenderer(PointerEngineDevice ped)
        {
            this._ped = ped;
        }

        public void ClearPad()
        {
            _ped.SendControlChange(176, 0, 0);
        }

        public void DrawPixel(int x, int y, int color, bool turnoff)
        {
                this._ped.SendMsg(1, PointerEngineHelper.GetButtons()[x,y], !turnoff ? (byte) 1 : (byte) 0, Convert.ToByte(color));
        }

        public void DrawSquare(int width, int height, int startx, int starty, int color, bool hollow) {
            if (hollow)
            {
                for (int x = startx; x <= width+startx; x++)
                {
                    for (int y = starty; y <= height+starty; y++)
                    {
                        this._ped.SendMsg(1, PointerEngineHelper.GetButtons()[x, y], 1, Convert.ToByte(color));
                    }
                }
                for (int x = startx + 1; x <= width - 1+startx; x++)
                {
                    for (int y = starty + 1; y <= height - 1+starty; y++)
                    {
                        this._ped.SendMsg(1, PointerEngineHelper.GetButtons()[x, y], 0, Convert.ToByte(color));
                    }
                }
            } else
            {
                for (int x = startx; x <= width+startx; x++)
                {
                    for (int y = starty; y <= height+starty; y++)
                    {
                        this._ped.SendMsg(1, PointerEngineHelper.GetButtons()[x, y], 1, Convert.ToByte(color));
                    }
                }
            }
        }
    }
}
