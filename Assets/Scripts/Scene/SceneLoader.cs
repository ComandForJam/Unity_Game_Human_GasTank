using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int indexScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Scr_BaseHero>() != null)
        {
            SceneManager.LoadScene(indexScene);
        }
    }
}
