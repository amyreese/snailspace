using System;   
using System.Collections.Generic;
using System.IO;
using System.Text;
using LuaInterface;

namespace SnailsPace
{
    class LuaConfig
    {
        // Default value dictionaries
        private Dictionary<String, Double> DoubleDefaultValues;
        private Dictionary<String, String> StringDefaultValues;

        // User preference dictionaries
        private Dictionary<String, Double> DoubleValues;
        private Dictionary<String, String> StringValues;
        
        // Track changes to the configuration
        private Boolean changed = false;

        // The Lua interpreter
        private Lua lua;

        /**
         * Initialize the dictionaries and the interpreter
         */
        public LuaConfig() : this( new Dictionary<String, Double>(), new Dictionary<String, String>() )
        {     
        }

        public LuaConfig(Dictionary<String, Double> defaultDoubles, Dictionary<String, String> defaultStrings)
        {
            lua = new Lua();

            DoubleDefaultValues = new Dictionary<String,Double>(defaultDoubles);
            StringDefaultValues = new Dictionary<String,String>(defaultStrings);

            DoubleValues = new Dictionary<String, Double>();
            StringValues = new Dictionary<String, String>();
        }

        /**
         * Return the combined Double values
         */
        public Dictionary<String, Double> getDoubles()
        {
            Dictionary<String, Double> Doubles = new Dictionary<String, Double>(DoubleDefaultValues);
            Dictionary<String, Double>.Enumerator DoubleEnumerator = Doubles.GetEnumerator();
            while( DoubleEnumerator.MoveNext() )
            {
                if (Doubles.ContainsKey(DoubleEnumerator.Current.Key))
                {
                    Doubles[DoubleEnumerator.Current.Key] = DoubleEnumerator.Current.Value;
                }
                else
                {
                    Doubles.Add(DoubleEnumerator.Current.Key, DoubleEnumerator.Current.Value);
                }
            }
            DoubleEnumerator.Dispose();
            return Doubles;
        }

        /**
         * Return the combined String values
         */
        public Dictionary<String, String> getStrings()
        {
            Dictionary<String, String> Strings = new Dictionary<String,String>(StringDefaultValues);
            Dictionary<String, String>.Enumerator StringEnumerator = StringValues.GetEnumerator();
            while( StringEnumerator.MoveNext() )
            {
                if (Strings.ContainsKey(StringEnumerator.Current.Key))
                {
                    Strings[StringEnumerator.Current.Key] = StringEnumerator.Current.Value;
                }
                else
                {
                    Strings.Add(StringEnumerator.Current.Key, StringEnumerator.Current.Value);
                }
            }
            StringEnumerator.Dispose();
            return Strings;
        }

        /**
         * Set a configuration value of type Double
         */
        public void setDouble(String key, Double value)
        {
            if (DoubleValues.ContainsKey(key))
            {
                if (DoubleValues[key] != value)
                {
                    DoubleValues[key] = value;
                    changed = true;
                }
            }
            else
            {
                DoubleValues.Add(key, value);
                changed = true;
            }
        }

        public void setInt(String key, int value)
        {
            setDouble(key, value);
        }

        /**
         * Set a configuration value of type String
         */
        public void setString(String key, String value)
        {
            if (StringValues.ContainsKey(key))
            {
                if (StringValues[key] != value)
                {
                    StringValues[key] = value;
                    changed = true;
                }
            }
            else
            {
                StringValues.Add(key, value);
                changed = true;
            }
        }

        /**
         * Get a configuration value of type Double
         */
        public Double getDouble(String key)
        {
            if (DoubleValues.ContainsKey(key))
            {
                return DoubleValues[key];
            }
            else if (DoubleDefaultValues.ContainsKey(key))
            {
                return DoubleDefaultValues[key];
            }
            else
            {
                return 0;
            }
        }

        public int getInt(String key)
        {
            return (int)getDouble(key);
        }

        /**
         * Get a configuration value of type String
         */
        public String getString(String key)
        {
            if (StringValues.ContainsKey(key))
            {
                return StringValues[key];
            }
            else if (StringDefaultValues.ContainsKey(key))
            {
                return StringDefaultValues[key];
            }
            else
            {
                return null;
            }
        }

        /**
         * Execute a file in the Lua interpreter and grab appropriate values.
         */
        public void readFile(String filename)
        {
            try {
                lua.DoFile(filename);
            } catch( LuaException e ) {
            }

            Dictionary<String, Double>.Enumerator DoubleDefaultEnumerator = DoubleDefaultValues.GetEnumerator();
            while( DoubleDefaultEnumerator.MoveNext() )
            {
                Object value = lua[DoubleDefaultEnumerator.Current.Key];
                if (null != value)
                {
                    DoubleValues.Add(DoubleDefaultEnumerator.Current.Key, (Double)value);
                }
            }
            DoubleDefaultEnumerator.Dispose();

            Dictionary<String, String>.Enumerator StringDefaultEnumerator = StringDefaultValues.GetEnumerator();
            while (StringDefaultEnumerator.MoveNext())
            {
                Object value = lua[StringDefaultEnumerator.Current.Key];
                if (null != value)
                {
                    StringValues.Add(StringDefaultEnumerator.Current.Key, (String)value);
                }
            }
            StringDefaultEnumerator.Dispose();
        }

        /**
         * Output the configuration to the given file (if changed)
         */
        public void writeFile(String filename)
        {
            if (!changed)
            {
                return;
            }

            TextWriter file = new StreamWriter(filename);

            Dictionary<String, Double>.Enumerator DoubleEnumerator = DoubleValues.GetEnumerator();
            while( DoubleEnumerator.MoveNext() )
            {
                file.WriteLine(DoubleEnumerator.Current.Key + " = " + DoubleEnumerator.Current.Value);
            }
            DoubleEnumerator.Dispose();

            Dictionary<String, String>.Enumerator StringEnumerator = StringValues.GetEnumerator();
            while( StringEnumerator.MoveNext() )
            {
                file.WriteLine(StringEnumerator.Current.Key + " = " + StringEnumerator.Current.Value);
            }
            StringEnumerator.Dispose();

            file.Close();
        }

        /**
         * Set the default values for the configuration dictionaries
         */
        public void setDefaults(Dictionary<String, Double> values)
        {
            Dictionary<String, Double>.Enumerator DoubleEnumerator = values.GetEnumerator();
            while (DoubleEnumerator.MoveNext())
            {
                DoubleDefaultValues.Add(DoubleEnumerator.Current.Key, DoubleEnumerator.Current.Value);
            }
            DoubleEnumerator.Dispose();
        }

        public void setDefaults(Dictionary<String, String> values)
        {
            Dictionary<String, String>.Enumerator StringEnumerator = values.GetEnumerator();
            while (StringEnumerator.MoveNext())
            {
                StringDefaultValues.Add(StringEnumerator.Current.Key, StringEnumerator.Current.Value);
            }
            StringEnumerator.Dispose();
        }

    }
}
