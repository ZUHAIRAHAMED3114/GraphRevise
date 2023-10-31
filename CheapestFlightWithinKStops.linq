<Query Kind="Program" />

    public class Heap<T>
    {
        List<T> list;
        public Func<T,T, int> Comparer;
        public Heap(Func<T,T,int> Comparer)
        {
            list = new List<T>();
            this.Comparer = Comparer;
        }
        public void Push(T item) {
            list.Add(item);
            var currentIdex = list.Count() - 1;

            while (currentIdex > 0) {
                var parentIndex = (currentIdex - 1) / 2;
                if (Comparer(list[currentIdex], list[parentIndex]) >= 0) {
                    break;
                }
                swap(currentIdex, parentIndex);
                currentIdex = parentIndex;
            }
        }

        private void swap(int currentIdex, int parentIndex)
        {
            var parentValue = list[parentIndex];
            list[parentIndex] = list[currentIdex];
            list[currentIdex] = parentValue;
        }

        public T Pop() {
            if (Count() <= 0)
            throw new Exception();
            
            var min= list[0];
            list[0] = list[Count() - 1];
            list.RemoveAt(Count() - 1);
            int index = 0;
            while (true)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int smallest = index;

                if (leftChild < Count() && Comparer(list[leftChild], list[smallest]) < 0)
                {
                    smallest = leftChild;
                }
                if (rightChild < Count() && Comparer(list[leftChild], list[smallest]) < 0)
                {
                    smallest = rightChild;
                }

                if (smallest == index)
                {
                    break;
                }

                swap(index, smallest);
                index = smallest;
            }
            return min;
        }
        public int Count() {
            return list.Count();
        }


        

    }

   //After Creating Heap 
   // Now We are solving Problem
   
   
   
   
   
   
   
     public void Main() {
//            var input = new[] {
//                new []{0,1,100 },  //Sorce ,Destination,Price
//                new []{1,2,100 },
//                new []{ 0,2,500}
//                };
//            var Stops = 1;
//            var result = FindCheapestPrice(input,Stops);
//            Console.WriteLine(result);

        
            var input = new[] {
                new []{0,1,100 },  //Sorce ,Destination,Price
                new []{1,2,100 },
                new []{2,0,100},
				new []{1,3,600},
                new []{2,3,200}
                };
            var Stops = 1;
			var source=0;
			var destination=3;
            var result = FindCheapestPrice(input,Stops,source,destination);
            Console.WriteLine(result);

		}

        private int FindCheapestPrice(int[][] graph,int stops,int src,int dest) {
            var dictionary = new Dictionary<int, List<(int, int)>>();
            for (int i = 0; i < graph.Length; i++)
            {
                var value = graph[i];
                var source = value[0];
                var destin = value[1];
                var price = value[2];
                if (!dictionary.ContainsKey(source))
                {
                    dictionary[source] = new List<(int, int)>();
                }

                dictionary[source].Add((destin, price));
            }
			 //dictionary.Dump();
            //stops+1 why we are taking like this is because after passing Src Node we Considered it as OneStop we 
            return CheapestPrice(src, dest, stops+1, dictionary);
         
        }

        private int CheapestPrice(int source,
                                  int dest,
                                  int Stops,
                                  Dictionary<int,List<(int,int)>> WtGraph) {

            /* city,price,stop */
            var Minheap=new Heap<(int,int,int)>((a,b)=>a.Item2.CompareTo(b.Item2));
            // Intial Values We Are Adding --> City,Price,HowManyStops    
            Minheap.Push((0, 0, Stops));
			"Minimum Heap".Dump();
	
            while (Minheap.Count() > 0) {
					
                var (city, price, stop) = Minheap.Pop();
                if (city == dest)
                    return price;

                if (stop > 0) {
                    if (WtGraph.ContainsKey(city)) {
                     foreach (var (destCity, destPrice) in WtGraph[city]) {
                            Minheap.Push((destCity, price + destPrice, stop - 1));
                        }

                    }
                }
                
            }
            
            return -1;
        }