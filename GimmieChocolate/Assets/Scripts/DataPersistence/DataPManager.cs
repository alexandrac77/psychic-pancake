using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPManager : MonoBehaviour
{
    // file name that data is saved to. specified in unity inspector.
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPManager instance { get; private set; } // get publically & modify privately

    private void Awake()
    {
        // Log error as there should only be one data persistence manager per scene.
        if(instance!= null)
        {
            Debug.Log("more than one data persistence manager in scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        // Pass in gameObject that this script is attached to so it won't be destroyed.
        DontDestroyOnLoad(this.gameObject);

        // Needs to exist when OnSceneLoaded is called.
        // Application.persistentDataPath gives the OS standard directory for persisting data in a Unity project.
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    // Need these methods to be able to subscribe to OnSceneLoaded.
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    // Lifecycle methods. Subscribe to call them.
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ///Debug.Log("OnSceneLoaded called.");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        // Initialise game data to be new game data object.
        this.gameData = new GameData(); 
    }
    public void LoadGame()
    {
        // Load saved data from a file using data handler.
        // returns null if it doesn't exist.
        this.gameData = dataHandler.Load(); 

        // If no game can be loaded, initialise new game.
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Need to start a new game before data can be loaded. ");
            //NewGame();
            return;
        }

        // Loop through data persistence objects in list & call LoadData, passing
        // them the game data.
        // Push loaded data to scripts that need it.
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        // If there is no data to save, log warning.
        if(this.gameData == null)
        {
            Debug.LogWarning("No data could be found. A new game must be started to save data.");
        }
        // Pass data to other scripts so it can be updated.
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            // pass game data by reference.
            dataPersistenceObj.SaveData(ref gameData);
        }
        // Save data to a file using data handler.
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        // Save game each time app quits.
        SaveGame();
    }
    
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // Find all scripts that implement IDataPersistence interface
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        // return new list & pass in result of script call to initialise the list.
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}
