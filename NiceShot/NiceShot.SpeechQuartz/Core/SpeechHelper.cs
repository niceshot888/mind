using SpeechLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NiceShot.SpeechQuartz.Core
{
    public enum VoiceStatus
    {
        Play,
        Ready,
        Pause,
    }

    public class SpeechHelper
    {

        private readonly SpVoice _voice;

        public SpeechHelper()
        {
            _voice = new SpVoice();
        }

        public void Speak(string text, SpeechVoiceSpeakFlags speakFlag = SpeechVoiceSpeakFlags.SVSFDefault)
        {
            _voice.Speak(text, speakFlag);
        }

        public void Pause()
        {
            if (_voice != null)
                _voice.Pause();
        }

        public static void Speech(string text)
        {
            var voice = new SpVoice();
            voice.Rate = -1;
            voice.Volume = 100;
            voice.Voice = voice.GetVoices().Item(0);
            voice.Speak(text);
        }

        public static void TheadStart(string s)
        {
            Thread thread = new Thread(() => Speech(s));
            thread.Start();
        }
    }
}
