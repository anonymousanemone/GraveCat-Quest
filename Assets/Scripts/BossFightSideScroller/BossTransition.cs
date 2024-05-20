using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossTransition : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Transition();
        }
    }

    private void Transition()
    {
        StartCoroutine(DelayThenFade());
    }

    IEnumerator DelayThenFade()
    {
        Animator anim = blackScreen.GetComponent<Animator>();
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;

        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(curSceneIndex + 1);
    }
}


