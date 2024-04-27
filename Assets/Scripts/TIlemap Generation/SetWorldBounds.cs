using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class SetWorldBounds : MonoBehaviour
{
    //[SerializeField] private Tilemap grid;
    private void Awake()
    {
        var bounds = GetComponent<TilemapRenderer>().bounds;
        Globals.WorldBounds = bounds;
    }
}
