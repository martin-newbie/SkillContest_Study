using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boundery
{
    public class Manager : MonoBehaviour
    {
        public Vector2 center;
        public Vector2 size;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(center, size * 2);
        }

        public bool IsOutX(float x, float xSize)
        {
            return x + xSize > center.x + size.x || x - xSize < center.x - size.x;
        }

        public bool IsOutY(float y, float ySize)
        {
            return y + ySize > center.y + size.y || y - ySize < center.y - size.y;
        }
    }
}
