using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : PlayerData
{
    public int x;
    public int y;
    UIManager uiManager;
    public TMP_Text WishBones;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WishBones.text = "Wish Bones: " + wishBones;
    }

}
