using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunting : MonoBehaviour
{
    public Vector3Int position;
    public Dijkstra dijkstra;
    public List<TileLogic> path;
    // Start is called before the first frame update
    void Start()
    {
        position =new Vector3Int((int)transform.position.x,(int)transform.position.y,(int)transform.position.z);
    }

    public void SetInitialPosition()
    {
        // position = new Vector3Int((int)transform.position.x,(int)transform.position.y,(int)transform.position.z);
        // dijkstra.InitialPosition = position;
    }

    [ContextMenu("StartTurn")]
    public void StartTurn()
    {
        Debug.Log("StartTurn Hunting");
        dijkstra.InitialPosition = position;
        if (path != null)
        {
                    
            Debug.Log(path.Count);
                    
            if (path.Count - 1== -1)
            {
                Debug.Log("==");
                path.Clear();
                path = null;
                dijkstra.isComplete = false;
                        
                dijkstra.TriggerSearch();
            }
            else
            {
                Move();
            }
        }
        else
        {
            if (dijkstra.isComplete)
            {
                dijkstra.TriggerPrintPath();
                        
                path= new List<TileLogic>();
                path = dijkstra.GetPath();
                        
                Move();
                        
                // Debug.Log(path);
            }
            else
            {
                dijkstra.TriggerSearch();
            }
        }
        
    }
    void Move()
    {
        Debug.Log("Move");
        if (path!=null && path.Count>=1)
        {
            position = path[0].Position;
            transform.position = position;
            path.RemoveAt(0); 
        }
    }
}
