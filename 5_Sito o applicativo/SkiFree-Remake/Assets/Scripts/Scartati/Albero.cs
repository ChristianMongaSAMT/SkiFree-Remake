using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Albero : MonoBehaviour
{
    private PolygonCollider2D collider;
    void Start(){
        collider = GetComponent<PolygonCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
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
