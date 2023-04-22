using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // load secret level after 2 seconds when player collides with pipe.
            Invoke("HiddenLevel", 2f);
        }
    }

    private void HiddenLevel()
    {
        SceneManager.LoadScene("SecretLevel");
    }

}
