<Query Kind="Program" />



// You can define other methods, fields, classes and namespaces here
//
//Given an m x n 2D binary grid grid which represents a map of '1's (land) and '0's (water), return the number of islands.
//
//An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically. You may assume all four edges of the grid are all surrounded by water.
//
//Example 1:
//
//Input: grid = [
//  ["1","1","1","1","0"],
//  ["1","1","0","1","0"],
//  ["1","1","0","0","0"],
//  ["0","0","0","0","0"]
//]
//Output: 1
//Example 2:
//
//Input: grid = [
//  ["1","1","0","0","0"],
//  ["1","1","0","0","0"],
//  ["0","0","1","0","0"],
//  ["0","0","0","1","1"]
//]
//Output: 3
 
int[] xPosition=new int[]{0,0,1,-1};
int[] yPosition=new int[]{1,-1,0,0};
void Main()
{
	var grid=new []{ new []{1,1,0,0,0},
				  	 new []{1,1,0,0,0},
			 		 new []{0,0,1,0,0},
			 		 new []{0,0,0,1,1} 
				   };
	var visited=getVisitedArray(grid);		
	var numberofIsland=0;
	for(int i=0;i<grid.Length;i++){
		for(int j=0;j<grid[i].Length;j++){
			if(grid[i][j]==1 && !visited[i][j])	
			{
				numberofIsland++;
				DFS(i,j,visited,grid);
			}
		}
	}
	numberofIsland.Dump();
}

 bool[][] getVisitedArray(int[][] graph){
    var numberofRows=graph.Length;
	var numberofColumns=graph[0].Length;
	var visited= new bool[numberofRows][];
	for(int i=0;i<numberofRows;i++){
		visited[i]=new bool[numberofColumns];
	}
 	return visited;
 }

bool isValid(int hor,int ver,bool[][] visited){
			if((hor>=0 && hor<visited.Length)   
		    	&& (ver>=0 && ver<visited[hor].Length)	
				&&(!visited[hor][ver])){
				return true;
			}
		return false;
}
void DFS(int hor,int ver,bool[][] visited,int[][] grid){
     if(!isValid(hor,ver,visited) || grid[hor][ver]!=1){
	 	return ;
	 }
		
	visited[hor][ver]=true;	
	for(int i=0;i<xPosition.Length;i++){
		var newHor=hor+xPosition[i];
		var newVer=ver+yPosition[i];
		DFS(newHor,newVer,visited,grid);
	}
	
 }