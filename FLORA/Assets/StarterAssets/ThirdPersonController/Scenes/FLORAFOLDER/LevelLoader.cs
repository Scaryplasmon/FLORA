using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float transitionTime = 1f;
    public float introTime = 6f;

    public Animator transition;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= introTime){

            LoadNextLevel();
        }
        
    }

    public void LoadNextLevel(){

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        Debug.Log("I'm gonna go Ahead");

    }

    IEnumerator LoadLevel(int levelIndex){

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
