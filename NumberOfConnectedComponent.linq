<Query Kind="Program" />

void Main()
{

	var Edges=new[]{new[]{1,0},new[]{2,1},new[]{3,4}};
	var AdjacentMatrix=getAdjacentMatrix(Edges);
 	var Nodes=   getNumberOfNodes(AdjacentMatrix);
	var numberofConnectedComponent=getNumberofConnectedComponent(AdjacentMatrix,Nodes);
	numberofConnectedComponent.Dump();
}

Dictionary<int,List<int>> getAdjacentMatrix(int[][] graph){
  var dictionary=new Dictionary<int,List<int>>();
  var numberofRows=graph.Length;
  var numberOfColumns=graph[0].Length;
  for(int i=0;i<numberofRows;i++){
  		var currentEdge=graph[i];
		var source=currentEdge[0];
		var destination=currentEdge[1];
		
		if(!dictionary.ContainsKey(source)){
			dictionary[source]=new List<int>();
		}
		if(!dictionary.ContainsKey(destination)){
			dictionary[destination]=new List<int>();
		}
		
        dictionary[source].Add(destination);
		dictionary[destination].Add(source);
  }	
 return dictionary;
}

List<int> getNumberOfNodes(Dictionary<int,List<int>> graph){
		HashSet<int> set=new HashSet<int>();
	    var keys=graph.Keys.ToList();
		keys.ForEach(z=>{ 
			set.Add(z); 
			graph[z].ForEach(y=>{
			 set.Add(y);	
			});
		
		});
		
	
		return set.ToList();
}

int getNumberofConnectedComponent(Dictionary<int,List<int>> graph,List<int> Nodes){
	var visited=new bool[Nodes.Count];	
	int numberofComponet=0;
	for(int i=0;i<Nodes.Count;i++){
		if(!visited[Nodes[i]]){
		   // "In Main Mehtod".Dump();
		   // Nodes[i].Dump();
		  	numberofComponet++;
			DFS(Nodes[i],visited,graph);
		}
	}
	return numberofComponet;
}

void DFS(int source,bool[] visited,Dictionary<int,List<int>> graph){
	 visited[source]=true;
	// "In DFS METHOD".Dump();
	// source.Dump();
	 if(!graph.ContainsKey(source))return;
	
	  graph[source]
	  	.ForEach(z=>{
			if(!visited[z])
			 DFS(z,visited,graph);
		});
}


//void PrintConnectedComponent(Dictionary<int,List<int>> graph,List<int> Nodes){
//		
//
//}
