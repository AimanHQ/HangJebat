using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HQ
{
    public class NewBehaviourScript : MonoBehaviour
    {
    public Button switchSceneButton; // Reference to the UI button
    public string nextSceneName;     // Name of the next scene to load

    void Start()
    {
        // Add a listener to the button to trigger the scene switch
        switchSceneButton.onClick.AddListener(SwitchScene);
    }

    void SwitchScene()
    {
        // Load the next scene by its name
        SceneManager.LoadScene("CUTSCENE1");
    }
    }
}
