<Query Kind="Program" />

	/*
	A transformation sequence from word beginWord to word endWord using a dictionary wordList is a sequence of words beginWord -> s1 -> s2 -> ... -> sk such that:
	
	Every adjacent pair of words differs by a single letter.
	Every si for 1 <= i <= k is in wordList. Note that beginWord does not need to be in wordList.
	sk == endWord
	Given two words, beginWord and endWord, and a dictionary wordList, return the number of words in the 
	shortest transformation sequence from beginWord to endWord, or 0 if no such sequence exists.
	
	Example 1:
	==========
	Input: beginWord = "hit", endWord = "cog", wordList = ["hot","dot","dog","lot","log","cog"]
	Output: 5
	Explanation: One shortest transformation sequence is "hit" -> "hot" -> "dot" -> "dog" -> cog", which is 5 words long.
	
	Example 2:
	==========
	Input: beginWord = "hit", endWord = "cog", wordList = ["hot","dot","dog","lot","log"]
	Output: 0
	Explanation: The endWord "cog" is not in wordList, therefore there is no valid transformation sequence.
	
	*/
	
	//Creating Adjacent Matrix For Above Situation...?
	
	//solution
	//--------
	/*
	
	STEP-1
	======
	Creating Adjacent Matrix List For the Given List of Words

	STEP-2
	======
	Finally Creating a breadth First Search
	
	
	
	
	*/
	
	
void Main()
{
	var wordList=new[]{"hot","dot","dog","lot","log","cog"};
	var beginWord="hit";
	var endWord="cog";
	var adjacentMatrix=getPatternWord(wordList);
	//BFS(beginWord,endWord,adjacentMatrix);
    var stack=new Stack<string>();
	var listofString=new List<string>();
	DFS(beginWord,beginWord,endWord,stack,adjacentMatrix,listofString);
}

void DFS(string givenWord,string beginWord,string endWord,Stack<string> stack,Dictionary<string,List<string>> adjacentMatrix,List<string> visited){
			
			
			if(givenWord.Length!=endWord.Length) return;
			if(givenWord==endWord) {
				 var x=stack.ToArray().Reverse();
				 var list=new List<String>(x);
				 list.Add(givenWord);
				 "Final ROUTE".Dump();
				 list.Dump();
			}
     
	 	if(visited.Contains(givenWord)){
			return;
		}
	    
		visited.Add(givenWord); //ADDING
		stack.Push(givenWord);
		
		var patterns=getPattern(givenWord);
		
		var listofAdjacentWords=new List<string>();
			patterns.ToList()
				.ForEach(y=>{
					if(adjacentMatrix.ContainsKey(y)){
					   
					   
						adjacentMatrix[y].ToList().ForEach(z=>{
								if(!listofAdjacentWords.Contains(z) && z!=givenWord ) {
									listofAdjacentWords.Add(z);
								}
							
						});
						
					   
					}
	
				});
		 listofAdjacentWords.ForEach(z=>{
		 if(!stack.Contains(z)){
		 	 	DFS(z,beginWord,endWord,stack,adjacentMatrix,visited);
		 }
	
		});
		visited.Remove(givenWord); //REMOVING
		stack.Pop();
	
			
			
}

void BFS(string beginWord,string endWord,Dictionary<string,List<string>> adjacentMatrix){

	var queue=new Queue<string>();
	var visited=new List<string>();
	queue.Enqueue(beginWord);
	
	while(queue.Count>0){
		var firstWord=queue.Peek();
		 var y= queue.Dequeue();
		 visited.Add(firstWord);
		 firstWord.Dump();
		("Popoing From the Queue"+firstWord).Dump();
		if(firstWord==endWord){
			 return;
		}
		
		var patterns=getPattern(firstWord);
		var listofAdjacentWords=new List<string>();
		patterns.ToList()
				.ForEach(y=>{
					if(adjacentMatrix.ContainsKey(y)){
						adjacentMatrix[y].ToList().ForEach(z=>{
								if(!queue.Contains(z)&& !visited.Contains(z) ){
									queue.Enqueue(z);
								}
							
						});
					}
	
				});
	
		"List Of Words Available in the Queue Is..".Dump();
	}

}

Dictionary<string,List<string>> getPatternWord(string[] Words){
	  Dictionary<string,List<string>> dictionary=new Dictionary<string,List<string>>();
 
		Words.ToList()
			 .ForEach(x=>{
			   for(int i=0;i<x.Length;i++){
			     char[] characters=x.ToCharArray();
		 	     characters[i]='*';
				 var pattern=new string(characters);
				  if(dictionary.ContainsKey(pattern)){
				 	 dictionary[pattern].Add(x);
				  }	else{
				  	dictionary[pattern]=new List<string>(){x};
				  }
				}
			
			 
		});
		
		return dictionary;
}

List<string> getPattern(string word){
	var List=new List<string>();
	for(int i=0;i<word.Length;i++){
		var chars=word.ToCharArray();
		chars[i]='*';
		List.Add(new String(chars));
	}
	return List;
}

	//adjacentMatrix.ToList()
	//			  .ForEach(kvp=>{
	//			    var print=$"{kvp.Key}===>{string.Join(',',kvp.Value)}";
	//				print.Dump();
	//			  });
	//			  