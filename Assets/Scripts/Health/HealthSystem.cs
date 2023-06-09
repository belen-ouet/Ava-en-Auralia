using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;

    public Image fillBar;
    public float health;

    public void LoseHealth(int value)
    {
        //Do nothing if you are out of health
        if (health <= 0)
            return;

        //Reduce health 
        health -= value;

        //Refresh the UI fillBar
        fillBar.fillAmount = health / 100;

        //Check if your health is zero or less => lose heart
        if (health <= 0)
        {
            LoseLife();        
            health = 125;
            AudioManager.instance.PlaySFX("loseHeart");      
        }
    }

    public void LoseLife()
    {
        //If no lives remaining do nothing
        if (livesRemaining == 0)
            return;
        
        //Decrease the value of livesRemaining
        livesRemaining--;

        //Hide one of the life images
        lives[livesRemaining].enabled = false;

        //If we run out of lives we lose the game
        if(livesRemaining==0)
        {
            FindObjectOfType<Player>().Die();            
        }
    }
}
