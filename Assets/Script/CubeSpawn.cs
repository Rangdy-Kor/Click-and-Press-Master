using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class CubeSpawn : MonoBehaviour
    {
        public float cubePosMoveSpeed = 1f;
        public int spawnRandomValue;
        public int spawnRandomValueRange;
        public int spawnRandomGenerateRange = 1000;

        void Update()
        {
            if (transform.position.x < 23)
            {
                transform.position += cubePosMoveSpeed * Time.deltaTime * Vector3.right;
            }
            
            if (transform.position.x > 23)
            {
                transform.position += cubePosMoveSpeed * Time.deltaTime * Vector3.left;
            }
        }

        void FixedUpdate()
        {
            spawnRandomValue =  Random.Range(1, spawnRandomGenerateRange + 1);
            if (spawnRandomValue <= spawnRandomValueRange)
            {
                var cubeGo = ManagePool.PoolInstance.Pool.Get();
                cubeGo.transform.position = transform.position;
            }
        }
        
    }
}