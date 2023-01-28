using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingRange
{
    public class RotateBullet : MonoBehaviour
    {

        Vector3 centerPos;
        Vector2 dir;
        float moveSpd;
        float rotateSpd;
        float angle;
        float radius;

        public void InitBullet(Vector3 _startPos, Vector2 _dir, float _moveSpd, float _rotateSpd, float _startAngle, float _radius)
        {
            centerPos = _startPos;
            dir = _dir;
            moveSpd = _moveSpd;
            rotateSpd = _rotateSpd;
            angle = _startAngle;
            radius = _radius;
        }

        private void Update()
        {
            centerPos += (Vector3)dir * Time.deltaTime * moveSpd;
            angle += rotateSpd * Time.deltaTime;

            Vector3 pos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
            pos += centerPos;

            transform.position = pos;
        }
    }
}
