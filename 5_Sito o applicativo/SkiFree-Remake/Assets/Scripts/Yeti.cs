using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeti : MonoBehaviour
{
    private GameObject player;
    private float velocita = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //muove lo yeti verso la posizione del player
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, velocita * Time.deltaTime);

        if(player.transform.position.x < this.transform.position.x){
            
            //ruota di 180° se il personaggio si trova alla destra dello yeti
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 180, this.transform.eulerAngles.z);

        }else if(player.transform.position.x > this.transform.position.x){
            
            //torna alla rotazione di 0° se il personaggio si trova alla sinistra dello yeti
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z);

        }
    }

    private void OnCollisionEnter2D(Collision2D c){
        Destroy(c.gameObject);
    }

}
