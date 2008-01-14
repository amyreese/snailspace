using System;
using System.Collections.Generic;
using System.Text;

namespace SnailsPace.Config
{
    class VideoConfig : LuaConfig
    {
        private String videoConfigFile = "Config/Video.lua";

        public VideoConfig()
            : base()
        {
            #region Default values

            // Default double values
            Dictionary<String, Double> doubles = new Dictionary<String, Double>();
            doubles.Add("height", 600);
            doubles.Add("width", 800);
            setDefaults(doubles);

            // Default string values
            Dictionary<String, String> strings = new Dictionary<String, String>();
            strings.Add("fullscreen", "yes");
            setDefaults(strings);

            #endregion

            // Load user preferences
            this.readFile(videoConfigFile);

            #region User preference validation

            if (getDouble("height") < 480)
            {
                setDouble("height", 480);
            }

            if (getDouble("width") < 640)
            {
                setDouble("width", 640);
            }

            if (getString("fullscreen").ToLower() != "yes" && getString("fullscreen").ToLower() != "no")
            {
                setString("fullscreen", "no");
            }

            #endregion
        }

        public void save()
        {
            this.writeFile(videoConfigFile);
        }
    }
}
