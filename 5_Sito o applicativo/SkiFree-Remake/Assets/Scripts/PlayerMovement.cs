using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        /*
        //non si può muovere verso l'alto
        if(mousePosition.y < transform.position.y){
            position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed);
        }else if(mousePosition.x == transform.position.x){
            position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed/2);
        }
        */
         position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed);
    }

    private void FixedUpdate(){
        rb.MovePosition(position);
    }

    public Vector3 getPosition(){
        //Debug.Log(transform.position.x + " " + transform.position.y);
        return new Vector3(transform.position.x, transform.position.y, 0);
    }
}
