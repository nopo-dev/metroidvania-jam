using UnityEngine;

public class SceneSave : MonoBehaviour
{
    void Start(){
        //in case we ever need to save other objects between scenes
        for (int i = 0; i < Object.FindObjectsOfType<SceneSave>().Length; i++)
        {
            if (Object.FindObjectsOfType<SceneSave>()[i] != this)
            {
                if (Object.FindObjectsOfType<SceneSave>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }   

}
