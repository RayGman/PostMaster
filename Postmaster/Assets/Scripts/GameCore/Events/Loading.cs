using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image loadProgressImage;
    [SerializeField] private int loadSceneID;

    private void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    private IEnumerator AsyncLoad()
    {
        AsyncOperation operationLoadScene = SceneManager.LoadSceneAsync(loadSceneID);

        while (!operationLoadScene.isDone)
        {
            float progress = operationLoadScene.progress / 0.9f;
            loadProgressImage.fillAmount = progress;          
            yield return null;
        }
    }
}
