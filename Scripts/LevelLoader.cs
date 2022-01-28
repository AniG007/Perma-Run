using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Slider progressBar;
    public void LoadNextLevel(string SceneName)
    {
        StartCoroutine(LoadLevel(SceneName));
    }

    IEnumerator LoadLevel(string SceneName)
    {
        //Loading a level asynchronously
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneName);
        loadOperation.allowSceneActivation = false;

        //Done is true only if progress is 100, which would happen if allowSceneActivation is true. In this can since we set it to false, we check for 90% progress//ref:https://docs.unity3d.com/ScriptReference/AsyncOperation-progress.html
        while (!loadOperation.isDone)
        {
            //keeping prog to one decimal place. multiply by 100 for percentage
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            progressBar.value = progress;
            if (loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
