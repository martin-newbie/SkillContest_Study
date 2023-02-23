using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bezier
{
    public class BezierCurve : MonoBehaviour
    {
        [Range(0f, 1f)] public float t;
        public Transform[] poses;
        public Transform target;

        [Header("Condition")]
        public bool showDepth1;
        public bool showDepth2;
        public bool showDepth3;
        public Vector3 textOffset;

        [Header("Texts")]
        public GameObject abText;
        public GameObject bcText;
        public GameObject cdText;
        public GameObject abbcText;
        public GameObject bccdText;

        [Header("Line")]
        public LineRenderer abLine;
        public LineRenderer bcLine;
        public LineRenderer cdLine;
        public LineRenderer abbcLine;
        public LineRenderer bccdLine;
        public LineRenderer abbcbccdLine;

        void Update()
        {
            Vector3 vecPos = cubicBezierVec(poses[0].position, poses[1].position, poses[2].position, poses[3].position, t);
            Vector3 nextVec = cubicBezierVec(poses[0].position, poses[1].position, poses[2].position, poses[3].position, t + Time.deltaTime);

            var dir = nextVec - vecPos;
            float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            target.rotation = Quaternion.Euler(0, 0, z);

            target.position = vecPos;

            gizmosDraw(poses[0].position, poses[1].position, poses[2].position, poses[3].position, t);
        }

        Vector3 cubicBezierVec(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            var ab = Vector3.Lerp(a, b, t);
            var bc = Vector3.Lerp(b, c, t);
            var cd = Vector3.Lerp(c, d, t);

            var abbc = Vector3.Lerp(ab, bc, t);
            var bccd = Vector3.Lerp(bc, cd, t);

            return Vector3.Lerp(abbc, bccd, t);
        }

        void gizmosDraw(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            abText.SetActive(showDepth1);
            bcText.SetActive(showDepth1);
            cdText.SetActive(showDepth1);

            abLine.gameObject.SetActive(showDepth1);
            bcLine.gameObject.SetActive(showDepth1);
            cdLine.gameObject.SetActive(showDepth1);

            abbcText.SetActive(showDepth2);
            bccdText.SetActive(showDepth2);

            abbcLine.gameObject.SetActive(showDepth2);
            bccdLine.gameObject.SetActive(showDepth2);

            abbcbccdLine.gameObject.SetActive(showDepth3);


            var ab = Vector3.Lerp(a, b, t);
            abLine.SetPositions(new Vector3[2] { a, b });
            abText.transform.position = ab + textOffset;

            var bc = Vector3.Lerp(b, c, t);
            bcLine.SetPositions(new Vector3[2] { b, c });
            bcText.transform.position = bc + textOffset;

            var cd = Vector3.Lerp(c, d, t);
            cdLine.SetPositions(new Vector3[2] { c, d });
            cdText.transform.position = cd + textOffset;

            var abbc = Vector3.Lerp(ab, bc, t);
            abbcLine.SetPositions(new Vector3[2] { ab, bc });
            abbcText.transform.position = abbc + textOffset;

            var bccd = Vector3.Lerp(bc, cd, t);
            bccdLine.SetPositions(new Vector3[2] { bc, cd });
            bccdText.transform.position = bccd + textOffset;

            var abbcbccd = Vector3.Lerp(abbc, bccd, t);
            abbcbccdLine.SetPositions(new Vector3[2] { abbc, bccd });
        }
    }
}