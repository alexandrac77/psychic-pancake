using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INTERFACE
public interface IDataPersistence
{
    // Reading data so we dont need ref.
    void LoadData(GameData data);

    // Get reference so data can be modified.
    void SaveData(ref GameData data);
}
