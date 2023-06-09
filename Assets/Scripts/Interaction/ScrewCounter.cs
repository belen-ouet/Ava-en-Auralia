using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrewCounter : MonoBehaviour
{
    public static ScrewCounter instance;
    public Text screwText;
    public int currentScrews = 0;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        screwText.text = currentScrews.ToString();
    }

    public void IncreaseScrews(int v)
    {
        currentScrews += v;
        screwText.text = currentScrews.ToString();
    }
}

