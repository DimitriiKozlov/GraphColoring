using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AILab1
{
    class Graph
    {
        private const double M = 1;
        private const double Pc = 1;

        private readonly bool[,] _matrix;
        public readonly int NVertex;
        private readonly int _nEdge;
        private readonly int _nColor;
        private readonly int[] _vertexConflict;
        public int[] VertexColor;


        public Graph(GraphDataFile gdf)
        {
            _nEdge = gdf.NEdge;
            NVertex = gdf.NVertex;
            _nColor = gdf.NColors;
            _matrix = new bool[NVertex, NVertex];

            VertexColor = new int[NVertex];
            _vertexConflict = new int[NVertex];

            var rand = new Random();
            for (var i = 0; i < VertexColor.Length; i++)
                VertexColor[i] = rand.Next(_nColor);

            SetData(gdf.EdgeSource);

            for (var i = 0; i < NVertex; i++)
                UpdateConflict(i);

        }

        private void SetData(string source)
        {
            var words = source.Split();

            for (int i = 0; i < words.Length; i++)
            {
                if ((words[i] == "e") || (words[i] == ""))
                    continue;
                var n = Convert.ToInt32(words[i]) - 1;
                var m = Convert.ToInt32(words[i + 1]) - 1;
                _matrix[n, m] = _matrix[m, n] = true;
                i++;
            }
        }

        private List<int> GetAdjacentVertex(int id)
        {
            var res = new List<int>();

            for (var i = 0; i < NVertex; i++)
                if (_matrix[id, i])
                    res.Add(i);

            return res;
        }

        private void UpdateConflict(int id)
        {
            var aList = GetAdjacentVertex(id);
            _vertexConflict[id] = 0;
            for (var i = 0; i < aList.Count; i++)
                if (VertexColor[aList[i]] == VertexColor[id])
                    _vertexConflict[id]++;
        }

        public int Ants(int nAnts)
        {
            var ants = new int[nAnts];
            var iter = 0;
            var rand = new Random();

            for (var i = 0; i < nAnts; i++)
                ants[i] = rand.Next(NVertex);

            while (_vertexConflict.Sum() > 0)
            {
                iter++;
                if (iter > 9000)
                {
                    MessageBox.Show("Over 9000");
                    return 9000;
                }
                var fail = false;
                foreach (var ant in ants)
                    if ((_vertexConflict[ant] == 0) && (iter > 100))
                    {
                        fail = true;
                        break;
                    }

           //     MessageBox.Show(iter.ToString() + ", " + _vertexConflict.Sum().ToString());
                for (var i = 0; i < ants.Length; i++)
                {
                    var t = Ant(ants[i]);
                    if (_vertexConflict[t] > 0)
                        ants[i] = t;
                    else
                    {
                        var lst = new List<int>();
                        for (int j = 0; j < _vertexConflict.Length; j++)
                            if (_vertexConflict[j] > 0)
                                lst.Add(j);
                        if (lst.Count == 0)
                            return iter;
                        ants[i] = lst[rand.Next(lst.Count)];
                    }
                }
            }

            return iter;
        }


        private int Ant(int id)
        {
            var totalConflict = 0;

            var aList = GetAdjacentVertex(id);
            var iMaxConflict = aList[0];
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

            UpdateConflict(id);
            foreach (var i in aList)
                UpdateConflict(i);
            var newId = rand.Next(totalConflict) < M * _vertexConflict[iMaxConflict] ? iMaxConflict : aList[rand.Next(aList.Count)];
            return newId;
        }

        
    }
}
