using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float posXInic, anchura;
    public GameObject Camara;
    [SerializeField]
    private float efectoParallax;

    void Start()
    {
        posXInic = transform.position.x;
        anchura = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distX = Camara.transform.position.x * efectoParallax;
        transform.position = new Vector3 (posXInic+distX,
                                    transform.position.y,
                                    transform.position.z);
        //to avoid the end of the layers from showing                          
        float posicionCamara = Camara.transform.position.x;
        float AvanceParallaX = posXInic + (posicionCamara-posXInic) * efectoParallax;
        float distCamaraParallax = posicionCamara - AvanceParallaX;
        
        transform.position = new Vector3 (AvanceParallaX, transform.position.y, transform.position.z);
        
        if (distCamaraParallax >= anchura)
        posXInic = posicionCamara;
        else if (distCamaraParallax < -anchura)
        posXInic = posicionCamara;
    }
}
