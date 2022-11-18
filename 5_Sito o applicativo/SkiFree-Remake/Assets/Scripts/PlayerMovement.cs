using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Vector3 mousePosition;

    private const float SPEED = 0.05f;
    private const float JUMP_TIME = 1f;
    private float moveSpeed = SPEED;

    private float angle = 0;
    float yMousePosition = 0.0f;
    Rigidbody2D rb;
    BoxCollider2D bc;
    Vector2 position = new Vector3(0f, 0f);

    private void Start(){
        rb = GetComponent<Rigidbody2D>();

        bc = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        //se è per terra
        
        if(bc.isTrigger == false){
            if(transform.rotation.x < 0.1 && transform.rotation.x > -0.1){
                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                //non si può muovere verso l'alto
                /*if(mousePosition.y < transform.position.y){
                    position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed);
                }else if(mousePosition.x == transform.position.x){
                    position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed/2);
                }*/
                
                //se si preme il tasto sx
                if (Input.GetMouseButtonDown(0)){
                    Debug.Log(moveSpeed);
                    if(moveSpeed == SPEED){
                        Debug.Log("Tasto SX premuto");
                        bc.isTrigger = true;
                        bc.transform.position = new Vector3(bc.transform.position.x, bc.transform.position.y, -0.1f);
                        StartCoroutine(jump());
                    }
                }
            }else{
                Debug.Log(transform.rotation.x);
                angle = 0;
                StartCoroutine(waiter());
            }
            
        }else{
            //essendo in volo non può girare
            mousePosition = new Vector3(mousePosition.x, Camera.main.ScreenToWorldPoint(mousePosition).y, mousePosition.z);
            //se viene premuto il tasto destro fa una piccola rotazione
            if(Input.GetMouseButtonDown(1)){
                angle += 36;
                if(angle == 360){
                    transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
                    angle = 0;
                }
                transform.Rotate(new Vector3(angle,0,0));
            }

        }
        position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed);
    }

    private void FixedUpdate(){
        rb.MovePosition(position);
        //setMoveSpeed();
    }

    /*public Vector3 getPosition(){
        return new Vector3(transform.position.x, transform.position.y, 0);
    }*/

    private void OnCollisionEnter2D(Collision2D c){
        moveSpeed = 0f;
        Debug.Log(c.gameObject.name);

        //se si scontra con un ostacolo dinamico, quell'ostacolo si ferma nella sua posizione
        if(c.gameObject.name.Contains("dynamic")){
            c.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        //se si scontra contro lo Yeti il personaggio muore e viene caricata la schermata iniziale
        }else if(c.gameObject.name.Contains("Yeti")){
            Destroy(c.gameObject);
            SceneManager.LoadScene(0);
        }
        //per il tempo
        StartCoroutine(waiter());
    }

    IEnumerator waiter(){
        //aspetta tot secondi e poi ricomincia ad andare
        yield return new WaitForSeconds(2);
        //viene riportato dritto
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        
        moveSpeed = SPEED;
        Debug.Log(moveSpeed);
    }

    IEnumerator jump(){
        yield return new WaitForSeconds(2f);
        bc.isTrigger = false;
        bc.transform.position = new Vector3(bc.transform.position.x, bc.transform.position.y, 0.0f);
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
