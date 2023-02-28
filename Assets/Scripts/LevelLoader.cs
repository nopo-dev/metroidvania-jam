using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    [SerializeField] private float transitionTime = 1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Test");
            Debug.Log(PlayerStatus.Instance.HPManager.getCurrentHP());
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("loadnextlevel");
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
        }
        else {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex-1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        Debug.Log("test");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}
