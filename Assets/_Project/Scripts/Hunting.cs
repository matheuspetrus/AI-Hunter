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
        position = new Vector3Int((int)transform.position.x,(int)transform.position.y,(int)transform.position.z);
        dijkstra.InitialPosition = position;
    }

    [ContextMenu("StartTurn")]
    public void StartTurn()
    {
       
        dijkstra.InitialPosition = position;
       

        
        if (path != null)
        {
            Debug.Log($"path[{path.Count - 1}]");
                    
            if (path.Count - 1== -1)
            {
                Debug.Log("==");
                path.Clear();
                path = null;
                dijkstra.isComplete = false;
                //state = States.Spin;
            }
            else
            {
                position = path[0].Position;
                transform.position = position;
                path.RemoveAt(0);
                        
                Debug.Log("!=");
            }
        }
        else
        {
            if (dijkstra.isComplete)
            {
                dijkstra.TriggerPrintPath();
                        
                path= new List<TileLogic>();
                path = dijkstra.GetPath();
                        
                        
                position = path[0].Position;
                transform.position = position;
                path.RemoveAt(0);
                        
                Debug.Log(path);
            }
            else
            {
                dijkstra.TriggerSearch();
            }
        }
    }
}
