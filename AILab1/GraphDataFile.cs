using System;
using System.IO;

namespace AILab1
{
    class GraphDataFile
    {
        public string Name;
        public int NVertex;
        public int NEdge;
        public int NColors;
        public string Comment;
        public string EdgeSource;

        GraphDataFile(TextReader source)
        {
            var s = source.ReadLine();

            Comment = "";
            Name = s.Remove(0, s.IndexOf(':') + 2);
            NColors = Convert.ToInt32(s.Split('.')[1]);
            
            do
            {
                Comment += s.Remove(0, 1);
                s = source.ReadLine();
            } while (s[0] == 'c');

            NVertex = Convert.ToInt32(s.Split()[2]);
            NEdge = Convert.ToInt32(s.Split()[3]);
            EdgeSource = source.ReadToEnd();
        }
    }
}
