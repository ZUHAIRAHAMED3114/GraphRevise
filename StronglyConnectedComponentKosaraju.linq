<Query Kind="Program" />

        int dfs = 0;
        void Main() {
            var Edges = new[]{
                new []{0,1},
                new []{0,3},
                new []{1,2},
                new []{1,4},
                new []{2,0},
             	new []{2,6},
				new []{3,2},
				new []{4,5},
				new []{4,6},
				new []{5,6},
				new []{5,7},
				new []{5,8},
				new []{5,9},
				new []{6,4},
				new []{7,9},
				new []{8,9},
				new []{9,8},
	
			 };
           
		var stronglyConnectedComponent = GetStronglyConnectedComponent(Edges);
		stronglyConnectedComponent.Dump();
        }

        List<List<int>> GetStronglyConnectedComponent(int[][] edges)
        {
            var Nodes = GetNodes(edges);
            var visited = GetVisitedArray(edges);
            var dist = new int[100]; // This One Also If We Take Dictionalry will be better
            var low = new int[100];  // Take Dictionary
            var stack = new Stack<int>();
            var result = new List<List<int>>();
            var adjacentMatrix = GetAdjacencyList(edges,true);
            var finalList = new List<List<int>>();
            for (int i = 0; i < Nodes.Count; i++) {
                var source = Nodes[i];
                StrongConnectComp(source,adjacentMatrix,visited,dist,low,stack,finalList);
            }

            return finalList;
        }
        void StrongConnectComp(int source,Dictionary<int,List<int>> adjacentMatrix ,Dictionary<int,bool> visited,
                                int[] dist,int[] low,Stack<int> stack,List<List<int>> ConnectedCompo) {
            dfs++;
            if (visited.ContainsKey(source) && visited[source]) return;

            visited[source] = true;
            stack.Push(source);
            dist[source] = dfs;
            low[source] = dfs;

            if (!adjacentMatrix.ContainsKey(source)) {
                return;
            }

            var Neighbours = adjacentMatrix[source];
            for (int i=0;i<Neighbours.Count;i++) {
                var nextSource = Neighbours[i];
                if (!visited[nextSource])
                {

                    StrongConnectComp(nextSource, adjacentMatrix, visited, dist, low, stack, ConnectedCompo);
                    low[source] = Math.Min(low[source], low[nextSource]);
                }
                else {
                    if (stack.Contains(nextSource)) {
                        low[source] = Math.Min(low[source], dist[nextSource]);
                    }
                }
            }

            if (dist[source] == low[source]) {
                var listofComponet = new List<int>();
                while (stack.Count > 0 && stack.Peek() != source)
                {
                    listofComponet.Add(stack.Pop());
                }
                // popping last Element
                listofComponet.Add(stack.Pop());
				ConnectedCompo.Add(listofComponet);
            }

        }

        Dictionary<int, List<int>> GetAdjacencyList(int[][] graph,bool DirectedGraph) {
            var dictionary = new Dictionary<int, List<int>>();
            var numberofRows = graph.Length;
            var numberofColumns = graph[0].Length;

            for (int i=0;i<numberofRows;i++) {
                var CurrentEdge = graph[i];
                var source = CurrentEdge[0];
                var destination = CurrentEdge[1];
                if (!dictionary.ContainsKey(source)) {
                    dictionary[source] = new List<int>();
                }
                dictionary[source].Add(destination);
                if (!DirectedGraph) {
                    if (!dictionary.ContainsKey(destination)) {
                        dictionary[destination] = new List<int>();
                    }
                    dictionary[destination].Add(source);
                }

            }

            return dictionary;    
        }
        Dictionary<int,bool> GetVisitedArray(int[][] graph) {
            var dictionary = new Dictionary<int, bool>();
            GetNodes(graph).ForEach(z =>
            {
                if (!dictionary.ContainsKey(z))
                {
                    dictionary.Add(z, false);
                }
            });
            return dictionary;
        }
        List<int> GetNodes(int[][] graph) {
            HashSet<int> Set = new HashSet<int>();
            var numberofRows = graph.Length;
            var numberofColumns = graph[0].Length;
            for (int i = 0; i < numberofRows; i++) {
                var Edge = graph[i];
                Set.Add(Edge[0]);
                Set.Add(Edge[1]);
            }

            return Set.ToList();
        }