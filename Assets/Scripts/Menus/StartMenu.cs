using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject CreditsUI;

    void Start()
    {
        AudioManager.instance.PlayMusic("menu");
    }

    public void PlayGame ()
    {
        AudioManager.instance.PlaySFX("click");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void GoCredits ()
    {
        CreditsUI.SetActive(true);

        AudioManager.instance.PlaySFX("click");
    }

    public void GoStartMenu ()
    {
        CreditsUI.SetActive(false);

        AudioManager.instance.PlaySFX("click");
    }

    public void QuitGame ()
    {
        AudioManager.instance.PlaySFX("click");
        
        Debug.Log ("QUIT!!");
        Application.Quit();
    }

}
