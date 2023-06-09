using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class Proyectiles : MonoBehaviour
{
    private Transform target;
    private Transform enemy2;
    
    public float rangoAtaque = 4.5f;
    public float tiempoEsperaAtaque = 1f;


    private Vector3 direccion;
    private bool isAttacking = false;

    enum Directions { LEFT, RIGHT};
    Directions currentDirection = Directions.LEFT;

    //WAYPOINTS
    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 2f;

    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    //PROYECTIL
    public GameObject[] bullet;
    private GameObject bulletParent;
    public GameObject bulletParentLeft;
    public GameObject bulletParentRight;

    public float fireRate = 1f;
    private float nextFireTime;
    //public float bulletSpeed = 5f;
    //public float shootingInterval = 2f; //tiempo entre disparos
    private float timer;


    private void Start()
    {
        // Obtener la referencia al objeto del target y del enemy 
        target = GameObject.FindGameObjectWithTag("Target").transform;
        enemy2 = GameObject.FindGameObjectWithTag("Enemy2").transform;
        bulletParent = bulletParentLeft;
    }

    private void Update()
    {
        if (target != null)
        {      
            // Calcular la dirección hacia el target
            direccion = target.position - transform.position;
            direccion.z = 0f;
            direccion.y = 0f;

            // Calcular la distancia entre el enemigo y el target
            float distancia = direccion.magnitude;


            // Si target está fuera del rango de ataque, no hacer nada
            if (distancia > rangoAtaque)
            {
                isAttacking = false;
                if(!isAttacking)
                {
                    if (_waiting)
                    {
                        _waitCounter += Time.deltaTime;
                        if (_waitCounter < _waitTime)
                            return;
                        _waiting = false;
                    }

                    Transform wp = waypoints[_currentWaypointIndex];
                    Vector3 targetPosition = new Vector3(wp.position.x, transform.position.y, transform.position.z);

                    if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                    {
                        transform.position = targetPosition;
                        _waitCounter = 0f;
                        _waiting = true;

                        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
                        if(targetPosition.x>transform.position.x){
                            changeDirection(Directions.RIGHT);
                        }
                        else{
                            changeDirection(Directions.LEFT);
                        }
                        //transform.Rotate(0, 180, 0, Space.Self);                    

                        // Calculate the new target position with only the X value
                        //Vector2 newTargetPosition = new Vector2(target.position.y, transform.position.x);
                        //transform.LookAt(newTargetPosition);
                    }
                    
                }
            }

            // Si target está dentro del rango de ataque, atacar
            else if (distancia <= rangoAtaque && nextFireTime <Time.time)
            {
                int randomIndex = Random.Range(0, bullet.Length);

                Instantiate(bullet[randomIndex], bulletParent.transform.position, bulletParent.transform.rotation);
                nextFireTime = Time.time + fireRate;

            }
            checkDir();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar un gizmo para visualizar el rango de ataque en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }

    void checkDir ()
    {
        //All states can change its direction any time
        if (target.position.x > enemy2.position.x) {
            changeDirection(Directions.RIGHT);
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (target.position.x < enemy2.position.x) {
            changeDirection(Directions.LEFT);
            transform.Translate(Vector3.left * Time.deltaTime);
        }
    }

    
    void changeDirection(Directions direction)
	{
		if (currentDirection != direction)
		{
			currentDirection = direction;
            switch (direction) {
                case Directions.RIGHT:
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    bulletParent = bulletParentRight;
                    break;
                case Directions.LEFT:
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    bulletParent = bulletParentLeft;
                    break;
            }
        }
	}
}