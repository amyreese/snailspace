using System;
using System.Collections.Generic;
using System.Text;

namespace A_Snail_s_Pace.Config
{
    class VideoConfig : LuaConfig
    {
        private String videoConfigFile = "config/video.lua";

        public VideoConfig()
            : base()
        {
            Dictionary<String, Double> doubles = new Dictionary<string, double>();

            // Default values
            doubles.Add("height", 600);
            doubles.Add("width", 800);
            setDefaults(doubles);

            // Load user preferences
            this.readFile(videoConfigFile);

            // Validation
            if (getDouble("height") < 480)
            {
                setDouble("height", 480);
            }

            if (getDouble("width") < 640)
            {
                setDouble("width", 640);
            }
        }

        public void save()
        {
            this.writeFile(videoConfigFile);
        }
    }
}
