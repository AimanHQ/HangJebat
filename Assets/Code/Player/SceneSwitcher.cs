using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace HQ
{
    public class SceneSwitcher : MonoBehaviour
    {
    [SerializeField]
    private string sceneName; // The name of the scene you want to switch to

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }
    }
}
