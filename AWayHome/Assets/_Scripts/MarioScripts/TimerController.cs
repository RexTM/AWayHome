using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    [Header("Change Timer Value Here")]
    public float maxTime = 5.0f;

    public GameObject TimeUp;
    public Image timerLinearImage;
    public float timeRemaining;
    public TextMeshProUGUI timerText;

    private Board board;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        timeRemaining = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0) 
        {
            timeRemaining -= Time.deltaTime;
            timerLinearImage.fillAmount = timeRemaining / maxTime;
            timerText.text = timeRemaining.ToString("0.00");
        }
        else
        {
            board.currentState = GameState.WAIT;
            TimeUp.SetActive(true);
        }
    }
}
