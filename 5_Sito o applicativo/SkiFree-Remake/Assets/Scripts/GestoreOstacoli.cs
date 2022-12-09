using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestoreOstacoli : MonoBehaviour
{
    private const float DISTANZA_PER_DISTRUGGERE = 5f;
    private GameObject gameObjectLG;
    private LevelGenerator levelGenerator ;
    private PolygonCollider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        //Game Object del LevelGenerator
        gameObjectLG = GameObject.Find("LevelGenerator");

        levelGenerator = gameObjectLG.GetComponent<LevelGenerator>();

        collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //se il personaggio si trova ad una certa distanza dall'oggetto esso si distrugge
        if(levelGenerator.cam.transform.position.y < this.transform.position.y - DISTANZA_PER_DISTRUGGERE){
            Destroy(this.gameObject);
        }

        //se viene premuto il tasto sinistro e quindi si trova in volo
        if (Input.GetMouseButtonDown(0)){
                collider.isTrigger = true;
                StartCoroutine(salto());
        }
    }

    IEnumerator salto(){
        yield return new WaitForSeconds(PlayerMovement.JUMP_TIME);
        collider.isTrigger = false;
    }
}
