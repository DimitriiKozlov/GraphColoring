using System;

namespace AILab1
{
    class Graph
    {
        private readonly bool[,] _matrix;
        public readonly int NVertex;
        public readonly int NEdge;
        public int[] VertexColor;
        public int NColor;


        public Graph(int nV, int nE)
        {
            NEdge = nE;
            NVertex = nV;
            _matrix = new bool[NVertex, NVertex];

            VertexColor = new int[NVertex];
            NColor = NEdge - NVertex + 1;

            var rand = new Random();
            for (var i = 0; i < VertexColor.Length; i++)
                VertexColor[i] = rand.Next(NColor);
        }

        public void SetData(string source)
        {
            var words = source.Split();

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "e")
                    continue;
                var n = Convert.ToInt32(words[i]);
                var m = Convert.ToInt32(words[i + 1]);
                _matrix[n, m] = _matrix[m, n] = true;
                i++;
            }
        }

        
    }
}
