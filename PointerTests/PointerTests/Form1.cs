using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PointerEngine;
using radio42.Multimedia.Midi;

namespace PointerTests
{
    public partial class Form1 : Form
    {
        static PointerEngineDevice ped = new PointerEngineDevice(1, 0);
        static PointerEngineRenderer per = new PointerEngineRenderer(ped);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PointerEngineInput pei = new PointerEngineInput(ped, clickhandler);
            per.ClearPad();
            per.DrawSquare(3, 3, 0, 0, 60, true);
            per.DrawSquare(3, 3, 4, 0, 60, false);
            per.DrawPixel(6, 7, (int)PointerEngineHelper.BasicColors.HALF_RED, false);
        }

        private void clickhandler(MidiMessageEventArgs mmea)
        {
            if (mmea.ShortMessage != null)
            {
                PointerEngineHelper.Position2D pos = PointerEngineHelper.NoteToGridPos(mmea.ShortMessage.Note);
                per.DrawPixel(pos.x, pos.y, (int)PointerEngineHelper.BasicColors.FULL_AMBER, mmea.ShortMessage.Velocity == 127 ? false : true);
            }
        }
    }
}
