using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScenes : MonoBehaviour
{
    [SerializeField] private Image blackScreen;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(LoadScene("New Scene"));
        }
    }

    IEnumerator LoadScene(string _sceneCaption)
    {
        while(blackScreen.color.a<1)
        {
            var col = blackScreen.color;
            col.a += 0.01f;
            blackScreen.color = col;
            yield return new WaitForFixedUpdate();
        }
        SceneManager.LoadScene(_sceneCaption);
    }

    public void Click()
    {
        print("Click");
    }

}
