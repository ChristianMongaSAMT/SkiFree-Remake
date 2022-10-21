using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] ostacoli;
    public Camera cam;
    private const int DISTANZA_VERTICALE = 3;
    private const int DISTANZA_ORIZZONTALE = 20;
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
                    Instantiate(ostacoli[rand], new Vector2(xCamera+i, yCamera - DISTANZA_VERTICALE - randSposta), Quaternion.identity);
                }
                //nuovo numero random per la scelta dell'ostacolo da generare
                rand = Random.Range(0, ostacoli.Length);
            }
        }else{
            //se la y dove bisogna creare la riga di ostacoli si trova fuori dal range della riga precedente (per non fare sovrapposizioni) crea la riga
            if(yCamera - DISTANZA_VERTICALE < yUsate[yUsate.Count-1] - RANGE ){
                yUsate.Add(yCamera-DISTANZA_VERTICALE);

                //loop per generare tutti gli ostacoli di una riga
                for(int i = -20; i < DISTANZA_ORIZZONTALE; i++){
                    rand = Random.Range(0, ostacoli.Length);
                    int randPercentuale = Random.Range(0, 101);
                    
                    if(randPercentuale < 60){
                        float randSposta = Random.Range(0, 1f);
                        Instantiate(ostacoli[rand], new Vector2(xCamera+i, yCamera - DISTANZA_VERTICALE - randSposta), Quaternion.identity);
                    }
                }
            }
        }

        if(yUsate.Count > 3){
            float riga = yUsate[yUsate.Count-3];
            for(int i = -20; i < 20; i++){
                
                
            }
        }
    }
}
