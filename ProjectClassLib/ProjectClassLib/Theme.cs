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
    public class Theme
    {
        string[] data;
        string path = @"Themes";
        string filename = "Default";

        public Theme() { readData(); }

        public Theme(string _path) {
            path = _path;
            readData();
        }

        public Theme(string _path, string _filename)
        {
            path = _path;
            filename = _filename;
            readData();
        }
        
        public DashStyle getStyle(string _style)
        {
            string up = _style.ToUpper();
            DashStyle ds = DashStyle.Solid;
            switch (up)
            {
                case "SOLID":
                    ds = DashStyle.Solid;
                    break;
                case "DOT":
                    ds = DashStyle.Dot;
                    break;
                case "DASH":
                    ds = DashStyle.Dash;
                    break;
                case "DASHDOT":
                    ds = DashStyle.DashDot;
                    break;
                case "DASHDOTDOT":
                    ds = DashStyle.DashDotDot;
                    break;
            }
            return ds;
        }

        FontStyle getFontStyle(string text)
        {
            FontStyle style = FontStyle.Regular;
            switch (text)
            {
                case "REGULAR":
                    style = FontStyle.Regular;
                    break;
                case "ITALIC":
                    style = FontStyle.Italic;
                    break;
                case "BOLD":
                    style = FontStyle.Bold;
                    break;
                case "STIKEOUT":
                    style = FontStyle.Strikeout;
                    break;
                case "UNDERLINE":
                    style = FontStyle.Underline;
                    break;
                default:
                    style = FontStyle.Regular;
                    break;
            }
            return style;
        }

        string fullPath()
        {
            return path + @"\" + filename + ".ini";
        }

        void readData()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(fullPath()))
            {
                File.Create(fullPath());
            }
            else
            {
                data = File.ReadAllLines(fullPath());
            }
        }

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

        public DashStyle getDashStyleForKey(string _section, string _key)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
            DashStyle value = DashStyle.Solid;


            string[] sec = getValuesBySection(section);
            List<string> output = new List<string>();
            for (int i = 0; i < sec.Length; i++)
            {
                string[] split = sec[i].Split('=');
                if (split[0] == key)
                {
                    value = getStyle(split[1]);
                }
            }
            return value;
        }

        public void setValueForKey(string _section, string _key, string _value)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
            string value = _value.ToUpper();
            int[] range = getSectionRange(_section);

            string[] sec = getValuesBySection(section);

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
                            }
                        }
                    }
                }
            }

            File.WriteAllLines(fullPath(), data);
        }

        public void setFontForKey(string _section, string _key, Font _value)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
            int[] range = getSectionRange(_section);

            string[] sec = getValuesBySection(section);

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
                                string val = string.Format("[NAME={0};SIZE={1};STYLE={2}]", _value.Name, _value.Size.ToString().Split(',')[0], _value.Style.ToString().ToUpper());
                                data[j] = key + "=" + val;
                            }
                        }
                    }
                }
            }

            File.WriteAllLines(fullPath(), data);
        }
        
        public void setColorForKey(string _section, string _key, Color _value)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
            int[] range = getSectionRange(_section);

            string[] sec = getValuesBySection(section);

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
                                string val = _value.A + ";" + _value.R + ";" + _value.G + ";" + _value.B;
                                data[j] = key + "=" + val;
                            }
                        }
                    }
                }
            }

            File.WriteAllLines(fullPath(), data);
        }
        
        public void addFontForSection(string _section, string _key, Font _value)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
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
                        string val = string.Format("[NAME={0};SIZE={1};STYLE={2}]", _value.Name, _value.Size.ToString().Split(',')[0], _value.Style.ToString().ToUpper());
                        dataList.Insert(j, key + "=" + val);
                    }
                }
                data = dataList.ToArray<string>();
            }
            File.WriteAllLines(fullPath(), data);
            readData();
        }

        public void addColorForSection(string _section, string _key, Color _value)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
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
                        string val = _value.A + ";" + _value.R + ";" + _value.G + ";" + _value.B;
                        dataList.Insert(j, key + "=" + val);
                    }
                }
                data = dataList.ToArray<string>();
            }
            File.WriteAllLines(fullPath(), data);
            readData();
        }

        private int[] getSectionRange(string _section)
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

        public Font getFontByKey(string _section, string _key)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
            Font value = new Font("Calibri Light", 12, FontStyle.Regular, GraphicsUnit.Pixel);


            string[] sec = getValuesBySection(section);
            List<string> output = new List<string>();
            for (int i = 0; i < sec.Length; i++)
            {
                string[] split = sec[i].Split('=');
                if (split[0] == key)
                {
                    string rem = sec[i].Substring(sec[i].IndexOf('=')).Replace("[", "").Replace("]", "");
                    string[] fontArgs = rem.Split(';');

                    string name = "Modern No. 20";
                    int size = 12;

                    List<FontStyle> styles = new List<FontStyle>();

                    foreach (var str in fontArgs)
                    {
                        string str2 = str;
                        if (str.IndexOf('=') == 0) str2 = str.Substring(1);
                        string[] pair = str2.Split('=');
                        if (pair[0] == "NAME") name = pair[1];
                        else if (pair[0] == "SIZE")
                        {
                            if (pair[1].Contains(","))
                            {
                                size = int.Parse(pair[1].Split(',')[0]);
                            }
                            else
                            {
                                size = int.Parse(pair[1]);
                            }
                        }
                        else if (pair[0] == "STYLE")
                        {
                            if (pair[1].Contains(","))
                            {
                                string[] style = pair[1].Replace(" ", "").Split(',');
                                styles.Add(getFontStyle(style[0]));
                                styles.Add(getFontStyle(style[1]));
                            }
                            else
                            {
                                styles.Add(getFontStyle(pair[1]));
                            }
                        }
                    }
                    if (styles.Count == 1) value = new Font(name, size, styles[0], GraphicsUnit.Pixel);
                    else if (styles.Count > 1) value = new Font(name, size, styles[0] | styles[1], GraphicsUnit.Pixel);
                }
            }
            return value;
        }

        public Color getColorByKey(string _section, string _key)
        {
            string key = _key.ToUpper();
            string section = _section.ToUpper();
            Color value = Color.White;


            string[] sec = getValuesBySection(section);
            List<string> output = new List<string>();
            for (int i = 0; i < sec.Length; i++)
            {
                string[] split = sec[i].Split('=');
                if (split[0] == key)
                {
                    string[] rgb = split[1].Split(';');
                    try
                    {
                        value = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]), int.Parse(rgb[3]));
                    }
                    catch (Exception)
                    {
                        value = Color.White;
                    }
                }
            }
            return value;
        }
    }
}
