using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : Pathfinder
{
    public int heuristicWeight = 2;

    protected override IEnumerator Search(TileLogic start)
    {
        int interationCout = 0;
        tilesSearch =new List<TileLogic>();
        Board.Instance.ClearSearch();
        TileLogic objective = Board.GetTile(ObjectivePosition);
        TileLogic current;
        
        List<TileLogic> openSet = new List<TileLogic>();
        openSet.Add(start);
        start.CostFromOrigin = 0;

        while (openSet.Count>0)
        {
            openSet.Sort((x,y)=> x.Score.CompareTo(y.Score));
            current = openSet[0];
            Board.Instance.PaintTile(current.Position,Color.green);

            if (current== objective)
            {
                Debug.Log("Found the objective");
                break;
            }
            openSet.RemoveAt(0);
            tilesSearch.Add(current);
            for (int i = 0; i < Board.Directions.Length; i++)
            {
                TileLogic next = Board.GetTile(current.Position + Board.Directions[i]);
                yield return  new WaitForSeconds(0.05f);
                interationCout++;
                
                if (next == null || next.CostFromOrigin<=current.CostFromOrigin+next.MoveCost)
                {
                    continue;
                }

                next.CostFromOrigin = current.CostFromOrigin + next.MoveCost;
               
                if (current.CostFromOrigin+next.MoveCost<SearchLength)
                {
                    next.CostFromOrigin = current.CostFromOrigin + next.MoveCost;
                    next.Previous = current;
                    next.CostToObjective = Vector3Int.Distance(next.Position, objective.Position) *heuristicWeight;
                    next.Score = next.CostToObjective + next.CostFromOrigin;

                    if (!tilesSearch.Contains(next))
                    {
                        openSet.Add(next);
                    }
                    
                    Board.Instance.PaintTile(next.Position,Color.yellow);
                }
                
            }
        }
    }
}
