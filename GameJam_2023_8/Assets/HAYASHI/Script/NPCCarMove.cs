using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HAYASHI.Script
{
    [RequireComponent(typeof(NPCCarMove))]
    public class NPCCarMove : MonoBehaviour
    {
        // 目的地のオブジェクトを配列で保持
        public Transform[] m_Destinations;
        // 車の移動速度
        public float m_CarMoveSpeed = 25f;
        // 現在の目的地のインデックス
        private int m_CurrentDestinationIndex = 0;

        private void Start()
        {
            MoveToDestination(m_CurrentDestinationIndex);
        }

        private void MoveToDestination(int destinationIndex)
        {
            // 目的地が無効なインデックスの場合は終了
            if (destinationIndex >= m_Destinations.Length || destinationIndex < 0)
                return;

            // 目的地への移動を開始
            StartCoroutine(MoveCoroutine(m_Destinations[destinationIndex].position));
        }

        private IEnumerator MoveCoroutine(Vector3 destination)
        {
            //車両が目的地に到達するまでループ
            //車両の現在位置と目的地の距離を計算し0.05ｆ以上大きいとループする
            while (Vector3.Distance(transform.position, destination) > 0.05f)
            {
                //目的地の方向を計算
                Vector3 lookDir = destination - transform.position;
                //車両が垂直方向に回転しないようにする
                //これがないと車両の挙動がバグります
                lookDir.y = 0f;
                if (lookDir != Vector3.zero)
                {
                    // 目的地の方向を向く
                    transform.rotation = Quaternion.LookRotation(lookDir);
                }

                // 目的地への移動
                transform.position = Vector3.MoveTowards(transform.position, destination, m_CarMoveSpeed * Time.deltaTime);
                yield return null;
            }

            // 目的地に到着したら次の目的地へ
            m_CurrentDestinationIndex++;
            if (m_CurrentDestinationIndex >= m_Destinations.Length)
            {
                // 最後の目的地に到達したら最初に戻る
                m_CurrentDestinationIndex = 0;
            }

            // 次の目的地へ移動
            MoveToDestination(m_CurrentDestinationIndex);
        }
    }
}