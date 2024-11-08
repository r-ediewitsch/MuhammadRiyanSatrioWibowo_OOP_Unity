using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    void Awake()
    {
        if (animator == null)
        {
            GameObject transitionImage = GameObject.Find("SceneTransition");
            if (transitionImage != null)
            {
                animator = transitionImage.GetComponent<Animator>();
                DontDestroyOnLoad(animator.gameObject);
            }
            else
            {
                Debug.LogError("SceneTransition image with Animator not found!");
            }
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitUntil(() => operation.isDone);

        Player.Instance.transform.position = new(0, -4.5f);
    }

    public void LoadScene(string sceneName)
    {
        animator.SetTrigger("StartTransition");
        StartCoroutine(LoadSceneAsync(sceneName));
        animator.SetTrigger("EndTransition");
    }
}
