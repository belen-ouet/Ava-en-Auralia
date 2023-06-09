using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{    
    //Detection Point
    public Transform detectionPoint;
    //Detection Radius
    private const float detectionRadius = 0.2f;
    //Detection Layer
    public LayerMask detectionLayer;
    //Cached Trigger Object
    public GameObject detectedObject;

    //List of picked items
    public List<GameObject> pickedItems = new List<GameObject>();
    public List<GameObject> pickedScrews = new List<GameObject>();

    void Update()
    {
        if(DetectObject())
        {           
            if(InteractInput())
            {
                //Pick up object            
                detectedObject.GetComponent<Item>().Interact();
            } else
            {
                //Touch object
                detectedObject.GetComponent<Screw>().Detect();
            }            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool DetectObject()
    {        
       Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position,detectionRadius,detectionLayer); 
        
        if(obj==null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    public void PickUpItem(GameObject item)
    {
        pickedItems.Add(item);
    }

    public void DetectScrews(GameObject item)
    {
        pickedScrews.Add(item);
    }
}
