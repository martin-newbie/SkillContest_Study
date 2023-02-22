using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boundery
{
    public class Player : MonoBehaviour
    {
        public Manager bounderyManager;
        public float moveSpeed;

        void Update()
        {
            int dirX = 0, dirY = 0;
            
            if (Input.GetKey(KeyCode.RightArrow))
            {
                dirX = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                dirX = -1;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                dirY = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                dirY = -1;
            }

            Vector2 dir = new Vector2(dirX, dirY) * Time.deltaTime * moveSpeed;


            if(bounderyManager.IsOutX(transform.position.x + dir.x, 0.45f))
            {
                dir.x = 0;
            }

            if(bounderyManager.IsOutY(transform.position.y + dir.y, 0.7f))
            {
                dir.y = 0;
            }


            transform.position += (Vector3)dir;

        }
    }
}
