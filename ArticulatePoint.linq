<Query Kind="Program" />


//Critical Connections in a Network
//There are n servers numbered from 0 to n - 1 connected by undirected server-to-server connections forming a network where connections[i] = [ai, bi] represents a connection between servers ai and bi.
//Any server can reach other servers directly or indirectly through the network.
//
//A critical connection is a connection that, if removed, will make some servers unable to reach some other server.
//
//
//Input: n = 4, connections = [[0,1],[1,2],[2,0],[1,3]]
//Output: [[1,3]]
//Explanation: [[3,1]] is also accepted

        int dfs = 0;
        void Main() {
//		
//		1.addEdge(1, 0); 
//        g1.addEdge(0, 2); 
//        g1.addEdge(2, 1); 
//        g1.addEdge(0, 3); 
//        g1.addEdge(3, 4); 
		
            var connection = new[]{
                    new[]{1,0 },
                    new[]{0,2 },
                    new[]{2,1 },
                    new[]{0,3 },
					new[]{3,4 }
            };
            var AdjacentMatrix=getAdjacentMatrix(connection, false);
            List<(int, int)> criticalConenction = new List<(int, int)>();
            List<int> articulatePoints = new List<int>();
            getArticulatePoint(AdjacentMatrix,articulatePoints,criticalConenction);
			AdjacentMatrix.Dump();
			articulatePoints.Dump();
			criticalConenction.Dump();
        }

    

        void getArticulatePoint(Dictionary<int, List<int>> adjacentMatrix, List<int> articulatePoints, List<(int, int)> criticalConenction)
        {
            var numberofNodes = GetNodes(adjacentMatrix);
            var visited = new bool[numberofNodes.Count + 1];
            var dist = new int[numberofNodes.Count + 1];
            var low = new int[numberofNodes.Count + 1];

            numberofNodes.ForEach(x =>
            {
                if (!visited[x]) {
                    SccTarjanDFS(x, visited, dist, low, adjacentMatrix, articulatePoints, criticalConenction);
                }

            });
            return;
        }

        void SccTarjanDFS(int source,bool[] visited,int[] dist,int[] low,Dictionary<int,List<int>> adjacentMatrix,
                                            List<int> articulatePoints,List<(int,int)> criticalConnection) {
            if (visited[source]) return;
            dfs++;
            dist[source] = dfs;
            low[source] = dfs;
            visited[source] = true;
            
            if (adjacentMatrix.ContainsKey(source)) {
                var remainingEdge = adjacentMatrix[source];
                remainingEdge.ForEach(x =>
                {
                    if (!visited[x])
                    {
                        SccTarjanDFS(x, visited, dist, low, adjacentMatrix,articulatePoints,criticalConnection);
                        low[source] = Math.Min(low[source], low[x]);
                     

					}
                    else {
                        low[source] = Math.Min(low[source], dist[x]); 
                    }
					  
					  
					  if (low[x] >= dist[source]) {
                        articulatePoints.Add(source);
                        criticalConnection.Add((source,x));
                     }
                  
                    
                });    
                
                
            }
        }

        public Dictionary<int, List<int>> getAdjacentMatrix(int[][] graph,bool directed) {
            int numberofRows = graph.Length;
            int numberofColums = graph[0].Length;
            var dictionary = new Dictionary<int, List<int>>();

            for (int i=0;i<numberofRows;i++) {
                var Edge = graph[i];
                var source = Edge[0];
                var destination = Edge[1];
                if (!dictionary.ContainsKey(source)) {
                    dictionary[source] = new List<int>();
                }
                dictionary[source].Add(destination);

                if (!directed) {
                    if (!dictionary.ContainsKey(destination))
                    {
                        dictionary[destination] = new List<int>();
                    }
                    dictionary[destination].Add(source);
                }
            }

            return dictionary;                
        }

        public List<int> GetNodes(Dictionary<int, List<int>> AdjacentMatrix) {
            HashSet<int> set = new HashSet<int>();
            var keys = AdjacentMatrix.Keys;
            keys.ToList().ForEach(x =>
            {
                set.Add(x);
                AdjacentMatrix[x].ForEach(y => { set.Add(y); });
                 
            });
            return set.ToList();
        }
        