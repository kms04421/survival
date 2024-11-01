using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float rotationAngle = 70f; // 회전할 각도
    public float delay = 0.5f; // 회전 간격 (초)

    private bool rotatingLeft = true; // 왼쪽, 오른쪽 회전 방향

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

                // 방향 전환
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
