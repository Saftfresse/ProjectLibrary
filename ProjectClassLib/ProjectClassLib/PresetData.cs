using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectClassLib
{
    public class PresetData
    {
        // Variablen //
        
        public string[] data;
        string filename = @"Presets";
        string path = @"";


        // Funktionen //

        public PresetData()
        {
            readData();
        }

        public PresetData(string _path, string _filename)
        {
            path = _path;
            filename = _filename;
            readData();
        }

        string fullPath()
        {
            return path + @"\" + filename + ".ini";
        }

        public void refresh()
        {
            readData();
        }

        public void readData()
        {
            if (!File.Exists(fullPath()))
            {
                File.Create(fullPath());
            }
            else
            {
                data = File.ReadAllLines(fullPath());
            }
        }

        public void saveData()
        {
            File.WriteAllLines(fullPath(), data);
        }

        public void addCategory(string category)
        {
            List<string> list = data.ToList();
            list.Add("[" + category.ToUpper() + "]");
            data = list.ToArray();
            saveData();
            readData();
        }

        public void removeString(string toBeRemoved, string category)
        {
            List<string> ret = new List<string>();
            bool match = false;
            Regex cat = new Regex("\\[.{1,20}\\]");
            foreach (var item in data)
            {
                if (cat.IsMatch(item))
                {
                    string net = item.Replace("[", "").Replace("]", "");
                    if (net.ToUpper() == category.ToUpper())
                    {
                        ret.Add(item);
                        match = true;
                        continue;
                    }
                    else
                    {
                        ret.Add(item);
                        match = false;
                        continue;
                    }
                }
                else if (match)
                {
                    if (item != toBeRemoved) ret.Add(item);
                }else if (!match)
                {
                    ret.Add(item);
                }
            }
            data = ret.ToArray();
            saveData();
        }

        public void addString(string category, string toBeAdded)
        {
            List<string> list = data.ToList();
            list.Insert(list.IndexOf("[" + category.ToUpper() + "]") + 1, toBeAdded);
            data = list.ToArray();
            saveData();
        }
        
        public string[] getStrings(string category)
        {
            List<string> ret = new List<string>();
            bool write = false;
            Regex cat = new Regex("\\[.{1,20}\\]");
            foreach (var item in data)
            {
                if (cat.IsMatch(item))
                {
                    string net = item.Replace("[", "").Replace("]", "");
                    if (net.ToUpper() == category.ToUpper())
                    {
                        write = true;
                    }
                    else
                    {
                        write = false;
                    }
                }else if (write)
                {
                    ret.Add(item);
                }
            }
            return ret.ToArray();
        }
    }
}
