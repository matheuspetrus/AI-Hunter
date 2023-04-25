using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    public List<GameObject> huntings;
    public Hunter hunter;

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
                huntings[i].GetComponent<Hunting>().StartTurn();
            }
        }
        
        hunter.GetComponent<Hunter>().StartTurn();
        turnsCount++;
        turnsCountText.text = turnsCount.ToString();
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
