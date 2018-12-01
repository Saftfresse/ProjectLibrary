using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClassLib
{
    public class TextfileParser
    {
        string[] data;
        string filename = @"NewTextFile";
        string path = @"";

        public TextfileParser()
        {
            readData();
        }

        public TextfileParser(string _path, string _filename)
        {
            path = _path;
            filename = _filename;
            readData();
        }

        string fullPath()
        {
            return path + @"\" + filename + ".txt";
        }

        public void readData()
        {
            if (!Directory.Exists(Path.GetFullPath(fullPath())))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(fullPath()))
            {
                //File.Create(fullPath());
            }
            else
            {
                data = Tools.ReadLines(fullPath()).ToArray();
            }
        }

        public async void saveData()
        {
            try
            {
                File.WriteAllLines(fullPath(), data);
            }
            catch (Exception)
            {
                bool success = false;
                int count = 0;
                while (!success && count < 30)
                {
                    try
                    {
                        File.WriteAllLines(fullPath(), data, Encoding.UTF8);
                        Console.WriteLine("Succeeded to write");
                        success = true;
                    }
                    catch (Exception)
                    {
                        await Task.Delay(500);
                        count++;
                        Console.WriteLine("Failed to write! Try " + count);
                    }
                }
            }
        }

        public string[] getLines()
        {
            return data;
        }

        public string getLine(int index)
        {
            return data[index];
        }

        public int getIndexOf(string line)
        {
            int ind = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == line)
                {
                    ind = i;
                }
            }
            return ind;
        }

        public void addLine(string newLine)
        {
            List<string> lines = data.ToList();
            lines.Add(newLine);
            data = lines.ToArray();
        }

        public void addLine(string newLine, int index)
        {
            List<string> lines = data.ToList();
            lines.Insert(index, newLine);
            data = lines.ToArray();
        }

        public void Clear()
        {
            data = new string[0];
        }
    }
}
