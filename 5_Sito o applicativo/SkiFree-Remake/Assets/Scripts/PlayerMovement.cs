using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 mousePosition;

    private const float SPEED = 0.05f;
    private const float JUMP_TIME = 1f;
    private float moveSpeed = SPEED;
    Rigidbody2D rb;
    BoxCollider2D bc;
    Vector2 position = new Vector3(0f, 0f);

    private void Start(){
        rb = GetComponent<Rigidbody2D>();

        bc = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        
        //non si può muovere verso l'alto
        /*if(mousePosition.y < transform.position.y){
            position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed);
        }else if(mousePosition.x == transform.position.x){
            position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed/2);
        }*/
        
        //se si preme il tasto sx e il player ha il boxcollider attivo, quindi se è per terra, allora può "saltare"
        if (Input.GetMouseButtonDown(0) && bc.isTrigger == false){
            Debug.Log("Tasto SX premuto");
            if(moveSpeed == SPEED){
              bc.isTrigger = true;
               StartCoroutine(jump());
            }
        }
            

        position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed);
    }

    private void FixedUpdate(){
        rb.MovePosition(position);
        //setMoveSpeed();
    }

    public Vector3 getPosition(){
        return new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D c){
        moveSpeed = 0f;
        //per il tempo
        StartCoroutine(waiter());
        Debug.Log("COLLISIONE CON " + c);
    }

    IEnumerator waiter(){
        //aspetta tot secondi e poi ricomincia ad andare
        yield return new WaitForSeconds(2);
        moveSpeed = SPEED;
        Debug.Log(moveSpeed);
    }

    IEnumerator jump(){
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
    /*
    private void setMoveSpeed(){
        StartCoroutine(accelerate());
    }
    IEnumerator accelerate(){
        
        yield return new WaitForSeconds(0.5f);
        if(moveSpeed < 0.1f){
            moveSpeed += 0.01f;
            Debug.Log(moveSpeed);
        }
        
    }*/
}
