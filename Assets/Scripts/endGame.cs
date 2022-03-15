using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{
    [SerializeField] int startingLives = 3;
    public void ending()
    {
        TrackLives lifeTracker = FindObjectOfType<TrackLives>();
        lifeTracker.numLives = startingLives;
        SceneManager.LoadScene(0);
    }
}