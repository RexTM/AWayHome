using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;

public class TimerController : MonoBehaviour
{
    [Header("Difficulty Level")]
    [Range(0, 4)]
    public int difficulty = 0;

    [SerializeField]
    public static float maxTime = 60f;

    public GameObject restartButton;
    public GameObject TimeUp;
    public Image timerLinearImage;
    public float timeRemaining;
    public TextMeshProUGUI timerText;

    private Board board;



    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        DifficultyLevel();
        timeRemaining = maxTime;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(timeRemaining > 0) 
        {
            timeRemaining -= Time.deltaTime;
            timerLinearImage.fillAmount = timeRemaining / maxTime;
            timerText.text = timeRemaining.ToString("0");
        }
        else
        {
            board.currentState = GameState.WAIT;
            TimeUp.SetActive(true);
            restartButton.SetActive(true);
        }
    }

    public void DifficultyLevel()
    {
        switch (difficulty)
        {
            case 0:
                maxTime = 60f;
                break;
            case 1:
                maxTime = 50f;
                break;
            case 2:
                maxTime = 40f;
                break;
            case 3:
                maxTime = 30f;
                break;
            case 4:
                maxTime = 20f;
                break;
            default:
                break;
        }
    }
}


