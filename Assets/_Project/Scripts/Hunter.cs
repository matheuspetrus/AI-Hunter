using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public TMP_Text statesHunter;
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

                statesHunter.text ="Current State: "+ States.Spin.ToString();
                
                if (path!=null)
                {
                    path.Clear();
                    path = null;
                }
                
             
                aStar.isComplete = false;
                
                aStar.ObjectivePosition = new Vector3Int((int) Target.transform.position.x, 
                    (int) Target.transform.position.y, (int) Target.transform.position.z);
                
                Debug.Log("States.Spin: "+ aStar.ObjectivePosition);
             
                state = States.Following;

                break;
            case States.Attacking:
                
                statesHunter.text ="Current State: "+ States.Attacking.ToString();
                Destroy(Target);
                //path.Clear();
                path = null;
                //Target = null;
                aStar.isComplete = false;
                dijkstra.isComplete = false;
                state = States.Search;
                
                break;
            case States.Following:
                
                statesHunter.text ="Current State: "+ States.Following.ToString();
                if (path != null)
                {
                    
                    Debug.Log(path.Count);
                    
                    if (path.Count - 1== 0)
                    {
                        Debug.Log("==");

                        state = States.Spin;
                    }
                    else
                    {
                        Move();
                    }
                }
                else
                {
                    if (aStar.isComplete)
                    {
                        aStar.TriggerPrintPath();
                        
                        path= new List<TileLogic>();
                        path = aStar.GetPath();
                        
                        Move();
                        
                        // Debug.Log(path);
                    }
                    else
                    {
                        aStar.TriggerSearch();
                    }
                }


              
                break;
                
            case States.Search:

                statesHunter.text = "Current State: "+States.Search.ToString();
                
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

                break;
            default:
                statesHunter.text ="Current State: "+ "Default";
                print ("Incorrect intelligence level.");
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Hunting"))
        {
            Target = other.gameObject;
            state = States.Spin;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Hunting"))
        {
            Target = null;
            state = States.Search;
        }
    }

    public void StartAttack()
    {
        state = States.Attacking;
    }
    public void StopAttack()
    {
        state = States.Search;
    }

    void Move()
    {
        Debug.Log("Move");
        if (path.Count>=1)
        {
            position = path[0].Position;
            transform.position = position;
            path.RemoveAt(0); 
        }
       
    }
}
