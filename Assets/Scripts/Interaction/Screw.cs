using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Screw : MonoBehaviour
{    
    public enum InteractionType {NONE, Grab}
    
    public InteractionType interactType;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    public void Detect()
    {
        switch (interactType)
        {
            case InteractionType.Grab:
                //Add the object to the PickedUpItems list
                FindObjectOfType<InteractionSystem>().DetectScrews(gameObject);
                //Disable the object
                gameObject.SetActive(false);
                ScrewCounter.instance.IncreaseScrews(1);

                AudioManager.instance.PlaySFX("screw");
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }
    }
}
