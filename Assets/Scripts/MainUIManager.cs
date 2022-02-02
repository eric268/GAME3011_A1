using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainUIManager : MonoBehaviour
{
    public void OnPlayingMiningButtonPressed()
    {
        SceneManager.LoadScene("MiningMiniGame");
    }

    public void OnQuitButtonPressed()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
