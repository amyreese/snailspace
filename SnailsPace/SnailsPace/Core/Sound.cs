using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace SnailsPace.Core
{
    class Sound
    {
        AudioEngine audioEngine;
        public SoundBank soundBank;
        WaveBank waveBank;

        public Sound()
        {
            audioEngine = new AudioEngine("Resources/Audio/SnailsPace.xgs");
            soundBank = new SoundBank(audioEngine, "Resources/Audio/SnailsPace.xsb");
            waveBank = new WaveBank(audioEngine, "Resources/Audio/SnailsPace.xwb");
        }

        public void update()
        {
            audioEngine.Update();
        }
    }
}
