using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int game;
    public Vector3 playerPos;
    public SerialisableDictionary<string, bool> candyCollected;


    // Constructor values used when the game starts with no data to load.
    public GameData()
    {
        // Initialise to zero.
        playerPos = Vector3.zero;
        //candyCollected = new SerialisableDictionary<string, bool>();
    }
}
