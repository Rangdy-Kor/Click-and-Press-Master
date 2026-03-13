using UnityEngine;
using System.Collections;

namespace Script
{
    public class CubeSpawn : MonoBehaviour
    {
        public float cubePosMoveSpeed = 5f;
        public int spawnRandomTime;
        public int spawnRandomTimeRangeMin;
        public int spawnRandomTimeRangeMax;
        private WaitForSeconds _waitForSeconds;
        
        [SerializeField] private int currentSpawnDelay;

        void Start()
        {
            StartCoroutine(SpawnCube());
        }
        
        void FixedUpdate()
        {
            if (transform.position.x is > 17 or < -17)
            {
                cubePosMoveSpeed *= -1;
            }
            
            transform.position +=  Time.fixedDeltaTime * cubePosMoveSpeed * Vector3.right;
        }

        IEnumerator SpawnCube()
        {
            
            spawnRandomTime =  Random.Range(spawnRandomTimeRangeMin, spawnRandomTimeRangeMax + 1);
            _waitForSeconds = new WaitForSeconds(spawnRandomTime);

            yield return _waitForSeconds;
            
            var cubeGo = ManagePool.PoolInstance.Pool.Get();
            cubeGo.transform.position = transform.position;

            StartCoroutine(SpawnCube());
        }
    }
}