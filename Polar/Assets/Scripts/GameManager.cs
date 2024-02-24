using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int numOfHearts;
    public int numOfFish;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetHearts()
    {
        numOfHearts = 3;
    }
    public void Resetfish()
    {
        numOfFish = 0;
    }
}
