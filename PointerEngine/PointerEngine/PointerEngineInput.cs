using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using radio42.Multimedia.Midi;

namespace PointerEngine
{
    public class PointerEngineInput
    {
        private Action<MidiMessageEventArgs> callbackHandler;
        public PointerEngineInput(PointerEngineDevice ped, Action<MidiMessageEventArgs> callback)
        {
            if (ped.isReady)
            {
                this.callbackHandler = callback;
                ped._inDevice.AutoPairController = true;
                ped._inDevice.MessageFilter = MIDIMessageType.SystemRealtime | MIDIMessageType.SystemExclusive;
                ped._inDevice.MessageReceived += EventHandler;
                ped._inDevice.Start();
            } else
            {
                throw new Exception("PointerEngineDevice not ready! Did you call OpenDevice()?");
            }
        }
        public void EventHandler(Object o, MidiMessageEventArgs mmea)
        {
            this.callbackHandler(mmea);
        }
    }
}
