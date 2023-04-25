using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ChangeTilemapAlpha : MonoBehaviour
{
    public Tilemap tilemap;
    public Slider slider;
    void Start()
    {
       
    }

    private void Update()
    {
        TilemapRenderer tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
        Material tilemapMaterial = tilemapRenderer.material;
        Color tilemapColor = tilemapMaterial.color;

        // Define um novo valor de alfa (transparência) para o material do Tilemap
        tilemapColor.a = slider.value;
        tilemapMaterial.color = tilemapColor;
    }
}
