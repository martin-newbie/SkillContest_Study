using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingRange
{
    public class Bullet : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public bool isRun = true;

        void Start()
        {
            Invoke("Destroy", 5f);
        }

        void Update()
        {
            if (!isRun) return;

            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }

        void Destroy()
        {
            Destroy(gameObject);
        }
    }

}