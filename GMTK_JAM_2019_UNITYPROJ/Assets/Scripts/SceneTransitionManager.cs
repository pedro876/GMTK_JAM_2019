using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] float timeToTransition = 1f;
    [SerializeField] Animator transitionAnimator;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionToScene(sceneName));
    }

    IEnumerator TransitionToScene(string sceneName)
    {
        transitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(timeToTransition);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        StartCoroutine(TransitionToNextScene());
    }

    IEnumerator TransitionToNextScene()
    {
        transitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(timeToTransition);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        StartCoroutine(TransitionToScene(SceneManager.GetActiveScene().name));
    }
}