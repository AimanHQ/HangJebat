using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HQ
{
    public class nextsceneloader : MonoBehaviour
    {
        void OnEnable()
        {
            SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
        }
    }
}
