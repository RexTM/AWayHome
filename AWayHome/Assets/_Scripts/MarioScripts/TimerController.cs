using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{

    public GameObject TimeUp;
    public Image timerLinearImage;
    public float timeRemaining;
    public float maxTime = 5.0f;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
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
            TimeUp.SetActive(true);
        }
    }
}
