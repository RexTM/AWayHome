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

    /*
    private void OnGUI()
    {
        /*
        if(GUI.Button(new Rect(150, 200, 100, 50), "Add WishBone"))
        {
            wishBones += 2;
        }
        */

    /*
        if(GUI.Button(new Rect(x, y, 100, 50), "Buy Choice"))
        {
            if(wishBones >= 5)
            {
                wishBones -= 5;
            }
            else
            {
                return;
            }
        }
    }
    */
}
