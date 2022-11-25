using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Albero : MonoBehaviour
{
    private PolygonCollider2D bc;
    void Start(){
        bc = GetComponent<PolygonCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
                Debug.Log("Tasto SX premuto");
                bc.isTrigger = true;
                //bc.transform.position = new Vector3(bc.transform.position.x, bc.transform.position.y, -0.1f);
                StartCoroutine(salto());
        }
    }

     IEnumerator salto(){
        yield return new WaitForSeconds(2f);
        bc.isTrigger = false;
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
    }
}
