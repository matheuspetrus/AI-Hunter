using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class StraightLine : MonoBehaviour
{
    public Vector3Int InitialPosition;

    public Vector3Int TargetPosition;

  void Search()
    {
        int xMovement = TargetPosition.x > InitialPosition.x ? 1 : -1;
        int yMovement = TargetPosition.y > InitialPosition.y ? 1 : -1;

        Vector3Int currentPosition = InitialPosition;
        while (currentPosition.x != TargetPosition.x) {
            currentPosition.x += xMovement;
            Board.Instance.PaintTile(currentPosition,Color.red);
        
        }
        while (currentPosition.y != TargetPosition.y)
        {
            currentPosition.y += xMovement;
            Board.Instance.PaintTile(currentPosition, Color.red);

        }
    }
}
