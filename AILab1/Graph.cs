using System;
using System.Collections.Generic;
using System.Linq;

namespace AILab1
{
    class Graph
    {
        private const double M = 1;
        private const double Pc = 1;

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

        private List<int> getAdjacentVertex(int id)
        {
            var res = new List<int>();

            for (var i = 0; i < _nVertex; i++)
                if (_matrix[id, i])
                    res.Add(i);

            return res;
        }

        private void updateConflict(int id)
        {
            var aList = getAdjacentVertex(id);
            _vertexConflict[id] = 0;
            for (var i = 0; i < aList.Count; i++)
                if (VertexColor[i] == VertexColor[id])
                    _vertexConflict[id]++;
        }

        public void Ants(int nAnts)
        {
            
        }

        private int ant(int id)
        {
            var iMaxConflict = 0;
            var totalConflict = 0;

            var aList = getAdjacentVertex(id);
            var adjacentColor = new int[_nColor];

            foreach (var i in aList)
            {
                var iConf = _vertexConflict[i];

                totalConflict += iConf;
                if (_vertexConflict[iMaxConflict] < iConf)
                    iMaxConflict = i;

                adjacentColor[VertexColor[i]]++;
            }
            
            var rand = new Random();

            if (rand.Next(100) <= Pc*100)
            {
                var iMin = 0;
                for (var i = 1; i < adjacentColor.Length; i++)
                    if (adjacentColor[iMin] > adjacentColor[i])
                        iMin = i;
                VertexColor[id] = iMin;
            }
            else
                VertexColor[id] = rand.Next(_nColor);

            updateConflict(id);
            foreach (var i in aList)
                updateConflict(i);

            return rand.Next(totalConflict) <= M*iMaxConflict ? iMaxConflict : _vertexConflict[aList[rand.Next(aList.Count)]];
        }

        
    }
}
