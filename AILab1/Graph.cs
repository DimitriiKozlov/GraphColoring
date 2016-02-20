using System;
using System.Collections.Generic;

namespace AILab1
{
    class Graph
    {
        private readonly bool[,] _matrix;
        private readonly int _nVertex;
        private readonly int _nEdge;
        private readonly int _nColor;
        private int[] _vertexConflict;
        public int[] VertexColor;


        public Graph(GraphDataFile gdf)
        {
            _nEdge = gdf.NEdge;
            _nVertex = gdf.NVertex;
            _nColor = gdf.NColors;
            _matrix = new bool[_nVertex, _nVertex];

            VertexColor = new int[_nVertex];
            _vertexConflict = new int[_nVertex];

            var rand = new Random();
            for (var i = 0; i < VertexColor.Length; i++)
                VertexColor[i] = rand.Next(_nColor);

            SetData(gdf.EdgeSource);

            for (var i = 0; i < _nVertex; i++)
                updateConflict(i);

        }

        private void SetData(string source)
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

        private List<int> get_Vertex(int id)
        {
            var res = new List<int>();

            for (var i = 0; i < _nVertex; i++)
                if (_matrix[id, i])
                    res.Add(i);

            return res;
        }

        private void updateConflict(int id)
        {
            var _ = get_Vertex(id);
            _vertexConflict[id] = 0;
            for (var i = 0; i < _.Count; i++)
                if (VertexColor[i] == VertexColor[id])
                    _vertexConflict[id]++;
        }



        
    }
}
