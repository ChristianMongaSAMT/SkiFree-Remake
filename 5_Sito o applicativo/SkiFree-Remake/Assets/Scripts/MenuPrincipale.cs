using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipale : MonoBehaviour
{
    public void Gioca(){
        SceneManager.LoadScene(1);
    }

    public void Esci(){
        Application.Quit();
    }
}
