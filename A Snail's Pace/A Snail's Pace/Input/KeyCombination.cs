using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace A_Snail_s_Pace.Input
{
    public class KeyCombination
    {
        private List<Keys> keys;
        public List<Keys> getKeys()
        {
            List<Keys> clone = new List<Keys>(keys);
            return clone;
        }

        /// <summary>
        /// Creates a new KeyCombination object representing the pressing of a multiple keys
        /// </summary>
        /// <param name="keys"></param>
        public KeyCombination(List<Keys> keys)
        {
            this.keys = keys;
        }

        /// <summary>
        /// Creates a new KeyCombination object representing the pressing of a multiple keys
        /// </summary>
        /// <param name="keys"></param>
        public KeyCombination(Keys[] keys)
        {
            this.keys = new List<Keys>();
            foreach (Keys key in keys)
            {
                this.keys.Add(key);
            }
        }

        /// <summary>
        /// Creates a new KeyCombination object representing the pressing of a single key
        /// </summary>
        /// <param name="key"></param>
        public KeyCombination(Keys key)
        {
            keys = new List<Keys>();
            keys.Add(key);
        }

        /// <summary>
        /// Checks to see if this KeyCombination is the same as another KeyCombination
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(KeyCombination))
            {
                // This is done this way just for quickness-sake.
                // There is almost definately a way we could make this faster
                List<Keys> objKeys = ((KeyCombination)obj).keys;
                foreach (Keys key in objKeys)
                {
                    if (!keys.Contains(key))
                    {
                        return false;
                    }
                }
                foreach (Keys key in keys)
                {
                    if (!objKeys.Contains(key))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashcode = 0;
            foreach (Keys key in keys)
            {
                hashcode = hashcode + key.GetHashCode();
            }
            return hashcode;
        }

        public override string ToString()
        {
            String output = "";
            foreach (Keys key in keys)
            {
                if (output.Length != 0)
                {
                    output = output + " + ";
                }
                output = output + key.ToString();
            }
            return output;
        }
    }
}
