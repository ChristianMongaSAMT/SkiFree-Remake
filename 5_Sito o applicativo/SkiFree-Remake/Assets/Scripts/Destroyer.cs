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
        
        if(lg.cam.transform.position.y < this.transform.position.y - DISTANZA_PER_DISTRUGGERE){
            Debug.Log("BOOOOOOOOOOOOOOOOOOOOOOM");
            Destroy(this.gameObject);
        }
    }
}
