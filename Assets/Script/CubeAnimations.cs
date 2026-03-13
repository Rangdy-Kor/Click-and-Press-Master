using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Script
{
    public class CubeAnimations:  MonoBehaviour
    {
        public IObjectPool<GameObject> Pool { get;  set; }
        public float cubeAnimateSpeed = 1f;

        void FixedUpdate()
        {
            if (transform.position.y > -15)
            {
                transform.Translate( Vector3.down * cubeAnimateSpeed);
            }
            else
            {
                Pool.Release(gameObject);
            }
        }
    }
}