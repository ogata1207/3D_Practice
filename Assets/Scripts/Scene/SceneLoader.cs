using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private static  AsyncOperation async;

    public static IEnumerator LoadNextScene (string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        while(!async.isDone)
        {
            yield return null;
        }
        yield return null;
    }
}
