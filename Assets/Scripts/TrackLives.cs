using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackLives : MonoBehaviour
{
    [SerializeField] int LoseLifeIndex = 41;
    [SerializeField] int LoseGasIndex = 43;
    public static TrackLives instance;
    public int numLives = 3;
    public int levelNumber;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        if ((currentscene != LoseLifeIndex) &&  (currentscene != LoseGasIndex)) // this value should be the scene index for LoseLife
        levelNumber = currentscene;
    }
}
