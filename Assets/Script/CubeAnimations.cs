using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Script
{
    public class CubeAnimations:  MonoBehaviour
    {
        private bool _ifMainMenu;
        private bool _ifVisible;
        public IObjectPool<GameObject> Pool { get;  set; }
        public float cubeAnimateSpeed = 1f;
        
        private void OnEnable()
        {
            cubeAnimateSpeed = Random.Range(0.5f, 4f);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainMenu")
            {
                _ifMainMenu = true;
            }
            else
            {
                _ifMainMenu = false;
            }
        }

        private void OnBecameVisible()
        {
            _ifVisible = true;
        }

        private void OnBecameInvisible()
        {
            _ifVisible = false;
        }

        void Update()
        {
            if (_ifMainMenu && _ifVisible)
            {
                transform.Translate(Time.deltaTime * cubeAnimateSpeed * Vector3.down);
            }
            else
            {
                Pool.Release(gameObject);
            }
        }
    }
}