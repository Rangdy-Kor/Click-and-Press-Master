using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Script
{
    public class ManagePool : MonoBehaviour
    {
        public static ManagePool PoolInstance;
        public IObjectPool<GameObject> Pool { get; set; }

        public int defaultCapacity = 10;
        public int maxPoolSize = 15;

        public GameObject redCube;
        public GameObject greenCube;
        public GameObject blueCube;
        
        private float _posRandom;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

            for (int i = 0; i < defaultCapacity; i++)
            {
                CubeAnimations cube = CreatePooledItem().GetComponent<CubeAnimations>();
                cube.Pool = Pool;
            }
        }

        private GameObject CreatePooledItem()
        {
            
            _posRandom = Random.Range(0, 3);
            switch (_posRandom)
            {
                case 0:
                    GameObject redPoolGo = Instantiate(redCube);
                    redPoolGo.GetComponent<CubeAnimations>().Pool = Pool;
                    return redPoolGo;
                case 1:
                    GameObject greenPoolGo = Instantiate(greenCube);
                    greenPoolGo.GetComponent<CubeAnimations>().Pool = Pool;
                    return greenPoolGo;
                case 2:
                    GameObject bluePoolGo = Instantiate(blueCube);
                    bluePoolGo.GetComponent<CubeAnimations>().Pool = Pool;
                    return bluePoolGo;
                default:
                    return null;
            }
        }

        private void OnTakeFromPool(GameObject poolGo)
        {
            poolGo.SetActive(true);
        }

        private void OnReturnedToPool(GameObject poolGo)
        {
            poolGo.SetActive(false);
        }

        private void OnDestroyPoolObject(GameObject poolGo)
        {
            Destroy(poolGo);
        }
    }
}