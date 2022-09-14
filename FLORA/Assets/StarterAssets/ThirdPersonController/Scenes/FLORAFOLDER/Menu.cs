using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayButton(){
        Debug.Log("Restart");
        SceneManager.LoadScene("ComicStrip2");
        gameObject.SetActive(false);
        Debug.Log("Restarted");
    }

    public void ExitButton(){
        Application.Quit();
    }
}
