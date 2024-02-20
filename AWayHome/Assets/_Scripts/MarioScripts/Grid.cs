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

    private GamePiece[,] pieces;

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

        pieces = new GamePiece[width, height];
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++) 
            {
                GameObject newPiece = (GameObject)Instantiate(piecePrefabDict[PieceType.NORMAL], Vector2.zero, Quaternion.identity);
                newPiece.name = "Piece(" + x + "," + y + ")";
                newPiece.transform.parent = transform;

                pieces[x, y] = newPiece.GetComponent<GamePiece>();
                pieces[x, y].Init(x, y, this, PieceType.NORMAL);

                if(pieces[x,y].IsMovable())
                {
                    pieces[x,y].MovableComponent.Move(x, y);
                }
            }
        }


        transform.position = new Vector3(gridPosX, gridPosY, 0);
        transform.localScale = new Vector3 (gridScale, gridScale, gridScale);

        grid.SetActive(true);
    }

    public Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(transform.position.x - width / 2.0f + x, transform.position.y + height / 2.0f - y);
    }
}
