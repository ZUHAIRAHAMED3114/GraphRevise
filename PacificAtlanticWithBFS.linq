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

void Main()
{
	
	var Grid=new[]{
		 new[]{1,2,2,3,5},
	     new[]{3,2,3,4/*Here Error Is Identify...*/,4},
	     new[]{2,4,5,3,1},
  	     new[]{6,7,1,4,5},
	     new[]{5,1,1,2,4}
	};
 try{
 	
	//Grid.Dump();
	//visited.Dump();
	
	GetAtlanticVistedPosition(Grid);
 }catch(Exception ex){
   ex.Message.Dump();
 }

}
int[] xposition={0,0,1,-1};
int[] yposition={1,-1,0,0};

HashSet<(int,int)> GetAtlanticVistedPosition(int[][] grid){
   
	var set = new HashSet<(int,int)>();
	var numberOfRows=grid.Length;
	var numberOfColumns=grid[0].Length;
	for(int i=0;i<numberOfRows;i++)
		set.Add((i,0));
    for(int i=0;i<numberOfColumns;i++)
		set.Add((0,i));
	
	//set.Dump();
	for(int row=1;row<numberOfRows;row++){
	  for(int col=1;col<numberOfColumns;col++){
	     
			for(int k=0;k<xposition.Length;k++){
			    
			    if(!set.Contains((row,col))){
				
				var newRow=row+xposition[k];
				var newCol=col+yposition[k];
				if((reachEnd(newRow,newCol,grid)== false )&&canAtlanticReach(row,col,newRow,newCol,set,grid)==true){
					"Adding".Dump();
					set.Add((row,col));
				}
			 }
			}
	  }
	}
	
	return set;
}

bool[][] getVisitedArray(int[][] grid){
	var numberOfRows=grid.Length;
	var numberOfColumns=grid[0].Length;
	
	var visited=new bool[numberOfRows][];
    for(int i=0;i<visited.Length;i++){
	  visited[i]=new bool[numberOfColumns];
	}
	return visited;
}


bool reachEnd(int currHorx,
			 int currVert,int[][] grid){

	var numberOfRows=grid.Length;
	var numberOfColumns=grid[0].Length;		 
    var result=false; 
	if(currHorx>=numberOfRows ||
	   currHorx<=0 ||
	   currVert>=numberOfColumns ||
	   currVert<=0) {
	    result=true;
	   };
	   
    //result.Dump();
    return result;
}

bool canAtlanticReach(int row,int col,int nextRow,int nextCol,HashSet<(int,int)> set,int[][] grid){
	 if(set.Contains((nextRow,nextCol))) return true;
	 if(canFlow(row,nextRow,col,nextCol,grid)){
	 
	   for(int k=0;k<xposition.Length;k++){
				var newRow=row+xposition[k];
				var newCol=col+yposition[k];
				if((reachEnd(newRow,newCol,grid)== false )&& canAtlanticReach(row,col,newRow,newCol,set,grid)){
					return true;
				}
			}
	 }
		
	return false;	
}

bool canFlow(int currHorx,int nextHorx,
			 int currVert,int nextVert,
			 int[][] grid){

	var numberOfRows=grid.Length;
	var numberOfColumns=grid[0].Length;
	
	
	if(nextHorx>=numberOfRows ||
	   nextHorx<=0 ||
	   nextVert>=numberOfColumns ||
	   nextVert<=0) return false;
	   grid[currHorx][currVert].Dump();
	if(grid[currHorx][currVert]>=grid[nextHorx][nextVert]) return true;
	
	return false;
}

