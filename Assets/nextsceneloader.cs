using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HQ
{
    public class Nextsceneloader : MonoBehaviour
    {
        public void LoadNextScene()
        {
            SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
        }
    }
}
