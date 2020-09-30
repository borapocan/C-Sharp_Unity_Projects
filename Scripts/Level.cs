using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    //parameters
    [SerializeField] int breakableBlocks;

    //cached references
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }


    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlocksDestroyed()
    {
        breakableBlocks--;

        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
