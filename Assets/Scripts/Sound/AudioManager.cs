using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake() { instance = this; }
            
    //Sound Effects
    public AudioClip sfx_attack, sfx_avaDamage, sfx_loseHeart, sfx_bullet, sfx_enemyDamage, sfx_enemyDies, sfx_screw, sfx_trash, sfx_click;
    //Music
    public AudioClip music_menu, music_game;
    //Current Music Object
    public GameObject currentMusicObject;

    //Sound Object
    public GameObject soundObject;
    
    public void PlaySFX(string sfxName)
    {
        switch(sfxName)
        {
            case "attack":
                SoundObjectCreation(sfx_attack);
                break;
            case "avaDamage":
                SoundObjectCreation(sfx_avaDamage);
                break;
            case "loseHeart":
                SoundObjectCreation(sfx_loseHeart);
                break;
            case "bullet":
                SoundObjectCreation(sfx_bullet);
                break;
            case "enemyDamage":
                SoundObjectCreation(sfx_enemyDamage);
                break;
            case "enemyDies":
                SoundObjectCreation(sfx_enemyDies);
                break;
            case "screw":
                SoundObjectCreation(sfx_screw);
                break;
            case "trash":
                SoundObjectCreation(sfx_trash);
                break;
            case "click":
                SoundObjectCreation(sfx_click);
                break;
            default:
                break;
        }
    }

    void SoundObjectCreation(AudioClip clip)
    {
        //Create SoundsObject gameobject
        GameObject newObject = Instantiate(soundObject, transform);
        //Assign audioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip;
        //Play the audio
        newObject.GetComponent<AudioSource>().Play();
    }

    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "game":
                MusicObjectCreation(music_game);
                break;
            case "menu":
                MusicObjectCreation(music_menu);
                break;
            default:
                break;
        }
    }

    void MusicObjectCreation(AudioClip clip)
    {
        //Check if there's an existing music object, if so delete it
        if (currentMusicObject)
            Destroy(currentMusicObject);
        //Create SoundsObject gameobject
        currentMusicObject = Instantiate(soundObject, transform);
        //Assign audioclip to its audiosource
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        //Make the audio source looping
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        //Play the audio
        currentMusicObject.GetComponent<AudioSource>().Play();
    }

}
