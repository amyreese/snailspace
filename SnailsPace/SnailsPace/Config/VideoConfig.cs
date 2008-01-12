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
            this.readFile(videoConfigFile);
        }

        public void save()
        {
            this.writeFile(videoConfigFile);
        }
    }
}
