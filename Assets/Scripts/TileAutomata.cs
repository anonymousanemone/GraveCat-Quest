using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileAutomata : MonoBehaviour {
    //code from https://www.youtube.com/watch?v=xNqqfABXTNQ&t=1720s&ab_channel=DanielHofmann

    //map spawning using game of life algorithm parameters
    [Range(0, 100)]
    public int iniChance;
    [Range(1, 8)]
    public int birthLimit;
    [Range(1, 8)]
    public int deathLimit;

    //spawning the decorative stuff, from yours truly
    [Range(0, 100)]
    public int decorChance;
    [Range(0, 100)]
    public int obstacleChance;

    [Range(1,10)]
    public int numR;
    private int count = 0;

    private int[,] terrainMap;
    private int[,] decorMap;
    public Vector3Int tmpSize;
    public Tilemap topMap;
    public Tilemap thingMap;
    public Tile topTile;
    //public Tile botTile;
    public Tile[] obstacles;
    public Tile[] decor;

    int width;
    int height;

    //for generation of dark green patches using game of life, one click = one update
    public void doSim(int nu)
    {
        clearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;

        if (terrainMap==null)
            {
            terrainMap = new int[width, height];
            initPos();
            }


        for (int i = 0; i < nu; i++)
        {
            terrainMap = genTilePos(terrainMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y] == 1)
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                    //botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
            }
        }


    }

    //initial generation of green patches
    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }

        }

    }

    //generation of decorative things, press g to generate
    public void generateThings()
    {
        width = tmpSize.x;
        height = tmpSize.y;
        decorMap = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                decorMap[x, y] = Random.Range(1, 101) < decorChance ? 1 : 0;
                decorMap[x, y] = Random.Range(1, 101) < obstacleChance ? 2 : decorMap[x, y];
            }

        }

        thingMap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (decorMap[x, y] == 1)
                    thingMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), decor[Random.Range(0, decor.Length)]);
                else if (decorMap[x,y] == 2)
                {
                    thingMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), obstacles[Random.Range(0, obstacles.Length)]);
                }  
            }

        }

    }

    //updating green blob tiles from game of life algo
    public int[,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width,height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x+b.x >= 0 && x+b.x < width && y+b.y >= 0 && y+b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }

                if (oldMap[x,y] == 1)
                {
                    if (neighb < deathLimit) newMap[x, y] = 0;

                        else
                        {
                            newMap[x, y] = 1;

                        }
                }

                if (oldMap[x,y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 1;

                else
                {
                    newMap[x, y] = 0;
                }
                }

            }

        }



        return newMap;
    }

    //right click = do green simulation once
    //left click = clear map
    //key s = save tilemap
    //key g = generate decor
	void Update () {

        if (Input.GetMouseButtonDown(0))
            {
            doSim(numR);
            }


        if (Input.GetMouseButtonDown(1))
            {
            clearMap(true);
            }



        if (Input.GetKeyDown("s"))
        {
            SaveAssetMap();
            count++;
        }


        if (Input.GetKeyDown("g"))
        {
            generateThings();
        }




    }


    public void SaveAssetMap()
    {
        string saveName = "tmapXY_" + count;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/Prefabs/Tilemaps/" + saveName + ".prefab";
            if (PrefabUtility.CreatePrefab(savePath,mf))
            {
                EditorUtility.DisplayDialog("Tilemap saved", "Your Tilemap was saved under" + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT saved", "An ERROR occured while trying to saveTilemap under" + savePath, "Continue");
            }


        }


    }

    public void clearMap(bool complete)
    {

        topMap.ClearAllTiles();
        thingMap.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }


    }



}
