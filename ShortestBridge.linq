<Query Kind="Program" />

//You are given an n x n binary matrix grid where 1 represents land and 0 represents water.
//An island is a 4-directionally connected group of 1's not connected to any other 1's. There are exactly two islands in grid.
//You may change 0's to 1's to connect the two islands to form one island.
//Return the smallest number of 0's you must flip to connect the two islands.
//// You can define other methods, fields, classes and namespaces here



    void Main() {

            //var grid = new[]{
            //    new []{ 0,1,0},
            //    new []{ 0,0,0},
            //    new []{ 0,0,1}
            //};

            var grid2 = new[] {
                new[]{ 1,1,1,1,1 },
                new[]{ 1,0,0,0,1},
                new[]{ 1,0,1,0,1},
                new[]{1,0,0,0,1 },
                new[]{ 1, 1, 1, 1, 1 }

            };

            var x= getShortesBridge(grid2);
        }

        int getShortesBridge(int[][] Grid) {
          
            var firstIslandLocation = GetFirstIslandLocation(Grid);
            var bfsVistedArray = getVistedArray(Grid);
            firstIslandLocation.ForEach(v =>
            {
                bfsVistedArray[v.Item1][v.Item2] = true;
            });

            var queue = new Queue<(int, int)>(firstIslandLocation);
            int currentLevel = 0;
            while (queue.Count > 0) {
                
                int currentLevelLength = queue.Count;
                while (currentLevelLength > 0) {
                    var pop = queue.Dequeue();
                    for (int i=0;i<xDirection.Length;i++) {
                        var nextRow = pop.Item1 + xDirection[i];
                        var nextCol = pop.Item2 + yDirection[i];
                        if (!isInValid(nextRow, nextCol, bfsVistedArray)) {
                            if (Grid[nextRow][nextCol] == 1) {
                                return currentLevel;
                            }
                            bfsVistedArray[nextRow][nextCol]= true;
                            queue.Enqueue((nextRow,nextCol));
                        }
                       
                    }
                    currentLevelLength--;
                }
                currentLevel++;
            }

            return currentLevel;
        }
        int[] xDirection = new[] {0,0,1,-1 };
        int[] yDirection = new[] { 1, -1, 0, 0 };
        bool[][] getVistedArray(int[][] grid) {
            var result = new bool[grid.Length][];
            for (int i=0;i<grid.Length;i++) {
                result[i] = new bool[grid[i].Length];
            }
            return result;
        }

        List<(int, int)> GetFirstIslandLocation(int[][] grid) {
            var visitedArray = getVistedArray(grid);
            var isLandLocation = new List<(int,int)>();
            for (int row=0;row<grid.Length;row++) {
                for (int col=0;col<grid[row].Length;col++) {
                    if (grid[row][col] == 1 && isLandLocation.Count()==0) {
                        dfs(row,col,grid,visitedArray,isLandLocation);
                    }
                }
            }
            return isLandLocation;
        }
        bool isInValid(int row, int col, bool[][] visited) {
            if (row < 0 || col < 0 || row == visited.Length || col == visited[0].Length || visited[row][col]) 
                return true;
            return false;
        }

        void dfs(int row,int col,int[][] grid,bool[][] visited,List<(int,int)> isLandLocation) {
            // assuming the Current Row is Island...

            visited[row][col] = true;
            isLandLocation.Add((row,col));

            for (int i = 0; i < xDirection.Length; i++) {
                var nextRow = row + xDirection[i];
                var nextCol = col + yDirection[i];

                if (!isInValid(nextRow, nextCol, visited) && grid[nextRow][nextCol] == 1) {
                    dfs(nextRow, nextCol, grid, visited, isLandLocation);
                }
            }
        }

