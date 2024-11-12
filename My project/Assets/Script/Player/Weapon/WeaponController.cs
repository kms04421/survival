using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace MainSSM
{
    public class WeaponController : MonoBehaviour
    {
        public float rotationAngle = 70f; // ȸ���� ����
        public float delay = 0.5f; // ȸ�� ���� (��)

        private bool rotatingLeft = true; // ����, ������ ȸ�� ����
        private int Dam = 100;
        private void Start()
        {
            StartCoroutine(RotateObject());
        }

        private IEnumerator RotateObject()
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
        private void OnTriggerEnter2D(Collider2D collision)
        {
    
            if (!Player.ActionPlayer) return;
            if(collision.gameObject.layer >= 7)
            {
                
                collision.GetComponent<IHitListener>().OnHit(Dam);
            }
        }
       
    }


}