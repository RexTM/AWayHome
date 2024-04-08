using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();

    private void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }
    
    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentDot = board.allDots[i,j];
                if(currentDot != null)
                {
                    if(i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];
                        if(leftDot != null && rightDot != null)
                        {
                            if(leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {
                                if (currentDot.GetComponent<Dots>().isPower || leftDot.GetComponent<Dots>().isPower || rightDot.GetComponent<Dots>().isPower)
                                {
                                    currentMatches.Union(GetRowPieces(j));
                                }

                                if(!currentMatches.Contains(leftDot))
                                {
                                    currentMatches.Add(leftDot);
                                }
                                leftDot.GetComponent<Dots>().isMatched = true;
                                if (!currentMatches.Contains(rightDot))
                                {
                                    currentMatches.Add(rightDot);
                                }
                                rightDot.GetComponent<Dots>().isMatched = true;
                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                currentDot.GetComponent<Dots>().isMatched = true;
                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];
                        GameObject downDot = board.allDots[i , j - 1];
                        if (upDot != null && downDot != null)
                        {
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {
                                if (currentDot.GetComponent<Dots>().isPower || upDot.GetComponent<Dots>().isPower || downDot.GetComponent<Dots>().isPower)
                                {
                                    currentMatches.Union(GetColumnPieces(i));
                                }

                                if (!currentMatches.Contains(upDot))
                                {
                                    currentMatches.Add(upDot);
                                }
                                upDot.GetComponent<Dots>().isMatched = true;
                                if (!currentMatches.Contains(downDot))
                                {
                                    currentMatches.Add(downDot);
                                }
                                downDot.GetComponent<Dots>().isMatched = true;
                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                currentDot.GetComponent<Dots>().isMatched = true;
                            }
                        }
                    }
                }
            }        
        }
    }

    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.height; i++)
        {
            if (board.allDots[column, i] != null)
            {
                dots.Add(board.allDots[column, i]);
                board.allDots[column, i].GetComponent<Dots>().isMatched = true;
            }

        }

        return dots;
    }

    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if (board.allDots[i, row] != null)
            {
                dots.Add(board.allDots[i, row]);
                board.allDots[i, row].GetComponent<Dots>().isMatched = true;
            }

        }

        return dots;
    }

    public void CheckBombs()
    {
        //was anything moved?
        if (board.currentDot != null)
        {
            //does the moved piece match?
            if (board.currentDot.isMatched)
            {
                //unmatch the pieces
                board.currentDot.isMatched = false;
                //make bomb
                if (!board.currentDot.isPower)
                {
                    board.currentDot.MakeBomb();
                }
            }
            //does the other piece match?
            else if (board.currentDot.otherDot != null)
            {
                //unmatch the pieces
                Dots otherDot = board.currentDot.otherDot.GetComponent<Dots>();
                if(otherDot.isMatched)
                {
                otherDot.isMatched = false;
                }
                //make bomb
                if (!otherDot.isPower)
                {
                    otherDot.MakeBomb();
                }
                
            }
            
        }

    }
}
