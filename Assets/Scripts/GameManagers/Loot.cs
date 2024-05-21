using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Loot : MonoBehaviour
{
    private SpriteRenderer render;
    public static Loot instance;
    

    private void Start()
    {
        instance = this;

        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
    }

    //tutorial code from: https://www.youtube.com/watch?v=-7I0slJyi8g&ab_channel=Chris%27Tutorials
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (render.enabled && other.gameObject.CompareTag("Player"))
        {
            SceneControl.instance.WinGame();
        }

    }

    public void ShowLoot()
    {
        render.enabled = true;
    }

}