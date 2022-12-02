using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 mousePosition;

    private Camera mainCamera;

    [SerializeField]
    private Dati dati;

    private const float SPEED = 2f;
    public const float JUMP_TIME = 2f;
    private const int PUNTI_ACROBAZIA = 1;
    private float moveSpeed = SPEED;
    private bool volo = false;
    private float angle = 0;
    private bool acrobazia = false; 
    private float ultimaAngolazione = 0;
    Rigidbody2D rb;
    BoxCollider2D bc;

    //private Transform playerBodyTransform;
    Vector2 position = new Vector3(0f, 0f);

    private void Start(){
        rb = GetComponent<Rigidbody2D>();

        bc = GetComponent<BoxCollider2D>();

        mainCamera	= Camera.main;

        //playerBodyTransform = transform.GetChild(0);
    }

    private Vector2 PrendiPosizioneMouse(){
        //ritorna la posizione del mouse
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void SeguiMouseDelay(float speed){
        //salva la posizione del mouse in mousePosition
        mousePosition = PrendiPosizioneMouse();
        
        //muove il player dalla sua posizione verso quella del mouse
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
    }

    private void PuntaMouse(){
        //prende le posizioni del mouse
        mousePosition = PrendiPosizioneMouse();

        //prende la direzione in cui deve guardare
        Vector2 direzione = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        //sprite ruotato di 180 gradi (x) così punta correttametne al cursore
        transform.up = direzione;
    }

    private void Update(){
        
        //se è per terra
        if(!volo){

            //se è atterrato correttamente aggiunge punti altrimenti è come se si schiantasse
            if((transform.rotation.x >= -0.1 && transform.rotation.x <= 0.1) && acrobazia){
                dati.scriviPunteggio(PUNTI_ACROBAZIA);
            }else{
                if(acrobazia){
                    moveSpeed = 0;
                    StartCoroutine(scontro());   
                }
            }

            
            SeguiMouseDelay(moveSpeed);
            PuntaMouse();
            
            //non è in acrobazia
            acrobazia = false;
            angle = 0;
            
            //non si può muovere verso l'alto
            /*if(PrendiPosizioneMouse().y < transform.position.y){
                SeguiMouseDelay(moveSpeed);
                PuntaMouse();
            }
            /*else if(PrendiPosizioneMouse().x == transform.position.x){
                SeguiMouseDelay(moveSpeed/100);
                PuntaMouse();
            }*/

            //Se viene premuto il tasto sinistro salta
            if(Input.GetMouseButtonDown(0)){
                volo = true;
                angle = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
                //playerBodyTransform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                
                StartCoroutine(salto());
            }

            /*if (Input.GetMouseButtonDown(0)){
                Debug.Log(moveSpeed);
                if(moveSpeed == SPEED){
                    Debug.Log("Tasto SX premuto");
                    //bc.isTrigger = true;
                    volo = true;
                    transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
                    StartCoroutine(salto());
                }
            }*/

            /*if(transform.rotation.x < 0.1 && transform.rotation.x > -0.1){

                if((mousePosition.x - this.transform.position.x)*100 < 90 && (mousePosition.x - this.transform.position.x)*100 > -90){
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 0, (mousePosition.x - this.transform.position.x)*100);
                }
      
                //se si preme il tasto sx
                if (Input.GetMouseButtonDown(0)){
                    Debug.Log(moveSpeed);
                    if(moveSpeed == SPEED){
                        Debug.Log("Tasto SX premuto");
                        bc.isTrigger = true;
                        bc.transform.position = new Vector3(bc.transform.position.x, bc.transform.position.y, -0.1f);
                        StartCoroutine(salto());
                    }
                }
            }else{
                Debug.Log(transform.rotation.x);
                angle = 0;
                StartCoroutine(scontro());
            }*/
            
        }else{
            //essendo in volo non può girare
            mousePosition = new Vector3(mousePosition.x, mainCamera.ScreenToWorldPoint(mousePosition).y, mousePosition.z);

            //se viene premuto il tasto destro fa una piccola rotazione
            if(Input.GetMouseButtonDown(1)){
                acrobazia = true;
                angle += 60;
                if(angle == 360){
                    transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
                    angle = 0;
                }
               transform.rotation = Quaternion.Euler(new Vector3(angle,0,0));
            }

        }
        //si muove più veloce in volo
        position = Vector2.MoveTowards(transform.position, mousePosition, (moveSpeed/35) );
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

        //se si scontra con un ostacolo dinamico, quell'ostacolo si ferma nella sua posizione
        if(c.gameObject.name.Contains("dynamic")){
            c.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        //se si scontra contro lo Yeti il personaggio muore e viene caricata la schermata iniziale
        }else if(c.gameObject.name.Contains("Yeti")){
            Destroy(c.gameObject);
            SceneManager.LoadScene(0);
        }

        //aspetta 2 secondi prima di sbloccare il personaggio
        StartCoroutine(scontro());
        
    }

    IEnumerator scontro(){
        //aspetta 2 secondi e poi ricomincia ad andare
        yield return new WaitForSeconds(2);
        
        //vengono reimpostati i valori corretti
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        moveSpeed = SPEED;
    }

    public IEnumerator salto(){
        //rimane due secondi in volo
        yield return new WaitForSeconds(JUMP_TIME);

        //non sta più volando
        volo = false;
    }

    IEnumerator aspetta(int secondi){
        yield return new WaitForSeconds(secondi);
        Debug.Log("ASPETTA");
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
