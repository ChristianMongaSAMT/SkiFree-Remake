using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
   public static bool GiocoInPausa = false;

   public GameObject menuPausa;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GiocoInPausa){
                Resume();
            }else{
                Pause();
            }
        }
    }
    public void Resume(){
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        GiocoInPausa = false;
    }

    public void Pause(){
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        GiocoInPausa = true;
    }

    public void Esci(){
        Debug.Log("QUIT");
        Application.Quit();
    }
}
