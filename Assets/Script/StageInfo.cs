using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    [System.Serializable]
    public class StageInfo : MonoBehaviour
    {
        public string stageText;
        public GameObject stagePrefab;
    }
}