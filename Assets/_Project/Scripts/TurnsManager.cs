using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    public List<GameObject> huntings;
    public GameObject hunter;

    public bool isAutoTurn;

    public TMP_Text turnsCountText;
    private int turnsCount;

    void Start()
    {
        InvokeRepeating("AutoTurn", 3.0f, 3.0f);
    }
    
    public void NextTurn()
    {
        for (int i = 0; i < huntings.Count; i++)
        {
            if ( huntings[i]!=null || !huntings[i].Equals(null))
            {
                StartCoroutine(Turn(huntings[i]));
               
            }
        }
        
        StartCoroutine(Turn(hunter));
       
        turnsCount++;
        turnsCountText.text = turnsCount.ToString();
    }

    IEnumerator Turn(GameObject obj)
    {
        if (obj.GetComponent<Hunting>())
        {
            obj.GetComponent<Hunting>().StartTurn();
            yield return new WaitForSeconds(1f);
        }
        if (obj.GetComponent<Hunter>())
        {
            obj.GetComponent<Hunter>().StartTurn();
            yield return new WaitForSeconds(1f);
        }
    }

    void AutoTurn()
    {
        if (isAutoTurn)
        {
            NextTurn();
        }
    }

    public void SetAutoTurn()
    {
        isAutoTurn = !isAutoTurn;
    }
}
