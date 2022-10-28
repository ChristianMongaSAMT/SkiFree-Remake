using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private const float DISTANZA_PER_DISTRUGGERE = 5f;
    private GameObject go;
    private LevelGenerator lg ;
    //public Camera cam;
    void Start(){
        go = GameObject.Find("LevelGenerator");
        lg = go.GetComponent<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        //se il personaggio si trova ad una certa distanza dall'oggetto esso si distrugge
        if(lg.cam.transform.position.y < this.transform.position.y - DISTANZA_PER_DISTRUGGERE){
            Destroy(this.gameObject);
        }
    }
}
