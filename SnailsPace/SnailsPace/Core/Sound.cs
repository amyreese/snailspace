using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace SnailsPace.Core
{
    class Sound
    {
        // XACT components.
        AudioEngine audioEngine;
        SoundBank soundBank;
        WaveBank waveBank;

        Dictionary<String, Cue> cues;
        Dictionary<String, bool> repeat;

		/// <summary>
		/// Create the appropriate XACT components.
		/// </summary>
        public Sound()
        {
            audioEngine = new AudioEngine("Resources/Audio/SnailsPace.xgs");
            soundBank = new SoundBank(audioEngine, "Resources/Audio/SnailsPace.xsb");
            waveBank = new WaveBank(audioEngine, "Resources/Audio/SnailsPace.xwb");

            cues = new Dictionary<string,Cue>();
            repeat = new Dictionary<string,bool>();
        }

        /// <summary>
		/// Fire-and-forget sound.
        /// </summary>
        /// <param name="cue"></param>
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

        /// <summary>
		/// Start a repeating sound.
        /// </summary>
        /// <param name="cue"></param>
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

		/// <summary>
		/// Pause a playing sound.
		/// </summary>
		/// <param name="cue"></param>
        public void pause(String cue)
        {
            cache(cue);
            if (cues[cue].IsPlaying)
            {
                cues[cue].Pause();
            }
        }

        /// <summary>
		/// Stop a sound.
        /// </summary>
        /// <param name="cue"></param>
        public void stop(String cue)
        {
            cache(cue);
            if (cues[cue].IsPlaying || cues[cue].IsPaused)
            {
                cues[cue].Stop(AudioStopOptions.AsAuthored);
            }
            repeat[cue] = false;
        }

        /// <summary>
		/// Stop all sounds.
        /// </summary>
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

        /// <summary>
		/// Cache the Cue object and whether it should be repeating.
        /// </summary>
        /// <param name="cue"></param>
        private void cache(String cue)
        {
            if (!cues.ContainsKey(cue))
            {
                cues.Add(cue, soundBank.GetCue(cue));
                repeat.Add(cue, false);
            }
        }

        /// <summary>
		/// Recache a new version of a cue.
        /// </summary>
        /// <param name="cue"></param>
        private void recache(String cue)
        {
            cues[cue] = soundBank.GetCue(cue);
        }

        /// <summary>
		/// Set a global variable in the AudioEngine.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public void set(String name, float value)
        {
            audioEngine.SetGlobalVariable(name, value);
        }

        /// <summary>
		/// Get a global variable from the AudioEngine.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The value of the variable.</returns>
        public float get(String name)
        {
            return audioEngine.GetGlobalVariable(name);
        }

        /// <summary>
		/// Pass the update call to the AudioEngine.
        /// </summary>
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
