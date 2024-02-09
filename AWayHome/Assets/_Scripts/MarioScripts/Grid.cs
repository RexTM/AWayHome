using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Grid : MonoBehaviour
{
    public enum PieceType
    {
        NORMAL,
        Count,
    };

    [System.Serializable]
    public struct PiecePrefab
    {
        public PieceType type;
        public GameObject prefab;
    };

    public int width;
    public int height;

    public float gridPosX;
    public float gridPosY;

    public float gridScale;

    public PiecePrefab[] piecePrefabs;

    public GameObject backgroundPrefab;
    public GameObject grid;

    private Dictionary<PieceType, GameObject> piecePrefabDict;

    private GameObject[,] pieces;

    // Start is called before the first frame update
    void Start()
    {
        grid.SetActive(false);

        piecePrefabDict = new Dictionary<PieceType, GameObject>();
        for(int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePrefabDict.ContainsKey(piecePrefabs[i].type))
            {
                piecePrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }


        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                GameObject background = (GameObject)Instantiate(backgroundPrefab, GetWorldPosition(x, y, 0), Quaternion.identity);
                background.transform.parent = transform;
            }
        }

        pieces = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++) 
            {
                pieces[x,y] = (GameObject)Instantiate(piecePrefabDict[PieceType.NORMAL], GetWorldPosition(x, y, 1), Quaternion.identity);
                pieces[x,y].name = "Piece(" + x + "," + y + ")";
                pieces[x,y].transform.parent = transform;
            }
        }


        transform.position = new Vector3(gridPosX, gridPosY, 0);
        transform.localScale = new Vector3 (gridScale, gridScale, gridScale);

        grid.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(transform.position.x - width / 2.0f + x, transform.position.y + height / 2.0f - y);
    }
}
