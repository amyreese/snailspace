using System;   
using System.Collections.Generic;
using System.IO;
using System.Text;
using LuaInterface;

namespace A_Snail_s_Pace
{
    class LuaConfig
    {
        // Default value dictionaries
        private Dictionary<string, double> doubleDefaultValues;
        private Dictionary<string, string> stringDefaultValues;

        // User preference dictionaries
        private Dictionary<string, double> doubleValues;
        private Dictionary<string, string> stringValues;
        
        // Track changes to the configuration
        private Boolean changed = false;

        // The Lua interpreter
        private Lua lua;

        /**
         * Initialize the dictionaries and the interpreter
         */
        public LuaConfig() : this( new Dictionary<string, double>(), new Dictionary<string, string>() )
        {     
        }

        public LuaConfig(Dictionary<String, Double> defaultDoubles, Dictionary<String, String> defaultStrings)
        {
            lua = new Lua();

            doubleDefaultValues = new Dictionary<string,double>(defaultDoubles);
            stringDefaultValues = new Dictionary<string,string>(defaultStrings);

            doubleValues = new Dictionary<string, double>();
            stringValues = new Dictionary<string, string>();
        }

        /**
         * Return the combined double values
         */
        public Dictionary<String, Double> getDoubles()
        {
            Dictionary<String, Double> doubles = new Dictionary<String, Double>(doubleDefaultValues);

            foreach (KeyValuePair<String, Double> pair in doubleValues)
            {
                if (doubles.ContainsKey(pair.Key))
                {
                    doubles[pair.Key] = pair.Value;
                }
                else
                {
                    doubles.Add(pair.Key, pair.Value);
                }
            }

            return doubles;
        }

        /**
         * Return the combined string values
         */
        public Dictionary<String, String> getStrings()
        {
            Dictionary<String, String> strings = new Dictionary<string,string>(stringDefaultValues);

            foreach (KeyValuePair<String, String> pair in stringValues)
            {
                if (strings.ContainsKey(pair.Key))
                {
                    strings[pair.Key] = pair.Value;
                }
                else
                {
                    strings.Add(pair.Key, pair.Value);
                }
            }

            return strings;
        }

        /**
         * Set a configuration value of type double
         */
        public void setDouble(string key, double value)
        {
            if (doubleValues.ContainsKey(key))
            {
                if (doubleValues[key] != value)
                {
                    doubleValues[key] = value;
                    changed = true;
                }
            }
            else
            {
                doubleValues.Add(key, value);
                changed = true;
            }
        }

        public void setInt(string key, int value)
        {
            setDouble(key, value);
        }

        /**
         * Set a configuration value of type string
         */
        public void setString(string key, string value)
        {
            if (stringValues.ContainsKey(key))
            {
                if (stringValues[key] != value)
                {
                    stringValues[key] = value;
                    changed = true;
                }
            }
            else
            {
                stringValues.Add(key, value);
                changed = true;
            }
        }

        /**
         * Get a configuration value of type double
         */
        public double getDouble(string key)
        {
            if (doubleValues.ContainsKey(key))
            {
                return doubleValues[key];
            }
            else if (doubleDefaultValues.ContainsKey(key))
            {
                return doubleDefaultValues[key];
            }
            else
            {
                return 0;
            }
        }

        public int getInt(string key)
        {
            return (int)getDouble(key);
        }

        /**
         * Get a configuration value of type string
         */
        public string getString(string key)
        {
            if (stringValues.ContainsKey(key))
            {
                return stringValues[key];
            }
            else if (stringDefaultValues.ContainsKey(key))
            {
                return stringDefaultValues[key];
            }
            else
            {
                return null;
            }
        }

        /**
         * Execute a file in the Lua interpreter and grab appropriate values.
         */
        public void readFile(string filename)
        {
            try {
                lua.DoFile(filename);
            } catch( LuaException e ) {
            }

            foreach (KeyValuePair<string, double> pair in doubleDefaultValues)
            {
                Object value = lua[pair.Key];
                if (null != value)
                {
                    doubleValues.Add(pair.Key, (double)value);
                }
            }

            foreach (KeyValuePair<string, string> pair in stringDefaultValues)
            {
                Object value = lua[pair.Key];
                if (null != value)
                {
                    stringValues.Add(pair.Key, (string)value);
                }
            }
        }

        /**
         * Output the configuration to the given file (if changed)
         */
        public void writeFile(string filename)
        {
            if (!changed)
            {
                return;
            }

            TextWriter file = new StreamWriter(filename);

            foreach (KeyValuePair<string, double> pair in doubleValues)
            {
                file.WriteLine(pair.Key + " = " + pair.Value);
            }

            foreach (KeyValuePair<string, string> pair in stringValues)
            {
                file.WriteLine(pair.Key + " = " + pair.Value);
            }

            file.Close();
        }

        /**
         * Set the default values for the configuration dictionaries
         */
        public void setDefaults(Dictionary<string, double> values)
        {
            foreach (KeyValuePair<string, double> pair in values)
            {
                doubleDefaultValues.Add(pair.Key, pair.Value);
            }
        }

        public void setDefaults(Dictionary<string, string> values)
        {
            foreach (KeyValuePair<string, string> pair in values)
            {
                stringDefaultValues.Add(pair.Key, pair.Value);
            }
        }

    }
}
