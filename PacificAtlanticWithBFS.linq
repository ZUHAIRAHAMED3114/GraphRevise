<Query Kind="Program" />


//There is an m x n rectangular island that borders both the Pacific Ocean and Atlantic Ocean. 
//The Pacific Ocean touches the island's left and top edges, and the Atlantic Ocean touches the island's right and bottom edges.
//
//The island is partitioned into a grid of square cells. You are given an m x n integer matrix heights
//where heights[r][c] represents the height above sea level of the cell at coordinate (r, c).

// You can define other methods, fields, classes and namespaces here

//The island receives a lot of rain, and the rain water can flow to neighboring cells directly north, south, east, and west 
//if the neighboring cell's height is less than or equal to the current cell's height.
//Water can flow from any cell adjacent to an ocean into the ocean.

/*

Input: heights = [[1,2,2,3,5],[3,2,3,4,4],[2,4,5,3,1],[6,7,1,4,5],[5,1,1,2,4]]
Output: [[0,4],[1,3],[1,4],[2,2],[3,0],[3,1],[4,0]]
Explanation: The following cells can flow to the Pacific and Atlantic oceans, as shown below:
[0,4]: [0,4] -> Pacific Ocean 
       [0,4] -> Atlantic Ocean
[1,3]: [1,3] -> [0,3] -> Pacific Ocean 
       [1,3] -> [1,4] -> Atlantic Ocean
[1,4]: [1,4] -> [1,3] -> [0,3] -> Pacific Ocean 
       [1,4] -> Atlantic Ocean
[2,2]: [2,2] -> [1,2] -> [0,2] -> Pacific Ocean 
       [2,2] -> [2,3] -> [2,4] -> Atlantic Ocean
[3,0]: [3,0] -> Pacific Ocean 
       [3,0] -> [4,0] -> Atlantic Ocean
[3,1]: [3,1] -> [3,0] -> Pacific Ocean 
       [3,1] -> [4,1] -> Atlantic Ocean
[4,0]: [4,0] -> Pacific Ocean 
       [4,0] -> Atlantic Ocean
Note that there are other possible paths for these cells to flow to the Pacific and Atlantic oceans.

*/
/*
	 PENDING TO RUN WHOLE ALGORITHAM WITH DFS
*/
 		int[] xLocation = new []{0, 0, 1, -1 };
        int[] yLocation = new[] { 1, -1, 0, 0 };
        void Main()
        {
            var Grid = new[] {
                new[] { 1, 2, 2, 3, 5 },
                new[] { 3, 2, 3, 4, 4 },
                new[] { 2, 4, 5, 3, 1 },
                new[] { 6, 7, 1, 4, 5 },
                new[] { 5, 1, 1, 2, 4 }

            };
		
            var AtlanticLocation= GetAtlanticLocationBFS(Grid);
			//AtlanticLocation.Dump();
    		var PacificLocation=GetPacificLocationBFS(Grid);
			//PacificLocation.Dump();
        	AtlanticLocation.IntersectWith(PacificLocation);
			AtlanticLocation.Dump();
		}


  
        HashSet<(int, int)> GetAtlanticLocationBFS(int[][] Grid) {
            var visitedArray = getVisisted(Grid);
            var hashSet = new HashSet<(int,int)>();
            var queu = new Queue<(int, int)>();
            for (int i=(Grid.Length-1);i>=0;i--) {
                for (int j = (Grid[0].Length - 1); j >= 0; j--) {
                    if ((j == (Grid[0].Length - 1)) || (i == (Grid.Length - 1))) {
                        visitedArray[i][j] = true;
                        queu.Enqueue((i, j));
                        hashSet.Add((i, j));
                    }
                }
            }
			
			while (queu.Count > 0) {
                var currentLocation = queu.Dequeue(); 
                
                for(int i=0;i<xLocation.Length;i++){
                    var newX = currentLocation.Item1 + xLocation[i];
                    var newY = currentLocation.Item2 + yLocation[i];
                    if (IsValid(newX, newY, Grid) &&
                        CanFlow(currentLocation.Item1, currentLocation.Item2, newX, newY, Grid) &&
                         !visitedArray[newX][newY]) {
                        visitedArray[newX][newY] = true;
                        queu.Enqueue((newX, newY));
                        hashSet.Add((newX, newY));
                    }
                }
                
            }

            return hashSet;
        }

        bool IsValid(int currX, int currY,int[][] Grid) {
            if ((currY >= 0 && currY < Grid[0].Length) && 
                (currX >=0 && currX<Grid.Length)) {
                return true;
            }
            return false;
        }
        
        bool CanFlow(int currX,int currY,int nextX,int nextY,int[][] Grid) {
            if (Grid[currX][currY] <= Grid[nextX][nextY]) return true;
            return false;
        }


        HashSet<(int,int)> GetPacificLocationBFS(int[][] Grid) {
        var visitedArray = getVisisted(Grid);
            var hashSet = new HashSet<(int, int)>();
            var queu = new Queue<(int, int)>();
            for (int i =0; i < Grid.Length; i++)
            {
                for (int j = 0; j<Grid[0].Length; j++)
                {
                    if ((j == 0) || (i == 0))
                    {
                        visitedArray[i][j] = true;
                        queu.Enqueue((i, j));
                        hashSet.Add((i, j));
                    }
                }
            }
			
            while (queu.Count > 0)
            {
                var currentLocation = queu.Dequeue();

                for (int i = 0; i < xLocation.Length; i++)
                {
                    var newX = currentLocation.Item1 + xLocation[i];
                    var newY = currentLocation.Item2 + yLocation[i];
                    if (IsValid(newX, newY, Grid) &&
                        CanFlow(currentLocation.Item1, currentLocation.Item2, newX, newY, Grid) &&
                         !visitedArray[newX][newY])
                    {
                        visitedArray[newX][newY] = true;
                        queu.Enqueue((newX, newY));
                        hashSet.Add((newX, newY));
                    }
                }

            }

            return hashSet;
        }

        bool[][] getVisisted(int[][] Grid) {
            var numberofRows = Grid.Length;
            var numberofColumns = Grid[0].Length;
            var visited = new bool[numberofRows][];
            for (int i=0;i<visited.Length;i++) {
                visited[i] = new bool[numberofColumns];
            }
            return visited;
        }