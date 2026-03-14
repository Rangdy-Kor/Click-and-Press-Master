using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class ManageStage : MonoBehaviour
    {
        private readonly string _secondClockObjectName = "SecondClock";
        public GameObject secondClockObject;
        private TextMeshProUGUI _secondClockText;

        private float _secondClockNumber;
        public float secondClockLimit = 179f;

        private readonly Dictionary<string, Type> _missionTypes = new Dictionary<string, Type>()
        {
            { "Stage1", typeof(Stage1) },
            { "Stage2", typeof(Stage2) },
            { "Stage3", typeof(Stage3) },
            { "Stage4", typeof(Stage4) },
            { "Stage5", typeof(Stage5) },
            { "Stage6", typeof(Stage6) },
            { "Stage7", typeof(Stage7) },
        };

        public StageInfo[] allStageInfos;
        private readonly string _stageTitleObjectName = "StageTitle";
        public GameObject stageTitleObject;
        private TextMeshProUGUI _stageTitleText;
        private int _currentStage;
        private GameObject _spawnedObject;
        private IStage _currentStageInstance;

        public AudioSource stageClearAudio;

        private void Awake()
        {
            _secondClockNumber = 0f;
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
            if (scene.name == "InGame")
            {
                secondClockObject = GameObject.Find(_secondClockObjectName);
                if (secondClockObject != null)
                {
                    _secondClockText = secondClockObject.GetComponent<TextMeshProUGUI>();
                    _secondClockText.text = "";
                }

                stageTitleObject = GameObject.Find(_stageTitleObjectName);
                if (stageTitleObject != null)
                {
                    _stageTitleText = stageTitleObject.GetComponent<TextMeshProUGUI>();
                    _stageTitleText.text = "";
                }
            }
        }

        void Update()
        {
            if (ManageGame.GameInstance.isGameStart && !ManageGame.GameInstance.isGameFinish && !ManageGame.GameInstance.isGameClear)
            {
                if (_secondClockNumber > secondClockLimit)
                {
                    ManageGame.GameInstance.isGameFinish = true;
                }
                
                var minutes = Mathf.FloorToInt(_secondClockNumber / 60);
                float seconds = _secondClockNumber % 60;
                _secondClockText.text = $"{minutes:00}:{seconds:00.00}";

                _secondClockNumber += Time.deltaTime;
                
                if (_currentStageInstance != null && _currentStageInstance.Check())
                {
                    _currentStageInstance = null;
                    _currentStage++;
                    stageClearAudio.PlayOneShot(stageClearAudio.clip);
                    NextStage();
                }
            }
            else if (ManageGame.GameInstance.isGameStart && ManageGame.GameInstance.isGameFinish && !ManageGame.GameInstance.isGameClear)
            {
                _secondClockText.text = "Game Over";
            }
            else if (ManageGame.GameInstance.isGameStart && ManageGame.GameInstance.isGameFinish && ManageGame.GameInstance.isGameClear)
            {
                _secondClockText.text = "Game Clear";
            }
        }

        public void NextStage()
        {
            if (_currentStage < allStageInfos.Length)
            {
                if (_spawnedObject != null) Destroy(_spawnedObject);

                StageInfo current = allStageInfos[_currentStage];
                _stageTitleText.text = current.stageText;

                if (current.stagePrefab != null)
                    _spawnedObject = Instantiate(current.stagePrefab);
                    
                string stageKey = "Stage" + (_currentStage + 1);
                if (!_missionTypes.TryGetValue(stageKey, out var t)) return;
                
                _currentStageInstance = (IStage)Activator.CreateInstance(t);
                _currentStageInstance.Init(_spawnedObject);
            }
            else
            {
                ManageGame.GameInstance.isGameClear = true;
            }
        }
    }
}