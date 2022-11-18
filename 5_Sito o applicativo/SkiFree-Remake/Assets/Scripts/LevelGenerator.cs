using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] ostacoli;
    public GameObject[] ostacoliDinamici;
    public GameObject yeti;
    private bool yetiGenerato = false;
    public Camera cam;

    private GameObject dynamicObject;
    private const int DISTANZA_VERTICALE = 3;
    private const int DISTANZA_ORIZZONTALE = 20;
    private const float VELOCITA_OSTACOLI = 5f;
    private const int POSIZIONE_SPAWN_OSTACOLO_DINAMICO = 10;
    private const int RANGE = 2;
    private List<float> yUsate = new List<float>();

    private void Update(){
        //salvo le posizioni della camera
        float xCamera = cam.transform.position.x;
        float yCamera = cam.transform.position.y;

        //numero random per scegliere l'oggetto da generare
        int rand = Random.Range(0, ostacoli.Length);
        
        //se è la prima riga che si genera
        if(yUsate.Count == 0){
            //aggiungo la y della riga da generare alla lista di tutte le y utilizzate
            yUsate.Add(yCamera-DISTANZA_VERTICALE);

            //loop per generare tutti gli ostacoli di una riga
            for(int i = DISTANZA_ORIZZONTALE*-1; i < DISTANZA_ORIZZONTALE; i++){
                //numero random per decidere se generare o meno un ostacolo
                int randPercentuale = Random.Range(0, 101);             
                if(randPercentuale < 60){
                    //numero random per spostare un po' l'ostacolo, così da non generarli tutti in modo lineare
                    float randSposta = Random.Range(0, 1f);

                    //istanzio l'ostacolo 
                    Instantiate(
                        ostacoli[rand], 
                        new Vector2(xCamera+i, yCamera - DISTANZA_VERTICALE - randSposta), 
                        Quaternion.identity
                    );
                }
                //nuovo numero random per la scelta dell'ostacolo da generare
                rand = Random.Range(0, ostacoli.Length);
            }
        }else{
            //se la y dove bisogna creare la riga di ostacoli si trova fuori dal range della riga precedente (per non fare sovrapposizioni) crea la riga
            if(yCamera - DISTANZA_VERTICALE < yUsate[yUsate.Count-1] - RANGE ){
                yUsate.Add(yCamera-DISTANZA_VERTICALE);
                int randPercentuale = 0;

                //loop per generare tutti gli ostacoli di una riga
                for(int i = -20; i < DISTANZA_ORIZZONTALE; i++){
                    rand = Random.Range(0, ostacoli.Length);
                    randPercentuale = Random.Range(0, 101);
                    
                    if(randPercentuale < 60){
                        float randSposta = Random.Range(0, 1f);
                        Instantiate(ostacoli[rand], new Vector2(xCamera+i, yCamera - DISTANZA_VERTICALE - randSposta), Quaternion.identity);
                    }
                }


                //--------Generazione ostacoli dinamici--------//
                randPercentuale = Random.Range(0, 101);

                //se la percentuale è sotto il 20
                if(randPercentuale < 70){
                    rand = Random.Range(0, ostacoliDinamici.Length);
                    float randSposta = Random.Range(2, 3);
                    randPercentuale = Random.Range(0,101);

                    float posizioneX;
                    float velocita;

                    //id che stabilisce se deve generarsi a destra e va verso sinistra o il contrario
                    if(randPercentuale < 50){
                        //imposto valori se spawna a destra
                        posizioneX = xCamera+POSIZIONE_SPAWN_OSTACOLO_DINAMICO;
                        velocita = VELOCITA_OSTACOLI * -1;
                    }else{
                        //imposto valori se spawna a sinistra
                        posizioneX = xCamera-POSIZIONE_SPAWN_OSTACOLO_DINAMICO;
                        velocita = VELOCITA_OSTACOLI;
                    }
                    //genera oggetto
                    var dynamicObject = Instantiate(ostacoliDinamici[rand], new Vector2(posizioneX, yCamera - DISTANZA_VERTICALE - randSposta), Quaternion.identity);
                    
                    //ricava rigidbody
                    var rb = dynamicObject.GetComponent<Rigidbody2D>();
                    
                    //imposta una velocità
                    rb.velocity = new Vector2(velocita, 0f);
                }
            }
        }

        //---Spawn Yeti---//
        if(yCamera <= -10f && !yetiGenerato){
            //dopo una certa distanza genera lo Yeti sopra il player e inizia a seguirlo
            Instantiate(yeti, new Vector2(xCamera, yCamera+3),Quaternion.identity);
            yetiGenerato = true;
        }
    }
}
