using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SnailsPace.Input
{
    public class KeyCombination
    {
        private Keys[] keys;
        public Keys[] getKeys()
        {
            return (Keys[])keys.Clone();
        }

        /// <summary>
        /// Creates a new KeyCombination object representing the pressing of a multiple keys
        /// </summary>
        /// <param name="keys"></param>
        public KeyCombination(Keys[] keys)
        {
            this.keys = keys;
        }

        /// <summary>
        /// Creates a new KeyCombination object representing the pressing of a single key
        /// </summary>
        /// <param name="key"></param>
        public KeyCombination(Keys key)
        {
            keys = new Keys[1];
            keys[0] = key;
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
                Keys[] objKeys = ((KeyCombination)obj).keys;
                // Might be able to be done faster
                if (objKeys.Length != keys.Length)
                {
                    return false;
                }
                else
                {
                    bool containsKey = false;
                    for (int index = 0; index < keys.Length; index++)
                    {
                        containsKey = false;
                        for (int index2 = 0; index2 < objKeys.Length; index2++)
                        {
                            if (keys[index] == objKeys[index2])
                            {
                                containsKey = true;
                                break;
                            }
                        }
                        if (!containsKey)
                        {
                            return false;
                        }
                    }
                    for (int index = 0; index < objKeys.Length; index++)
                    {
                        containsKey = false;
                        for (int index2 = 0; index2 < keys.Length; index2++)
                        {
                            if (objKeys[index] == keys[index2])
                            {
                                containsKey = true;
                                break;
                            }
                        }
                        if (!containsKey)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashcode = 0;
            for( int index = 0; index < keys.Length; index++ ) {
                hashcode = hashcode + keys[index].GetHashCode();
            }
            return hashcode;
        }

        public override string ToString()
        {
            String output = "";
            for (int index = 0; index < keys.Length; index++)
            {
                if (output.Length != 0)
                {
                    output = output + " + ";
                }
                output = output + keys[index].ToString();
            }
            return output;
        }
    }
}
