using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public static LevelManager LevelManager {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        LevelManager = FindObjectOfType<LevelManager>();;

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }
}
