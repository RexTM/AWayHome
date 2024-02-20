using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    private BackgroundTile[,] allTiles;

    private void Start()
    {
        allTiles = new BackgroundTile[width, height];
        Setup();
    }

    private void Setup()
    {
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 tempPosition = new Vector3(i, j, 91f);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity);
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "(" + i + ", " + j + ")";
            }
        }
    }
}
