<Query Kind="Program" />

//You are given an n x n integer matrix board where the cells are labeled from 1 to n2 in a Boustrophedon style starting from the bottom left of the board (i.e. board[n - 1][0]) and alternating direction each row.
//
//You start on square 1 of the board. In each move, starting from square curr, do the following:
//
//Choose a destination square next with a label in the range [curr + 1, min(curr + 6, n2)].
//This choice simulates the result of a standard 6-sided die roll: i.e., there are always at most 6 destinations, regardless of the size of the board.
//If next has a snake or ladder, you must move to the destination of that snake or ladder. Otherwise, you move to next.
//The game ends when you reach the square n2.
//A board square on row r and column c has a snake or ladder if board[r][c] != -1. The destination of that snake or ladder is board[r][c]. Squares 1 and n2 do not have a snake or ladder.
//
//Note that you only take a snake or ladder at most once per move. If the destination to a snake or ladder is the start of another snake or ladder, you do not follow the subsequent snake or ladder.
//
//For example, suppose the board is [[-1,4],[-1,3]], and on the first move, your destination square is 2. You follow the ladder to square 3, but do not follow the subsequent ladder to 4.
//Return the least number of moves required to reach the square n2. If it is not possible to reach the square, return -1.

 public void Main() {
            //[],[],[],[],[],[]

            var grid = new[] {
                new[]{ -1, -1, -1, -1, -1, -1 },
                new[]{ -1,-1,-1,-1,-1,-1 },
                new[]{ -1,-1,-1,-1,-1,-1},
                new[]{ -1,35,-1,-1,13,-1 },
                new[]{ -1, -1, -1, -1, -1, -1 },
                new[]{ -1, 15, -1, -1, -1, -1 }
            };

            //if (NumberofDepth(0, 0, grid, out int r))
            //{
            //    Console.WriteLine($"Current Depth{r}");
            //}

            //var g = GetPositionInGrid(grid, 17);

            //var f = GetPositionInGrid(grid, 15);
            //var k = GetPositionInGrid(grid,16);
            var n = GetPositionInGrid(grid, 35);
            var x = GetPositionInGrid(grid, 1);
        }

        bool isValid(int value, int[][] grid) {
            if (value < 0 &&
              value > ((grid.Length - 1) * (grid[0].Length - 1))
             )
            {
             
                return false;
            }
            return true;
        }

        bool NumberofDepth(int value, int currentDepth, int[][] grid, out int result) {
         
            if (value == ((grid.Length ) * (grid[0].Length ))) {
                 result = currentDepth;
                 return true;
            }

            List<int> finalReulst = new List<int>();
            for (int i = 1; i <= 6; i++) {
                var currentValue = value + i;
                if (isValid(currentValue, grid)) {
                    var (row, col) = GetPositionInGrid(grid, currentValue);
                    if (grid[row][col] != -1)
                    {
                        currentValue = grid[row][col];
                    }
                    if (NumberofDepth(currentValue, (currentDepth + 1), grid, out int tempResult))
                    {
                        finalReulst.Add(tempResult);
                    }
                }
            }
            if (finalReulst.Count > 0) {
                finalReulst.Sort((a,b)=>a.CompareTo(b));
                result = finalReulst.First();
                return true;
            }

            result = -1;
            return false;
        }
       
        public (int, int) GetPositionInGrid(int[][] Grid, int currntValue)
        {
            var numberofrows = Grid.Length;
            var numberofcolumns = Grid[0].Length;

            // var currentRow = (currntValue ) / numberofcolumns;
            double currentRow = (double)(currntValue) / numberofcolumns;
            double currentCol = (double)(currntValue ) / numberofrows;

            //int CR = currentRow;
            var temr = ((currentRow - Math.Floor(currentRow)) * numberofrows);
            var temc = ((currentCol - Math.Floor(currentCol)) * numberofcolumns);
          //  int CC = (int)((currentCol - Math.Floor(currentCol)) * numberofcolumns);
            int CC = (int)(Math.Round(temc, MidpointRounding.AwayFromZero));
            int CR= (int)(Math.Round(temr, MidpointRounding.AwayFromZero));
            if (currentRow % 2 == 0)
            {
                return (numberofrows- CR, CC-1);
            }
            else
            {
                return (numberofrows-1 - CR, numberofcolumns - CC);
            }
        }
// You can define other methods, fields, classes and namespaces here