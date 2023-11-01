<Query Kind="Program" />

//You are given an n x n integer matrix grid where each value grid[i][j] represents the elevation at that point (i, j).

//The rain starts to fall. At time t, the depth of the water everywhere is t. You can swim from a square to another 4-directionally
//adjacent square if and only if the elevation of both squares individually are at most t. You can swim infinite distances in zero time.
//Of course, you must stay within the boundaries of the grid during your swim.
  public class Heap<T>
    {
         List<T> list;
        public Func<T,T, int> Comparer;
        public Heap(Func<T,T,int> Comparer)
        {
            list = new List<T>();
            this.Comparer = Comparer;
        }
        public void Push(T item) {
            list.Add(item);
            var currentIdex = list.Count() - 1;

            while (currentIdex > 0) {
                var parentIndex = (currentIdex - 1) / 2;
                if (Comparer(list[currentIdex], list[parentIndex]) >= 0) {
                    break;
                }
                swap(currentIdex, parentIndex);
                currentIdex = parentIndex;
            }
        }

        private void swap(int currentIdex, int parentIndex)
        {
            var parentValue = list[parentIndex];
            list[parentIndex] = list[currentIdex];
            list[currentIdex] = parentValue;
        }

        public T Pop() {
            if (Count() <= 0)
            throw new Exception();
            
            var min= list[0];
            list[0] = list[Count() - 1];
            list.RemoveAt(Count() - 1);
            int index = 0;
            while (true)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int smallest = index;

                if (leftChild < Count() && Comparer(list[leftChild], list[smallest]) < 0)
                {
                    smallest = leftChild;
                }
                if (rightChild < Count() && Comparer(list[leftChild], list[smallest]) < 0)
                {
                    smallest = rightChild;
                }

                if (smallest == index)
                {
                    break;
                }

                swap(index, smallest);
                index = smallest;
            }
            return min;
        }
        public int Count() {
            return list.Count();
        }


        

    }


//Return the least time until you can reach the bottom right square (n - 1, n - 1) if you start at the top left square (0, 0)


        public void Main() {
            var grid = new[] {
                new[]{0,1,2,3,4 },
                new[]{ 24, 23, 22, 21, 5 },
                new[]{ 12, 13, 14, 15, 16 },
                new[]{11,17,18,19,20},
                new[]{ 10,9,8,7,6}
            };
			var parent=getReultArray(grid);
            var rst = getMinTime(grid, (0, 0), (4, 4),parent);
			parent.Dump();
			rst.Dump();
        }

        bool[][] getVisitedArray(int[][] gird) {
            var result = new bool[gird.Length][];
            for (int i=0;i<gird.Length;i++) {
                result[i] = new bool[gird[0].Length];
            }
            return result;
        }
        (int,int)[][] getReultArray(int[][] grid) {
            var result = new (int,int)[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                result[i] = new (int,int)[grid[0].Length];
            }
            return result;
        }

        bool isValid(int row,int col, bool[][] visited) {
            if (row < 0 || col<0 || row>=visited.Length || col>=visited[0].Length || visited[row][col]) {
                return false;
            }
            return true;
        }

        int getMinTime(int[][] grid,(int,int) source,(int,int) destination,(int,int)[][] parent) {

            var minHeap = new Heap<(int, int, int)>((a, b) => a.CompareTo(b));
            var (srcRow, srcCol) = source;
            var (dstRow, dstCol) = destination;
            var direction = new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) };
            var visited = getVisitedArray(grid);
            visited[srcRow][srcCol] = true;
            parent[srcRow][srcCol] = (srcRow,srcCol);
            minHeap.Push((grid[srcRow][srcCol], srcRow, srcCol));

            var destHeigt = grid[dstRow][dstCol];
            var MaxHeight = 0;

            while (minHeap.Count() > 0) {
                var popItem = minHeap.Pop();
                var (row, col) = (popItem.Item2, popItem.Item3);
                MaxHeight = Math.Max(MaxHeight, popItem.Item1);
                if (row == dstRow && col == dstCol) return MaxHeight;

                direction.ForEach((k) =>
                {
                    var (x, y) = k;
                    if (isValid(row + x, col + y, visited)) {

                        visited[row+x][col+y]= true;
                        parent[row + x][col + y] = (row,col);
                        minHeap.Push((grid[row+x][col+y],row+x,col+y));
                    }
                });

            }

            return MaxHeight;
        }