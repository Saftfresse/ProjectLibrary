using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectClassLib
{
    public class INIParser
    {
        string[] data;
        string filename = @"Settings";
        string path = @"";

        public INIParser()
        {
            readData();
        }

        public INIParser(string _path, string _filename)
        {
            path = _path;
            filename = _filename;
            readData();
        }

        string fullPath()
        {
            return path + @"\" + filename + ".ini";
        }

        /// <summary>
        /// Reads the INI File and creates it if neccessary.
        /// </summary>
        public void readData()
        {
            //if (!Directory.Exists(Path.GetFullPath(fullPath())))
            //{
            //    Directory.CreateDirectory(Path.GetFullPath(fullPath()));
            //}
            if (!File.Exists(fullPath()))
            {
                File.Create(fullPath());
            }
            else
            {
                data = File.ReadAllLines(fullPath());
                for (int i = 0; i < data.Length; i++)
                {
                    //data[i] = data[i].ToUpper();
                }
            }
        }

        /// <summary>
        /// Sets the PATH for the INI File
        /// </summary>
        /// <param name="_path">The absolute PATH to the File without the filename attached</param>
        public void setINIPath(string _path)
        {
            path = _path;
        }

        /// <summary>
        /// Sets the Value for a given key.
        /// </summary>
        /// <param name="_section">The section/paragraph to search for the key in</param>
        /// <param name="_key">The key</param>
        /// <param name="_value">The new value to be set</param>
        public void setValueForKey(string _section, string _key, string _value)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
            string value = _value.ToUpper();
            int[] range = getSectionRange(_section);

            string[] sec = getValuesBySection(section);

            bool existing = false;

            for (int i = 0; i < sec.Length; i++)
            {
                string[] split = sec[i].Split('=');
                if (split[0] == key)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        if (j >= range[0] && j <= range[1])
                        {
                            if (data[j] == sec[i])
                            {
                                data[j] = key + "=" + value;
                                existing = true;
                            }
                        }
                    }
                }
            }
            if (!existing)
            {
                addEntryForSection(_section, _key, _value);
            }

            File.WriteAllLines(fullPath(), data);

        }

        /// <summary>
        /// Adds a new Key and Value pair to a given section
        /// </summary>
        /// <param name="_section">The section/paragraph to create the key/value pair in</param>
        /// <param name="_key">The key</param>
        /// <param name="_value">The value for the key</param>
        public void addEntryForSection(string _section, string _key, string _value)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
            string value = _value.ToUpper();
            int[] range = getSectionRange(_section);

            string[] sec = getValuesBySection(section);

            bool new_entry = true;
            for (int i = 0; i < sec.Length; i++)
            {
                string[] split = sec[i].Split('=');
                if (split[0] == key)
                {
                    new_entry = false;
                }
            }
            if (new_entry)
            {
                List<string> dataList = data.ToList<string>();
                for (int j = 0; j < dataList.Count; j++)
                {
                    if (j == range[1])
                    {
                        dataList.Insert(j, key + "=" + value);
                    }
                }
                data = dataList.ToArray<string>();
            }
            File.WriteAllLines(fullPath(), data);
            readData();
        }

        /// <summary>
        /// Gets the Value for a given key and section
        /// </summary>
        /// <param name="_section">The section/paragraph to search for the value</param>
        /// <param name="_key">The given key to look for</param>
        /// <returns>The value which belongs to the key in UPPER-KEY</returns>
        public string getValueForKey(string _section, string _key)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
            string value = "";


            string[] sec = getValuesBySection(section);
            List<string> output = new List<string>();
            for (int i = 0; i < sec.Length; i++)
            {
                string[] split = sec[i].Split('=');
                if (split[0] == key)
                {
                    value = split[1];
                }
            }
            return value;
        }

        private int[] getSectionRange(string _section)
        {
            string section = _section.ToUpper();
            int startIndex = -1, endIndex = 0;
            if (data.Length > 0)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    string regex = @"([\[]" + section + @"[\]])";
                    Regex heading = new Regex(regex);
                    Regex postHeading = new Regex(@"([\[].{1,64}[\]])");
                    Regex entry = new Regex(@"(.{1,64}[=].{1,64})");
                    if (heading.IsMatch(data[i]))
                    {
                        if (startIndex == -1)
                        {
                            startIndex = i;
                            endIndex = i;
                        }
                    }
                    else
                    {
                        if (startIndex > -1)
                        {
                            if (entry.IsMatch(data[i]))
                            {
                                endIndex++;
                            }
                            else if (postHeading.IsMatch(data[i]))
                            {
                                endIndex = i - 1;
                                break;
                            }

                        }
                    }
                }
            }
            else
            {
                data = new string[] { "[" + section + "]", "" };
                for (int i = 0; i < data.Length; i++)
                {
                    string regex = @"([\[]" + section + @"[\]])";
                    Regex heading = new Regex(regex);
                    Regex postHeading = new Regex(@"([\[].{1,64}[\]])");
                    Regex entry = new Regex(@"(.{1,64}[=].{1,64})");
                    if (heading.IsMatch(data[i]))
                    {
                        if (startIndex == -1)
                        {
                            startIndex = i;
                            endIndex = i;
                        }
                    }
                    else
                    {
                        if (startIndex > -1)
                        {
                            if (entry.IsMatch(data[i]))
                            {
                                endIndex++;
                            }
                            else if (postHeading.IsMatch(data[i]))
                            {
                                endIndex = i - 1;
                                break;
                            }

                        }
                    }
                }
            }
            return new int[] { startIndex, endIndex };
        }

        public string[] getValuesBySection(string _section)
        {
            string section = _section.ToUpper();
            int startIndex = -1, endIndex = 0;
            for (int i = 0; i < data.Length; i++)
            {
                string regex = @"([\[]" + section + @"[\]])";
                Regex heading = new Regex(regex);
                Regex postHeading = new Regex(@"([\[].{1,64}[\]])");
                Regex entry = new Regex(@"(.{1,64}[=].{1,64})");
                if (heading.IsMatch(data[i]))
                {
                    if (startIndex == -1)
                    {
                        startIndex = i;
                        endIndex = i;
                    }
                }
                else
                {
                    if (startIndex > -1)
                    {
                        if (entry.IsMatch(data[i]))
                        {
                            endIndex++;
                        }
                        else if (postHeading.IsMatch(data[i]))
                        {
                            endIndex = i - 1;
                            break;
                        }

                    }
                }
            }
            List<string> o = new List<string>();
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                o.Add(data[i]);
            }
            return o.ToArray<string>();
        }

        public string getFirstValueBySection(string _section)
        {
            string[] allVals = getValuesBySection(_section);
            return allVals[0];
        }
    }
}
