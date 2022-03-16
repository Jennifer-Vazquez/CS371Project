using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
        public void Load()
        {
            TrackLives LifeTracker = FindObjectOfType<TrackLives>();
            int sceneNumber = LifeTracker.levelNumber;
            SceneManager.LoadScene(sceneNumber);
        }
}
