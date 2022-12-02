using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private const float DISTANZA_PER_DISTRUGGERE = 5f;
    private GameObject go;
    private LevelGenerator lg ;
    private PolygonCollider2D collider;


    void Start(){
        go = GameObject.Find("LevelGenerator");

        lg = go.GetComponent<LevelGenerator>();

        collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //se il personaggio si trova ad una certa distanza dall'oggetto esso si distrugge
        if(lg.cam.transform.position.y < this.transform.position.y - DISTANZA_PER_DISTRUGGERE){
            Destroy(this.gameObject);
        }

        //se viene premuto il tasto sinistro e quindi si trova in volo
        if (Input.GetMouseButtonDown(0)){
                Debug.Log("Tasto SX premuto");
                collider.isTrigger = true;
                StartCoroutine(salto());
        }

        IEnumerator salto(){
            yield return new WaitForSeconds(PlayerMovement.JUMP_TIME);
            collider.isTrigger = false;
        }
    }
}
