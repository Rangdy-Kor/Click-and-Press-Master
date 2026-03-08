using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class ManageScene : MonoBehaviour
    {
        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("InGame");
        }

        public void Quit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
