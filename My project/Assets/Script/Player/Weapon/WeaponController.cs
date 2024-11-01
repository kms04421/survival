using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float rotationAngle = 70f; // ȸ���� ����
    public float delay = 0.5f; // ȸ�� ���� (��)

    private bool rotatingLeft = true; // ����, ������ ȸ�� ����

    private void Start()
    {
        StartCoroutine(RotateObject());
    }

    IEnumerator RotateObject()
    {
        while (true)
        {
            if (Player.ActionPlayer)
            {
                float targetAngle = rotatingLeft ? rotationAngle : -rotationAngle;
                transform.rotation = Quaternion.Euler(0, 0, targetAngle);

                // ���� ��ȯ
                rotatingLeft = !rotatingLeft;

            }
            else
            {            
                transform.rotation = Quaternion.Euler(0, 0, 70);
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
