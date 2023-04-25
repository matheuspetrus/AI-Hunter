using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttack : MonoBehaviour
{
    public Hunter hunter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hunting"))
        {
           hunter.StartAttack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hunting"))
        {
            hunter.StopAttack();
        }
    }
}
