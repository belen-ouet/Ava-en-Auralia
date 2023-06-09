using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Vector2 playerInitPosition;

    void Start()
    {
        AudioManager.instance.PlayMusic("game");
        
        playerInitPosition = FindObjectOfType<Player>().transform.position;
    }

    public void Restart()
    {
        //1- Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //2- Reset the player's position 
        //Save the player's initial position when game starts
        //When respawning simply reposit the player to that init position
        //Reset the player's movement speed
        //FindObjectOfType<Player>().ResetPlayer();
        //FindObjectOfType<Player>().transform.position = playerInitPosition;
        //Reset the life count
    }
}
