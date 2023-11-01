<Query Kind="Program" />

void Main() {

        var grid =new[] {
                  new []{2147483647,-1,0,2147483647 },
                  new []{2147483647,2147483647,2147483647,-1},
                  new []{2147483647,-1,2147483647,-1},
                  new []{0,-1,2147483647,2147483647 }
            };

            var finalResult = getResultArray(grid);
            var visitedResult = getVisitedArray(grid);

            PrintWallAndGatesBFS(grid,visitedResult,finalResult);
        }

        void PrintWallAndGatesBFS(int[][] grid,bool[][] visited,int[][] result) {

            // var minHeap = new Heap<(int, int, int)>((a,b)=>a.Item1.CompareTo(b.Item1));

            var queue = new Queue<(int, int)>();
        
            for (int i = 0; i < grid.Length; i++) {
                for (int j = 0; j < grid[0].Length; j++) {
                    if (grid[i][j] == 0) {
					 visited[i][j] = true;
                        queue.Enqueue((i,j));
                    }
                }
            }

            int depht = 0;
            var direction = new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) };
            while (queue.Count > 0) {
                var currentQueueLength = queue.Count;
				
//				"------------------".Dump();
//				"items in Queue".Dump();
//				queue.Dump();
//
//				"finalResult".Dump();
//				result.Dump();
						
				while (currentQueueLength > 0) {

					var (row, col) = queue.Dequeue();
                    result[row][col]= depht;
                    currentQueueLength--;
                   		"depth".Dump();

							 $"Row--{row} and Col--{col} and depth--{depht}".Dump();
                

                    direction.ForEach(k =>
                    {
                       var newRow = row + k.Item1;
                        var newCol = col + k.Item2;
                        if (isValid(newRow, newCol, grid, visited))
                        {
                            visited[newRow][newCol] = true;
                            queue.Enqueue((newRow, newCol));
                        }
                    });
                
                }
                depht++;

            }



        }

        bool isValid(int newRow, int newCol, int[][] grid, bool[][] visited)
        {
            if (newRow < 0 || newCol < 0 || newRow >= grid.Length || 
                newCol >= grid.Length || visited[newRow][newCol] || 
                grid[newRow][newCol] != 2147483647) return false;
            return true;
        }

        int[][] getResultArray(int[][] grid)
        {

            var numberofRows = grid.Length;
            var numberofCols = grid[0].Length;
            var result = new int[numberofRows][];
            for (int i = 0; i < numberofRows; i++) {
                result[i] = new int[numberofCols];
            }
            return result;

        }

        bool[][] getVisitedArray(int[][] grid)
        {
            var numberofRows = grid.Length;
            var numberofCols = grid[0].Length;
            var returnr = new bool[numberofRows][];
            for (int i=0;i<numberofRows;i++) {
                returnr[i] = new bool[numberofCols];
            }
            return returnr;
        }