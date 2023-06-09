using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{    
    public enum InteractionType { NONE, PickUp}
    
    public InteractionType interactType;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    public void Interact()
    {
        switch (interactType)
        {
            case InteractionType.PickUp:
                //Add the object to the PickedUpItems list
                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                //Disable the object
                gameObject.SetActive(false);
                ItemCounter.instance.IncreaseItems(1);

                AudioManager.instance.PlaySFX("trash");
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }
    }
}
