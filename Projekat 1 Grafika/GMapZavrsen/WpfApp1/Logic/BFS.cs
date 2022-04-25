using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.Logic
{
    internal class BFS
    {
        static bool isValid(int row, int col)
        {
            // return true if row number and
            // column number is in range
            return (row >= 0) && (row < 250) &&
                (col >= 0) && (col < 250);
        }

        // These arrays are used to get row and column
        // numbers of 4 neighbours of a given cell
        static int[] rowNum = { -1, 0, 0, 1 };
        static int[] colNum = { 0, -1, 1, 0 };

        static int PathCountId { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="N"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns>Path of Qitems for line</returns>
        public static List<QItem> minDistance(ref MjestoMatrica[,] grid, int N, int x1, int y1, int x2, int y2)
        {

            Queue<QItem> queueNodes = new Queue<QItem>();
            QItem root = new QItem()
            {
                x = x1,
                y = y1,
                Parent = new QItem()
                {
                    Valid = false
                }
            };
            queueNodes.Enqueue(root);
            
            bool[,] visited = new bool[N, N];
            visited[x1, y1] = true;

            #region CreateBFSTre
            List<QItem> bfsTree = new List<QItem>();
            bfsTree.Add(root);

            bool routePossible = false;

            while (queueNodes.Count != 0)
            {
                QItem current = queueNodes.Peek();

                if (current.x == x2 && current.y == y2)
                {
                    // reached finish
                    routePossible = true;
                    break;
                }

                queueNodes.Dequeue();

                current.Valid = true;

                for (int i = 0; i < 4; i++)
                {
                    QItem currentChild = new QItem()
                    {
                        x = current.x + rowNum[i],
                        y = current.y + colNum[i],
                        Parent = current
                    };

                    if (
                        isValid(currentChild.x, currentChild.y) && 
                        (grid[currentChild.x, currentChild.y].Polje != Mjesto.Zauzeto ||
                        currentChild.x == x2 && currentChild.y == y2) &&
                        !visited[currentChild.x, currentChild.y])
                    {
                        visited[currentChild.x, currentChild.y] = true;

                        queueNodes.Enqueue(currentChild);
                        bfsTree.Add(currentChild);
                    }
                }
            }
            #endregion

            List<QItem> retVal = new List<QItem>();

            if (routePossible)
            {
                QItem curr = bfsTree.Find(el => el.x == x2 && el.y == y2);

                if (curr.Parent == null)
                {
                    throw new Exception("Second end not in bfs tree");
                }

                while (true)
                {
                    if (grid[curr.x, curr.y].Polje != Mjesto.Vod)
                    {
                        grid[curr.x, curr.y].Polje = Mjesto.Vod;
                        grid[curr.x, curr.y].PathId = PathCountId;
                    }

                    retVal.Add(curr);
                    
                    if (!((QItem)curr).Parent.Valid)
                        break;

                    curr = (QItem)curr.Parent;
                }
            }

            PathCountId++;
            return retVal;
        }
    }
}
