using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private int baseValue => 2;
    public Color32[] colors;  
    
    public Color32 GetColor(int result)
    {
        return colors[(int)Mathf.Log(result, baseValue) - 1];
    }
}
