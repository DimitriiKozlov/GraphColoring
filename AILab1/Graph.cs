using System;

namespace AILab1
{
    class Graph
    {
        private readonly bool[,] _matrix;
        private readonly int _nVertex;
        private int _nEdge;
        //public int[] VertexColor;


        public Graph(int nV, int nE)
        {
            _nEdge = nE;
            _nVertex = nV;
            _matrix = new bool[_nVertex, _nVertex];
            //VertexColor = new int[_nVertex];
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
