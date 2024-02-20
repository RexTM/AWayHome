using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPiece : MonoBehaviour
{
    public enum ColorType
    {
        BLUE,
        GREEN,
        RED,
        YELLOW,
        ANY,
        COUNT
    };

    [System.Serializable]
    public struct ColorBall
    {
        public ColorType color;
        public MeshRenderer colorVis;
    };

    public ColorBall[] colorBalls;

    private ColorType color;

    public ColorType Color
    {
        get { return color; }
        set { SetColor(value); }
    }

    private MeshFilter colorVis;

    private Dictionary<ColorType, MeshRenderer> ColorBallDict;

    private void Awake()
    {
        colorVis = transform.Find("piece").GetComponent<MeshFilter>();

        ColorBallDict = new Dictionary<ColorType, MeshRenderer>();
        for (int i = 0; i < colorBalls.Length; i++)
        {
            if (!ColorBallDict.ContainsKey(colorBalls[i].color))
            {
                ColorBallDict.Add(colorBalls[i].color, colorBalls[i].colorVis);
            }
        }
    }

    public void SetColor(ColorType newColor)
    {
        color = newColor;

        if (ColorBallDict.ContainsKey(newColor)) 
        { 
          // colorVis = ColorBallDict[newColor];
        }
    }
}
