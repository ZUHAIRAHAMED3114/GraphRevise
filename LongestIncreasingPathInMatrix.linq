<Query Kind="Program" />

//Given an m x n integers matrix, return the length of the longest increasing path in matrix.
//
//From each cell, you can either move in four directions: left, right, up, or down.
//You may not move diagonally or move outside the boundary (i.e., wrap-around is not allowed).
//
//Input: matrix = [[9,9,4],[6,6,8],[2,1,1]]
//Output: 4
//Explanation: The longest increasing path is [1, 2, 6, 9].

//Note :-> Please Solve This Problem in Future with Memoization
		
        int[] rows=new []{0,0,1,-1 };
        int[] cols = new[] { 1, -1, 0, 0 };
        void Main() 
		{
            var Matrix = new[] {
                    new []{ 1, 2, 3, 4 },
                    new []{ 2, 2, 3, 4 },
                    new []{ 3, 2, 3, 4 },
                    new []{ 4, 5, 6, 7 }
                };

            //var Matrix2 = new[] { new[] { 9, 9, 4 }, new[] { 6, 6, 8 }, new[] { 2, 1, 1 } };
			
			var longestPath=getLongestPath(Matrix);
			Matrix.Dump();
			longestPath.Dump();
        }
		
		
		


        int getLongestPath(int[][] Matrix) {
            var longestPath = 0;
            int numberofRows = Matrix.Length;
            int numberofColumns = Matrix[0].Length;
            var visitedArray = getVisitedArray(Matrix);
            for (int row=0;row<numberofRows;row++) {
                for (int col=0;col<numberofColumns;col++) {
                    longestPath = Math.Max(longestPath, dfs(row, col, Matrix, visitedArray));
                }
            }
            return longestPath;
        }
        bool[][] getVisitedArray(int[][] Matrix) {
            int numberofRows = Matrix.Length;
            int numberofColumns = Matrix[0].Length;

            var vistedArray = new bool[numberofRows][];
            for (int i = 0; i<vistedArray.Length; i++) {
                vistedArray[i] = new bool[numberofColumns];
            }
            return vistedArray;
        }

        int dfs(int row, int col, int[][] Matrix, bool[][] Visited) {
            var currrentValue = Matrix[row][col];
     
            var depth = 1;
            Visited[row][col] = true;
            for (int i=0;i<rows.Length;i++) {
                var nextRow = row + rows[i];
                var nextCol = col + cols[i];
                
                if (isValid(nextRow, nextCol, Visited) && Matrix[nextRow][nextCol]>Matrix[row][col]) {
                    depth = Math.Max(depth, 1 + dfs(nextRow, nextCol,Matrix,Visited));
                } 
            }
            Visited[row][col] = false;
            return depth;
        }

        bool isValid(int nextRow, int nextCol, bool[][] visited)
        {
            var numberofRows = visited.Length;
            var numberofCols = visited[0].Length;

            if (nextRow >= 0 && nextRow < numberofRows && nextCol >= 0 && nextCol < numberofCols && !visited[nextRow][nextCol]) return true;

            return false;
        }