using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class Vigilante : MonoBehaviour
{
    private Transform target;
    private Transform enemy1;

    public float velocidadMovimiento = 3f;
    public float velocidadAtaque = 8f;

    public float rangoAtaque = 4.5f;
    public float rangoPersecucion = 10;
    //public float distanciaUmbral = 10f;
    //public float distanciaMaximaPersecucion = 10f;
    public float tiempoEsperaAtaque = 1f;

    private Vector3 direccion;
    //private bool isAttacking = false;
    //private bool isFollowing = false;
    public static bool isAttacking = false;
    public static bool isFollowing = false;

    enum Directions { LEFT, RIGHT};
    Directions currentDirection = Directions.LEFT;

    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 2f;

    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private void Start()
    {
        // Obtener la referencia al objeto del target y del enemy 
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemy1 = GameObject.FindGameObjectWithTag("Enemy1").transform;
    }

    private void Update()
    {
        //UnityEngine.Debug.Log("Velocidad de movimiento: " + velocidadMovimiento);
        if (target != null)
        {
            
            // Calcular la dirección hacia el target
            direccion = target.position - transform.position;
            direccion.z = 0f;
            direccion.y = 0f;

            // Calcular la distancia entre el enemigo y el target
            float distancia = direccion.magnitude;

            // Si la distancia es mayor que la distancia máxima de persecución, no hacer nada
            if (distancia > rangoPersecucion)
            {
                Debug.Log("hello");
                isAttacking = false;
                isFollowing = false;

                if(isAttacking == false && isFollowing==false){

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
                    //aquí

                //aquí
            }

            // Mover hacia el target si está fuera del rango de ataque y más cerca que el umbral de distancia
            else if (distancia <= rangoPersecucion && !isFollowing)
            {
                Debug.Log("hi");
                
                transform.Translate(direccion.normalized * velocidadMovimiento * Time.deltaTime);
                isFollowing = true;
                
            }
            // Si está dentro del rango de ataque, atacar
            else if (distancia <= rangoAtaque && !isAttacking)
            {
                StartCoroutine(AttackRoutine());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<HealthSystem>().LoseHealth(25);
            AudioManager.instance.PlaySFX("avaDamage");
        }
    }

    void checkDir()
    {
        if (isAttacking == true){
            return;
        }
        if (isFollowing == false){
            return;
        }
        if(isAttacking == false && isFollowing==false){
            return;
        }

        //All states can change its direction any time
        if (target.position.x > enemy1.position.x) {
            changeDirection(Directions.RIGHT);
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (target.position.x < enemy1.position.x) {
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
                    break;
                case Directions.LEFT:
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    break;
            }
        }
	}
    
    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        isFollowing = false;

        // Obtener una copia de la dirección almacenada en la variable miembro
        Vector3 attackDirection = direccion;

        // Realizar la transformación durante 4 segundos
        float attackDuration = 2f;
        float timer = 0f;
        while (timer < attackDuration)
        {
            transform.Translate(attackDirection.normalized * velocidadAtaque * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // Detener al enemy1 durante 2 segundos
        //float pauseDuration = 2f;
        yield return new WaitForSeconds(tiempoEsperaAtaque);

        isAttacking = false;
    }
}