using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dots : MonoBehaviour
{
    [Header("Board Variables")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;

    private FindMatches findMatches;
    private Board board;
    public GameObject otherDot;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    public float swipeAngle = 0;
    public float swipeResist = 1f;

    [Header("Power Up")]
    public bool isPower;
    public GameObject Power;


    private PlayerData playerData;


    // Start is called before the first frame update
    void Start()
    {
        isPower = false;

        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        findMatches.FindAllMatches();

        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > 0.2f)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.2f);
            if (board.allDots[column,row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }

        if (Mathf.Abs(targetY - transform.position.y) > 0.2f)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.2f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();

        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }

    }


    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.2f);//0.5f
        if (otherDot != null)
        {
            if (!isMatched && !otherDot.GetComponent<Dots>().isMatched)
            {
                otherDot.GetComponent<Dots>().row = row;
                otherDot.GetComponent<Dots>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(0.2f);//0.5f
                board.currentDot = null;
                board.currentState = GameState.MOVE;
            }
            else
            {
                board.DestroyMatches();
            }
            //otherDot = null;
        }
    }
    
    private void OnMouseDown()
    {
        if (board.currentState == GameState.MOVE)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.MOVE)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            board.currentState = GameState.WAIT;
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            
            board.currentDot = this;
        }
        else
        {
            board.currentState = GameState.MOVE;
        }
    }
    
    void MovePiecesActual(Vector2 direction)
    {
        otherDot = board.allDots[column + (int)direction.x, row + (int)direction.y];
        previousRow = row;
        previousColumn = column;
        otherDot.GetComponent<Dots>().column += -1 * (int)direction.x;
        otherDot.GetComponent<Dots>().row += -1 * (int)direction.y;
        column += (int)direction.x;
        row += (int)direction.y;
        StartCoroutine(CheckMoveCo());
    }


    void MovePieces()
    {
        //Moving pieces depending on the angle and also checking if the piece is within the board play space
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width-1)
        {
            //Debug.Log("Right Swipe");
            ////Right Swipe
            //otherDot = board.allDots[column + 1, row];
            //previousRow = row;
            //previousColumn = column;
            //otherDot.GetComponent<Dots>().column -= 1;
            //column += 1;
            //StartCoroutine(CheckMoveCo());
            MovePiecesActual(Vector2.right);
            Handheld.Vibrate();
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height-1)
        {
            Debug.Log("Up Swipe");
            //Up Swipe
            otherDot = board.allDots[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dots>().row -= 1;
            row += 1;
            StartCoroutine(CheckMoveCo());
            Handheld.Vibrate();
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135 ) && column > 0)
        {
            Debug.Log("Left Swipe");
            //Left Swipe
            otherDot = board.allDots[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dots>().column += 1;
            column -= 1;
            StartCoroutine(CheckMoveCo());
            Handheld.Vibrate();
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            Debug.Log("Down Swipe");
            //Down Swipe
            otherDot = board.allDots[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dots>().row += 1;
            row -= 1;
            StartCoroutine(CheckMoveCo());
            Handheld.Vibrate();
        }
        else
        {
            board.currentState = GameState.MOVE;
        }
    }

    void FindMatches()
    {
        if(column > 0 && column < board.width -1 )
        {
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            if(leftDot1 != null && rightDot1 != null)
            {
                if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Dots>().isMatched = true;
                    rightDot1.GetComponent<Dots>().isMatched = true;
                    isMatched = true;

                    

                }
            }
            
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allDots[column, row + 1];
            GameObject downDot1 = board.allDots[column, row - 1];
            if(upDot1 != null && downDot1 != null)
            {
                if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Dots>().isMatched = true;
                    downDot1.GetComponent<Dots>().isMatched = true;
                    isMatched = true;

                    
                }
            }
            
        }
    }

    public void MakeBomb()
    {
        isPower = true;
        GameObject glow = Instantiate(Power, transform.position, Quaternion.identity);
        glow.transform.parent = this.transform;
    }
}
