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

        Dictionary<String, Cue> cues;
        Dictionary<String, bool> repeat;

        // Create the appropriate XACT crap.
        public Sound()
        {
            audioEngine = new AudioEngine("Resources/Audio/SnailsPace.xgs");
            soundBank = new SoundBank(audioEngine, "Resources/Audio/SnailsPace.xsb");
            waveBank = new WaveBank(audioEngine, "Resources/Audio/SnailsPace.xwb");

            cues = new Dictionary<string,Cue>();
            repeat = new Dictionary<string,bool>();
        }

        // Fire-and-forget sound
        public void play(String cue) { play(cue, true); }
        public void play(String cue, bool overlap)
        {
            cache(cue);
            if (cues[cue].IsPrepared)
            {
                cues[cue].Play();
            }
            else if (cues[cue].IsPaused)
            {
                cues[cue].Resume();
            }
            else if (overlap)
            {
                recache(cue);
                cues[cue].Play();
            }
        }

        // Start a repeating sound
        public void playRepeat(String cue)
        {
            cache(cue);
            repeat[cue] = true;
            if (cues[cue].IsPrepared)
            {
                cues[cue].Play();
            }
            else if (cues[cue].IsPaused)
            {
                cues[cue].Stop(AudioStopOptions.Immediate);
                recache(cue);
                cues[cue].Play();
            }
        }

        public void pause(String cue)
        {
            cache(cue);
            if (cues[cue].IsPlaying)
            {
                cues[cue].Pause();
            }
        }

        // Stop a sound
        public void stop(String cue)
        {
            cache(cue);
            if (cues[cue].IsPlaying || cues[cue].IsPaused)
            {
                cues[cue].Stop(AudioStopOptions.AsAuthored);
            }
            repeat[cue] = false;
        }

        // Stop all types of a sound.
        public void stopAll()
        {
            List<Cue>.Enumerator allCues = new List<Cue>(cues.Values).GetEnumerator();
            while (allCues.MoveNext())
            {
                Cue cue = allCues.Current;
                cue.Stop(AudioStopOptions.Immediate);
            }
            allCues.Dispose();
        }

        // Cache the Cue object and whether it should be repeating
        private void cache(String cue)
        {
            if (!cues.ContainsKey(cue))
            {
                cues.Add(cue, soundBank.GetCue(cue));
                repeat.Add(cue, false);
            }
        }

        // Recache a new version of a cue
        private void recache(String cue)
        {
            cues[cue] = soundBank.GetCue(cue);
        }

        // Set a global variable in the AudioEngine
        public void set(String name, float value)
        {
            audioEngine.SetGlobalVariable(name, value);
        }

        // Get a global variable from the AudioEngine
        public float get(String name)
        {
            return audioEngine.GetGlobalVariable(name);
        }

        // Pass the update call to the AudioEngine
        public void update()
        {
            List<String> allCues = new List<string>(cues.Keys);
            List<String>.Enumerator cueKeys = allCues.GetEnumerator();
            while(cueKeys.MoveNext())
            {
                String cue = cueKeys.Current;
                bool repeatable = repeat[cue];

                if (cues[cue].IsStopped)
                {
                    recache(cue);

                    if (repeatable)
                    {
                        cues[cue].Play();
                    }
                }
            }
            cueKeys.Dispose();

            audioEngine.Update();
        }
    }
}
