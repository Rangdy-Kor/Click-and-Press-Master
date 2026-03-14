using UnityEngine;
using UnityEngine.Pool;

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
                transform.position += cubeAnimateSpeed * Vector3.down;
            }
            else
            {
                Pool.Release(gameObject);
            }
        }
    }
}