using System.Collections;
using UnityEngine;
namespace MainSSM
{
    public enum WaponType
    {
        Melee,   // 근거리 무기
        Ranged   // 원거리 무기
    }
    public class WeaponController : MonoBehaviour
    {
        public float rotationSpeed = 70f;
      

        private bool isAttacking = false; //공격 중인지 여부
        private int Dam = 100;
        private Quaternion targetRotation;
        private Vector3 weapoonPos; // 무기 기본위치
        private Vector2 vector2Zero; //상태가 멈췄는지 체크용

        private Player player;

        public WaponType currentType;

        private void Start()
        {
            currentType = WaponType.Melee;
            weapoonPos = new Vector3(0, -0.2f, 0);
            player = transform.parent.GetComponent<Player>();
            vector2Zero = new Vector2(0, 0);
            targetRotation = Quaternion.Euler(0, 360, 0) * transform.rotation;
            
        }

        public void Attack()
        {
            switch (currentType)
            {
                case WaponType.Melee:
                    if (isAttacking) return;
                    StartCoroutine(MeleeAttack());
                    break;
                case WaponType.Ranged:
                    RangedAttack();
                    break;
            }
        }


        private IEnumerator MeleeAttack()
        {
            isAttacking = true;
          
            while (player.rote.x != 0 && player.rote.y != 0)
            {
                transform.Rotate(new Vector3(0, 0, -player.playerData.APS*100 * Time.deltaTime));
                yield return null; // 매 프레임마다 회전하도록 함
            }

            isAttacking = false;
            ResetWeaponPosition();
        }
        public void ResetWeaponPosition()
        {
            if (player.rote == vector2Zero)
            {
                if (player.rote.x == 0 && player.rote.y == 0)
                {
                    transform.position = player.transform.position + weapoonPos;
                    if (player.spriteRenderer.flipX)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }


                }
                return;
            }
        }
        public void RangedAttack() // 무기 회전 이동
        {
            ResetWeaponPosition();

            Vector3 direction = new Vector3(player.rote.x, player.rote.y, 0).normalized;

            transform.position = player.transform.position + direction * 0.75f;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void ExWeapoonMove() // 회전시 대상 포지션을 본인으로 하여 무기에 특이한 움직임 구현
        {
            Vector3 direction = new Vector3(player.rote.x, player.rote.y, 0).normalized;

            transform.position = transform.position + direction * 0.75f;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

    }


}
