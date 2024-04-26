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

    public Tilemap bgMap;
    public Tilemap obstacleMap;
    public Tilemap thingMap;

    public RuleTile bgTile;
    public Tile[] obstacles;
    public Tile[] decor;
    public Tile[][] tileTemplate;

    int width;
    int height;

    //for generation of dark green patches using game of life, one click = one update
    private void doSim(int nu)
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
                    bgMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), bgTile);
                    //botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
            }
        }


    }

    //initial generation of green patches
    private void initPos()
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
    private void generateThings()
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
        obstacleMap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (decorMap[x, y] == 1)
                {
                    int select = Random.Range(0, decor.Length);
                    if (select < 3)
                    {
                        select = Random.Range(0, decor.Length);
                    }
                    thingMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), decor[select]);
                }
                else if (decorMap[x, y] == 2)
                {
                    obstacleMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), obstacles[Random.Range(0, obstacles.Length)]);
                }  
            }

        }

    }

    //updating green blob tiles from game of life algo
    private int[,] genTilePos(int[,] oldMap)
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


    private void SaveAssetMap()
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

    private void clearMap(bool complete)
    {

        bgMap.ClearAllTiles();
        thingMap.ClearAllTiles();
        obstacleMap.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }


    }

    //private void autoTiler()
    //{

    //}


}
