using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic
{
    public Vector3Int Position;
    public Color Color;
    public float CostFromOrigin;
    public float CostToObjective;
    public float Score;
    public bool Occupied;
    public int MoveCost;
    public TileLogic Previous;
}
