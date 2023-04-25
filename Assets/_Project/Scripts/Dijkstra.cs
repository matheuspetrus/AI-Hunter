using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : Pathfinder
{
    protected override IEnumerator Search(TileLogic start)
    {
        int interationCount = 0;
        
        Board.Instance.ClearSearch();
       tilesSearch = new List<TileLogic>();
       
       tilesSearch.Add(start);
       
       Queue<TileLogic> CheckNow = new Queue<TileLogic>();
       Queue<TileLogic> CheckNext = new Queue<TileLogic>();

       start.CostFromOrigin = 0;
       
       CheckNow.Enqueue(start);
       while (CheckNow.Count>0)
       {
           TileLogic current = CheckNow.Dequeue();
            Board.Instance.PaintTile(current.Position,Color.green);
           for (int i = 0; i < Board.Directions.Length; i++)
           {
               TileLogic next = Board.GetTile(current.Position + Board.Directions[i]);
               yield return  new WaitForSeconds(0.05f);
               interationCount++;
               if (next == null || next.CostFromOrigin<=current.CostFromOrigin+next.MoveCost)
               {
                   continue;
               }

               if (current.CostFromOrigin+next.MoveCost<SearchLength)
               {
                   next.CostFromOrigin = current.CostFromOrigin + next.MoveCost;
                   next.Previous = current;

                   if (!tilesSearch.Contains(next))
                   {
                       CheckNext.Enqueue(next);
                   }
                  
                   Board.Instance.PaintTile(next.Position,Color.yellow);
               }
           }

           if (CheckNow.Count==0)
           {
               SwapReference(ref CheckNow, ref CheckNext);
           }
       }
       Debug.Log("interationCount: "+interationCount);
     
    }

    private void SwapReference(ref Queue<TileLogic> checkNow, ref Queue<TileLogic> checkNext)
    {
        Queue<TileLogic> temp = checkNow;
        checkNow = checkNext;
        checkNext = temp;
    }


}
