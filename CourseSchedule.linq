<Query Kind="Program" />

void Main()
{
	 var prerequisit = new[]{
            new [] {1, 0},
            new [] {2, 0},
            new [] {3, 1},
            new [] {3, 2}
        };
	 var nunberofcourses=4;
	 var graph= GetAdjacentGraph(prerequisit);
	 var visited=GetVisitedGraph(prerequisit);
	 
	 PrintAdjacentGraph(graph);
	 //visited.ToList().ForEach(x=>x.Dump());
	 
	 var list=getCourseShedule(graph,visited);
	 list.ForEach(x=>{x.Dump();});	
}



List<int> getCourseShedule(Dictionary<int,List<int>> graph,Dictionary<int,bool> visited){
 	Stack<int> Stack=new Stack<int>();
	DFS(0,Stack,graph,visited);
	var list=new List<int>(Stack);
 	
	return list;
 }
 void DFS(int Current,Stack<int> Stack,Dictionary<int,List<int>> graph,Dictionary<int,bool> Visited){
 			if(Visited[Current])
				return;
			//Current.Dump();
			
			Visited[Current]=true;
			
			if(graph.ContainsKey(Current)){
			
			("DFS for Note"+Current+"").Dump();
			graph[Current].ToList().Dump();
			var Edges=graph[Current].ToList();
			Edges.ForEach(y=>{
				DFS(y,Stack,graph,Visited);
			});	
			}
					
			Stack.Push(Current);
		
 }
 
 Dictionary<int,bool> GetVisitedGraph(int[][] listofCourses){
 		var dictionary=new Dictionary<int,bool>();	
		for(int i=0;i<listofCourses.Length;i++){
			 if(!dictionary.ContainsKey(listofCourses[i][1])){
			 	 dictionary[listofCourses[i][1]]=false;
			 }
			 if(!dictionary.ContainsKey(listofCourses[i][0])){
			 	 dictionary[listofCourses[i][0]]=false;
			 }
		}
		return dictionary;
 }
 Dictionary<int,List<int>> GetAdjacentGraph(int[][] listofCourses){
 		var dictionary=new Dictionary<int,List<int>>();	
 		 if(listofCourses.Length<1){return null;}
		 for(int i=0;i<listofCourses.Length;i++){
		     var course=listofCourses[i];
			 var source=course[1];
			 var destination=course[0];
			 if(dictionary.ContainsKey(source)){
			 	dictionary[source].Add(destination);
			 }else{
			 	dictionary[source]=new List<int>(){destination};
			 }			 
		 }
 
  	return dictionary;
  }
  
  void PrintAdjacentGraph(Dictionary<int,List<int>> graphs){
   graphs.ToList().ForEach(kvp=>{
	 	var Source=kvp.Key.ToString();
		var Edges=string.Join(",",kvp.Value);
		$"{Source}-->{Edges}".Dump();
		 
	 });
  }