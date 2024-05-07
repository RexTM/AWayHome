using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.VFX;

public enum GameState
{
    WAIT,
    MOVE
};


public class Board : MonoBehaviour
{
   

    public GameState currentState = GameState.MOVE;
    public int width;
    public int height;
    public int offSet;
    public GameObject tilePrefab;
    public GameObject[] dots;
    private BackgroundTile[,] allTiles;
    public GameObject[,] allDots;
    public Dots currentDot;
    private FindMatches findMatches;
    private PlayerData playerData;

    //vfxs
    [SerializeField] VisualEffect _explodeEffect;
    [SerializeField] VisualEffect _burstPrefab;

    private void Start()
    { 

        findMatches = FindObjectOfType<FindMatches>();
        allTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width, height];
        Setup();
    }

    private void Setup()
    {
        //Generating 2D array
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //Generating the Location for Background and Pieces
                Vector3 tempPosition = new Vector3(i, j + offSet, 0f);
                Vector3 tilePosition = new Vector3(i, j, 0f);

                //Instantiating the background tiles
                GameObject backgroundTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "(" + i + ", " + j + ")";
                int dotToUse = Random.Range(0, dots.Length);
                int maxIterations = 0;

                //Making sure the generated peices dont match
                while(MatchesAt(i,j, dots[dotToUse]) && maxIterations < 100)
                {
                    dotToUse = Random.Range(0, dots.Length);
                    //Failsafe in case of infinite loop
                    maxIterations++;
                }
                maxIterations = 0;

                //Instantiating the pieces
                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                dot.GetComponent<Dots>().row = j;
                dot.GetComponent<Dots>().column = i;
                dot.transform.parent = this.transform;
                dot.name = "(" + i + ", " + j + ")";
                allDots[i, j] = dot;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
            {
                return true;
            }
        }
        else if(column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        if (allDots[column, row].GetComponent<Dots>().isMatched)
        {
            //checking how many pieces are matched
            if (findMatches.currentMatches.Count == 4)
            {
                findMatches.CheckBombs();
            }

            //Adding wishbones when there is a merge
            PlayerData.wishBones = PlayerData.wishBones+1;

            //Destroying Pieces that have matched. 
            findMatches.currentMatches.Remove(allDots[column, row]);
            //VFXs display after pieces match
            SpawnParticle();
            //Button click sound
            sfxManager.sfxInstance.Audio.PlayOneShot(sfxManager.sfxInstance.Merging);
            Destroy(allDots[column, row]);
            allDots[column, row] = null;

            
        }

        void SpawnParticle()
        {
            //instantiate
            VisualEffect newBustEffect = Instantiate(_burstPrefab, allDots[column, row].transform.position, Quaternion.identity);
            //play
            newBustEffect.Play();
            //destroy
            Destroy(newBustEffect.gameObject, 1.5f);
        }
        
    }

    

    //plays particle effect
    void PlayParticle()
    {
        _explodeEffect.Play();
    }

    public void DestroyMatches()
    {
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i,j] !=  null)
                {
                    DestroyMatchesAt(i,j);
                }
            }
        }
        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo()
    {
        int nullCount = 0;
        for (int i = 0;i < width;i++)
        {
            for (int j=0;j < height;j++)
            {
                if (allDots[i,j] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    allDots[i,j].GetComponent<Dots>().row -= nullCount;
                    allDots[i,j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.5f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i,j] == null)
                {
                    Vector2 tempPosition = new Vector2(i,j + offSet);
                    int dotToUse = Random.Range(0, dots.Length);
                    GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    allDots[i,j] = piece;
                    piece.GetComponent<Dots>().row = j;
                    piece.GetComponent<Dots>().column = i;
                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i,j] != null)
                {
                    if (allDots[i,j].GetComponent<Dots>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private IEnumerator FillBoardCo()
    {
        RefillBoard();
        yield return new WaitForSeconds(.08f); //0.8f

        while(MatchesOnBoard())
        {
            yield return new WaitForSeconds(.08f); //0.8f
            DestroyMatches();
        }
        findMatches.currentMatches.Clear();
        currentDot = null;
        yield return new WaitForSeconds(.3f); //1f
        currentState = GameState.MOVE;
    }

}
