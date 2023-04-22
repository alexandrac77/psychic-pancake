using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CandyCollected : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int totalCandy = 0;
    private int candyCollected = 0;
    private TextMeshProUGUI candyCollectedText;

    private void Awake()
    {
        candyCollectedText = this.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        
    }
    
    public void LoadData(GameData data)
    {
        foreach(KeyValuePair<string, bool> pair in data.candyCollected)
        {
            if (pair.Value)
            {
                candyCollected++;
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        // keep empty
    }

    void Update()
    {
       // candyCollectedText.text = candyCollected + "/" +totalCandy;
    }
}
