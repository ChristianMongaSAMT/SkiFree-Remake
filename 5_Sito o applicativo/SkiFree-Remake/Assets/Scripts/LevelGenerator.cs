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
    private List<float> xUsate = new List<float>();
    private List<float> yUsate = new List<float>();
    

    private void Update(){
        
        float xCamera = cam.transform.position.x;
        float yCamera = cam.transform.position.y;
        int rand = Random.Range(0, ostacoli.Length);
        
        if(yUsate.Count == 0){
            Instantiate(ostacoli[rand], new Vector2(xCamera, yCamera-DISTANZA_VERTICALE), Quaternion.identity);
            yUsate.Add(yCamera-DISTANZA_VERTICALE);
        }else{
            //se la y dove bisogna creare gli ostacoli è troppo vicina/ha già degli ostacoli salta
            //se la y si trova fuori il range di quella precedente crea
            // < perché si lavora con il "-"
            if(yCamera - DISTANZA_VERTICALE < yUsate[yUsate.Count-1] - RANGE ){
                //----Creazione 1 Albero alla stessa X della camera----//
                //Instantiate(ostacoli[rand], new Vector2(xCamera, yCamera - DISTANZA_VERTICALE), Quaternion.identity);
                //yUsate.Add(yCamera-DISTANZA_VERTICALE);
                //----------------------------------------------------//
                yUsate.Add(yCamera-DISTANZA_VERTICALE);
                for(int i = -20; i < DISTANZA_ORIZZONTALE; i++){
                    rand = Random.Range(0, ostacoli.Length);
                    int randPercentuale = Random.Range(0, 101);
                    float randSposta = Random.Range(0, 1f);
                    if(randPercentuale < 60){
                        Instantiate(ostacoli[rand], new Vector2(xCamera+i, yCamera - DISTANZA_VERTICALE - randSposta), Quaternion.identity);
                        xUsate.Add(xCamera+i);
                    }
                }
            }
        }
    }
}
