using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolling
{
    public class ScrollMap : MonoBehaviour
    {
        public Transform[] maps;
        public float moveSpeed;

        float dist = 12.8f;

        private void Update()
        {
            foreach (var item in maps)
            {
                item.Translate(Vector3.down * Time.deltaTime * moveSpeed);

                if(item.position.y < -dist)
                {
                    var pos = item.position;
                    pos.y += dist * 2f;
                    item.position = pos;
                }
            }
        }
    }
}
