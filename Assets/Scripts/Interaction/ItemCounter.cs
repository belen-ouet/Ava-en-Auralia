using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCounter : MonoBehaviour
{
    public static ItemCounter instance;
    public Text itemText;
    public int currentItems = 0;

    public GameObject filter;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        itemText.text = currentItems.ToString();
    }

    public void IncreaseItems(int v)
    {
        currentItems += v;
        itemText.text = currentItems.ToString();
    }

    void Update()
    {
        if(currentItems==10)
        {
            filter.SetActive(false);

            Invoke("NextScene", 5);            
        }
    }
    
    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
