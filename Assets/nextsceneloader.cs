using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HQ
{
    public class NextSceneLoader : MonoBehaviour
    {
        [SerializeField]
        private string sceneName = "GamePlay"; // Default to "GamePlay" but editable in the Inspector

        private void OnEnable()
        {
            LoadNextScene();
        }

        public void LoadNextScene()
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
            else
            {
                Debug.LogWarning("Scene name is empty! Please set a scene name in the Inspector.");
            }
        }
    }
}
