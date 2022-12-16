using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public bool GiocoInPausa = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GiocoInPausa){
                Riprendi();
            }else{
                Pausa();
            }
        }
    }
    public void Riprendi(){
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        GiocoInPausa = false;
    }

    public void Pausa(){
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        GiocoInPausa = true;
    }

    public void TornaAlMenu(){
        SceneManager.LoadScene(0);
    }
}
