using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private Button loadGameButton;
    //private string sceneName;
    
    private void Start()
    {
        // Call Data Persistence Manager & check if it has data.
        // if no data, disable load game buttton.
        if (!DataPManager.instance.HasGameData())
        {
            loadGameButton.interactable = false;
        }
    }
    public void StartGame()
    {
        // Create new game & initialise game data.
        DataPManager.instance.NewGame();
        // Save game right before loading new scene so the new game data persists.
        DataPManager.instance.SaveGame();
        // Load gameplay scene.
        // this saves the game as well because of OnSceneUnloaded in the DataPManager script.
        SceneManager.LoadSceneAsync("Level1");
        ///sceneName = SceneManager.GetActiveScene().name;
    }
    public void EscapeSecretLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void OnContinueGameClicked()
    {
        Debug.Log("load old game");
        DataPManager.instance.SaveGame();
        // Load the next scene.
        // this will load the game because of OnSceneLoaded in the DataPManager script.
        SceneManager.LoadSceneAsync("Level1");
    }
}
