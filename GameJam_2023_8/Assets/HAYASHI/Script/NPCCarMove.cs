using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCarMove : MonoBehaviour
{
    // �ړI�n�̃I�u�W�F�N�g��z��ŕێ�
    public Transform[] m_Destinations;
    // �Ԃ̈ړ����x
    public float m_CarMoveSpeed = 25f;
    // ���݂̖ړI�n�̃C���f�b�N�X
    private int m_CurrentDestinationIndex = 0; 

    private void Start()
    {
        MoveToDestination(m_CurrentDestinationIndex);
    }

    private void MoveToDestination(int destinationIndex)
    {
        // �ړI�n�������ȃC���f�b�N�X�̏ꍇ�͏I��
        if (destinationIndex >= m_Destinations.Length || destinationIndex < 0)
            return;

        // �ړI�n�ւ̈ړ����J�n
        StartCoroutine(MoveCoroutine(m_Destinations[destinationIndex].position));
    }

    private IEnumerator MoveCoroutine(Vector3 destination)
    {
        //�ԗ����ړI�n�ɓ��B����܂Ń��[�v
        //�ԗ��̌��݈ʒu�ƖړI�n�̋������v�Z��0.05���ȏ�傫���ƃ��[�v����
        while (Vector3.Distance(transform.position, destination) > 0.05f)
        {
            //�ړI�n�̕������v�Z
            Vector3 lookDir = destination - transform.position;
            //�ԗ������������ɉ�]���Ȃ��悤�ɂ���
            //���ꂪ�Ȃ��Ǝԗ��̋������o�O��܂�
            lookDir.y = 0f;
            if (lookDir != Vector3.zero)
            {
                // �ړI�n�̕���������
                transform.rotation = Quaternion.LookRotation(lookDir);
            }

            // �ړI�n�ւ̈ړ�
            transform.position = Vector3.MoveTowards(transform.position, destination, m_CarMoveSpeed * Time.deltaTime);
            yield return null;
        }

        // �ړI�n�ɓ��������玟�̖ړI�n��
        m_CurrentDestinationIndex++;
        if (m_CurrentDestinationIndex >= m_Destinations.Length)
        {
            // �Ō�̖ړI�n�ɓ��B������ŏ��ɖ߂�
            m_CurrentDestinationIndex = 0;
        }

        // ���̖ړI�n�ֈړ�
        MoveToDestination(m_CurrentDestinationIndex);
    }
}