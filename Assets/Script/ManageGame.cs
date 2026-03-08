using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Serialization;

namespace Script
{
    public class ManageGame : MonoBehaviour
    {
        public static ManageGame Instance;
        
        private readonly string _coundDownObjectName = "CountDown";
        public GameObject countDownObject;
        private TextMeshProUGUI _countDownText;

        public bool isGameClear = false;
        public bool isGameFinish = false;
        public bool isGameStart = false;
        private int _countDownNumber;
        public int countDownTime = 3;

        private readonly WaitForSeconds _waitOneSecond = new WaitForSeconds(1.0f);

        public AudioSource backgroundAudio;
        public AudioSource countDownAudio;
        public AudioSource gameStartAudio;
        
        void Awake()
        {
            Instance = this;
            
            var objs = FindObjectsByType<ManageGame>(FindObjectsSortMode.None);
            if (objs.Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Init")
            {
                Instance.GetComponent<ManageScene>().MainMenu();
            }
            
            if (scene.name == "InGame")
            {
                countDownObject = GameObject.Find(_coundDownObjectName); 
                if (countDownObject != null)
                {
                    _countDownText = countDownObject.GetComponent<TextMeshProUGUI>();
                    _countDownText.text = "";
                    StartCoroutine(CountDownStart());
                }
            }
        }

        IEnumerator CountDownStart()
        {
            if (countDownObject == null || _countDownText == null)
            {
                Debug.LogError("UI 오브젝트가 연결되지 않았습니다!");
                yield break;
            }

            _countDownNumber = countDownTime;
            yield return new WaitForSeconds(0.5f);

            while (_countDownNumber >= 1)
            {
                _countDownText.text = _countDownNumber.ToString();
                countDownAudio.PlayOneShot(countDownAudio.clip);
                yield return _waitOneSecond;
                _countDownNumber--;
            }

            _countDownText.text = "Start!";
            gameStartAudio.PlayOneShot(gameStartAudio.clip);
            yield return new WaitForSeconds(0.25f);
            
            GameStart();
            countDownObject.SetActive(false);
        }

        public void GameStart()
        {
            isGameClear = false;
            isGameFinish = false;
            isGameStart = true;
            backgroundAudio.PlayOneShot(backgroundAudio.clip);
            
            ManageStage manageStage = gameObject.GetComponent<ManageStage>();
            manageStage.NextStage();
        }
    }
}
