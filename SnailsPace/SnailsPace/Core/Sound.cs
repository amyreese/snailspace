using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace SnailsPace.Core
{
    class Sound
    {
        // XACT crap
        AudioEngine audioEngine;
        SoundBank soundBank;
        WaveBank waveBank;

        // Create the appropriate XACT crap.
        public Sound()
        {
            audioEngine = new AudioEngine("Resources/Audio/SnailsPace.xgs");
            soundBank = new SoundBank(audioEngine, "Resources/Audio/SnailsPace.xsb");
            waveBank = new WaveBank(audioEngine, "Resources/Audio/SnailsPace.xwb");
        }

        // Fire-and-forget sound
        public void play(String cue)
        {
            soundBank.PlayCue(cue);
        }

        // Pass the update call to the AudioEngine
        public void update()
        {
            audioEngine.Update();
        }
    }
}
