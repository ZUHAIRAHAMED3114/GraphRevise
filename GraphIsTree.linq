<Query Kind="Program" />

    void Main()
        {
            //  1) Cycle Should Not Be There
            //  2) It Should Be Simple Component...
           var Edges1 = new[] {
                    new []{ 1,0},
                    new []{ 0,2},
                    new []{ 0,3},
                    new []{ 3,4}
            };
		
	
			var Edges2=new[]{
				new[]{0,1},
				new[]{1,2},
				new[]{2,3},
				new[]{1,3},
				new[]{1,4},
			};

			//[[0,1], [1,2], [2,3], [1,3], [1,4]]

    	  var result2= IsGraphTree(Edges2);
		  $"Final Result--{result2}".Dump();
	
		  var result1=IsGraphTree(Edges1);
		  	  $"Final Result--{result1}".Dump();
	   }

      bool IsGraphTree(int[][] Edges) {
            var adjacentMatrix = getAdjacentMatrix(Edges);
            var visitedMatrix = getVistedMatrix(Edges);
            var V= getNumberofNodes(Edges);
			
		//	adjacentMatrix.Dump();
		
			
			var E=Edges.Length;	
			if((V==E+1)&& !isCyclicDFS(0,-1,visitedMatrix,adjacentMatrix))
			 return true;
		
            return false;
        }

        private int getNumberofNodes(int[][] edges)
        {
            HashSet<int> set = new HashSet<int>();

            for (int j = 0; j < edges.Length; j++)
            {
                var edge = edges[j];
                var source = edge[0];
                var destiation = edge[1];
                set.Add(source);
                set.Add(destiation);
            }
			//set.Dump();
            return set.Count;
        }

	
        private bool[] getVistedMatrix(int[][] listOfEdges)
        {
            var numberofNodes = getNumberofNodes(listOfEdges);
            return new bool[numberofNodes];
        }

        private Dictionary<int, List<int>> getAdjacentMatrix(int[][] listOfEdges)
        {
            var dictionary = new Dictionary<int,List<int>>();
            var numberOfEdges = listOfEdges.Length;

            for (int j=0;j<numberOfEdges;j++) {
                var edge=listOfEdges[j];
                var source = edge[0];
                var destiation = edge[1];

                if (!dictionary.ContainsKey(source)) {
                    dictionary[source] = new List<int>();
                }
            
                dictionary[source].Add(destiation);
            }

            return dictionary;
        }

        bool isCyclicDFS(int source, int parent, bool[] NodesVisitd, Dictionary<int, List<int>> graph) {
                
            NodesVisitd[source] = true;
			var isCycle = false;
			if(!graph.ContainsKey(source)){
				return isCycle;
			}
			
			
            var childNodes = graph[source];
            
            for (int i=0;i<childNodes.Count;i++) {
				
                if (NodesVisitd[childNodes[i]] && childNodes[i] != parent) {
                    return true;
                }
                if (!isCycle) {
                    isCycle = isCyclicDFS(childNodes[i], source, NodesVisitd, graph);
                }

            }
            
            
            return isCycle;
        }