using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnHuntings : MonoBehaviour
{
    public Transform[] spawns;

    public GameObject prefabHunting;

    public int countSpawns;

    public TMP_InputField inputField;

    public TurnsManager turnsManager;



    public void PlaySpawn()
    {
        
        countSpawns = int.Parse(inputField.text);
        
        for (int i = 0; i < countSpawns; i++)
        {
           GameObject obj = Instantiate(prefabHunting, spawns[Random.Range(0, spawns.Length)].position,Quaternion.identity);
           
           turnsManager.huntings.Add(obj);
           
        }
        
    }
}
