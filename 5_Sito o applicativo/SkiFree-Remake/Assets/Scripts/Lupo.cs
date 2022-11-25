using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lupo : MonoBehaviour
{

    //se si scontra con il lupo imposta la condizione Colpito a 1 così l'animazione passerà da Run a Dead
    private void OnCollisionEnter2D(Collision2D c){
        this.GetComponent<Animator>().SetInteger("Colpito", 1);
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

}
