using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public Vector3Int position;
    public GameObject Target;
    public AStar aStar;
    public Dijkstra dijkstra;
   public List<TileLogic> path;
    enum States { Search,Following, Attacking, Spin,  };

    [SerializeField] States state;
    // Start is called before the first frame update
    void Start()
    {
        state = States.Search;
        position =new Vector3Int((int)transform.position.x,(int)transform.position.y,(int)transform.position.z);
    }
    
    [ContextMenu("StartTurn")]
    public void StartTurn()
    {
        aStar.InitialPosition = position;
        dijkstra.InitialPosition = position;
        //
        // aStar.ObjectivePosition = new Vector3Int((int) Target.transform.position.x, 
        //     (int) Target.transform.position.y, (int) Target.transform.position.z);
        
      
        
        switch (state)
        {
            case States.Spin:

                
                aStar.ObjectivePosition = new Vector3Int((int) Target.transform.position.x, 
                    (int) Target.transform.position.y, (int) Target.transform.position.z);
                
                Debug.Log("States.Spin: "+ aStar.ObjectivePosition);
             
                state = States.Following;

                break;
            case States.Attacking:
                print ("Hello and good day!");
                break;
            case States.Following:
                

                if (path != null)
                {
                    Debug.Log($"path[{path.Count - 1}]");
                    
                    if (path.Count - 1== 0)
                    {
                        Debug.Log("==");
                        path.Clear();
                        path = null;
                        aStar.isComplete = false;
                        state = States.Spin;
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
                    if (aStar.isComplete)
                    {
                        aStar.TriggerPrintPath();
                        
                        path= new List<TileLogic>();
                        path = aStar.GetPath();
                        
                        Debug.Log(path);
                    }
                    else
                    {
                        aStar.TriggerSearch();
                    }
                  
                 
                }

              
                break;
                
            case States.Search:

                if (path != null)
                {
                    Debug.Log($"path[{path.Count - 1}]");
                    
                    if (path.Count - 1== 0)
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
                        
                        Debug.Log(path);
                    }
                    else
                    {
                        dijkstra.TriggerSearch();
                    }
                }

                break;
            default:
                print ("Incorrect intelligence level.");
                break;
        }
        
    }
}
