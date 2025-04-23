using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>Controls the loading of an scene</summary>
public class SceneLoader : MonoBehaviour
{
    private bool _isLoaded = false;

    private void Start()
    {
        ///Makes sure the scene is not loaded
        if (SceneManager.sceneCount > 0)
        {
            for (int i=0; i<SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == gameObject.name)
                {
                    _isLoaded = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        ///Loads the scene when player enter to the scene zone
        if (collider.name == "Player")
        {
            if (!_isLoaded)
            {                
                StartCoroutine(LoadScene());
            }           
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        ///Unloads the scene when player exit to the scene zone
        if (collider.name == "Player")
        {
            UnLoadScene();
        }
    }

    ///<summary>Unoads the scene</summary>
    private void UnLoadScene()
    {
        if (_isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            _isLoaded = false;
        }
    }

    ///<summary>Waits for the scene loading/summary>
    private IEnumerator LoadScene()
    {
        AsyncOperation progress = SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);

        while (!progress.isDone)
        {
            yield return null;
        }
        _isLoaded = true;
    }


}
