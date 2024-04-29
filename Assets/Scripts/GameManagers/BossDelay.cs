using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDelay : MonoBehaviour
{
    [SerializeField] private GameObject DelayScreen;


    
    //https://forum.unity.com/threads/how-can-i-delay-my-code-for-half-second.1268963/
    private void Do_Delay(float s)   // ı want to delay this part.
    {
        StartCoroutine(DelayThenFall(s));
    }

    IEnumerator DelayThenFall(float delay)
    {
        yield return new WaitForSeconds(delay);
        
    }

}
