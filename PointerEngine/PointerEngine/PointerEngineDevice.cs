using radio42.Multimedia.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointerEngine
{
    public class PointerEngineDevice
    {
        byte[,] buttons = new byte[8,8];
        public MidiOutputDevice _outDevice;
        public MidiInputDevice _inDevice;
        public bool isReady;

        public PointerEngineDevice(int indevice, int outdevice)
        {
            OpenDevice(indevice, outdevice);
        }

        private MidiOutputDevice OpenDevice(int indevice, int outdevice)
        {
            buttons = PointerEngineHelper.GetButtons();
            _outDevice = new MidiOutputDevice(indevice);
            _inDevice = new MidiInputDevice(outdevice);
            if (!_outDevice.Open())
            {
                throw new Exception("Midi Open Error");
            }
            else
            {
                isReady = true;
            }
            if (!_inDevice.Open())
            {
                throw new Exception("Midi Open Error " + _inDevice.LastErrorCode.ToString() + _inDevice.Device);
            }
            else
            {
                isReady = true;
            }
            return _outDevice;
        }

        public void StopAndCloseDevice()
        {
            if ((!(_outDevice == null)
                        && _outDevice.IsOpened))
            {
                _outDevice.Close();
                isReady = false;
            }
        }

        public void SendControlChange(byte midistatus, byte controller, byte data2)
        {
            MidiShortMessage msg = new MidiShortMessage();
            msg.Channel = 1;
            msg.Status = midistatus;
            msg.Controller = controller;
            msg.Data2 = data2;
            _outDevice.Send(msg);
        }

        public void SendMsg(byte channel, byte note, byte volume, byte color)
        {
            MidiShortMessage msg = new MidiShortMessage();
            msg.Channel = 1;
            if ((volume == 1))
            {
                msg.Status = (byte) MIDIStatus.NoteOn;
            }
            else
            {
                msg.Status = (byte) MIDIStatus.NoteOff;
            }
            msg.Note = note;
            msg.Velocity = color;
            _outDevice.Send(msg);
        }
    }
}
