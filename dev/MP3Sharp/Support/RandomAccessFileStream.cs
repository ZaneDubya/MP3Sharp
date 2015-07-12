using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MP3Sharp.Support
{
    internal class RandomAccessFileStream
    {
        public static FileStream CreateRandomAccessFile(string fileName, string mode)
        {
            FileStream newFile = null;

            if (mode.CompareTo("rw") == 0)
                newFile = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate,
                    System.IO.FileAccess.ReadWrite);
            else if (mode.CompareTo("r") == 0)
                newFile = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            else
                throw new ArgumentException();

            return newFile;
        }
    }
}
