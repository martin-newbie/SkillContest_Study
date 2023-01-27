using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lerp
{
    public class Player : MonoBehaviour
    {
        public Transform model;
        public float moveSpeed = 5f;

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            Vector3 nor = new Vector3(x, y, 0f).normalized;
            transform.Translate(nor * moveSpeed * Time.deltaTime);

            if (nor != Vector3.zero)
            {
                float z = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;
                Quaternion lookRot = Quaternion.Euler(0, 0, z + 90f);
                model.rotation = Quaternion.Lerp(model.rotation, lookRot, Time.deltaTime * moveSpeed);
            }
        }

    }
}
