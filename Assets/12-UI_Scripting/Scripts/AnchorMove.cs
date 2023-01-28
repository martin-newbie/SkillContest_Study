using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIMove
{
    public class AnchorMove : MonoBehaviour
    {
        public RectTransform window;

        int state = 0;
        bool isMove = true;

        public void MoveWindow()
        {
            if (!isMove) return;

            StartCoroutine(MoveRoutine(0.5f));
        }

        IEnumerator MoveRoutine(float duration)
        {
            isMove = false;

            float targetX = state == 0 ? 1000f : 0f;
            state = state == 0 ? 1 : 0;

            Vector2 originPos = window.anchoredPosition;
            Vector2 targetPos = new Vector2(targetX, window.anchoredPosition.y);

            float timer = 0f;
            while (timer <= duration)
            {
                window.anchoredPosition = Vector2.Lerp(originPos, targetPos, ease(timer / duration));

                timer += Time.deltaTime;
                yield return null;
            }

            isMove = true;
            yield break;

            float ease(float x)
            {
                return 1f - Mathf.Pow(1f - x, 5);
            }
        }
    }
}
