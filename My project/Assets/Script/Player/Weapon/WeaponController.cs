using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Reflection;
using UnityEngine;
namespace MainSSM
{
   
    public class WeaponController : MonoBehaviour
    {
        private bool isAttacking = false; //공격 중인지 여부
        private int Dam = 100;
        private Quaternion targetRotation;
        private Vector3 weapoonPos; // 무기 기본위치
        private Vector2 vector2Zero; //상태가 멈췄는지 체크용
        float speed = 0;
        private Player player;
        private bool canFire = true; // 발사 가능 여부
        private WaitForSeconds fireDelay; // 연사 속도
        public WeaponType currentType;
        public List<GameObject> equippedWeaponList;
        private void Start()
        {
            currentType = WeaponType.Melee;
            weapoonPos = new Vector3(0, -0.2f, 0);
            player = transform.parent.GetComponent<Player>();
            vector2Zero = new Vector2(0, 0);
            targetRotation = Quaternion.Euler(0, 360, 0) * transform.rotation;
            fireDelay = new WaitForSeconds(5.5f - player.playerData.AdditionalAttackSpeed);
            //5 -(player.playerData.AdditionalAttackSpeed + player.playerData.BaseAttackSpeed)
        }

        public void Attack()
        {
            switch (currentType)
            {
                case WeaponType.Melee:
                    if (isAttacking) return;
                    StartCoroutine(MeleeAttack());
                    break;
                case WeaponType.Ranged:

                    RangedAttack();
                    break;
                case WeaponType.ExMelee:
                    if (isAttacking) return;
                    StartCoroutine(MeleeAttack());
                    break;
            }
        }


        private IEnumerator MeleeAttack()
        {
            isAttacking = true;
         
            speed = player.playerData.AdditionalAttackSpeed + player.playerData.BaseAttackSpeed;
            while (player.rote.x != 0 && player.rote.y != 0)
            {
                transform.Rotate(new Vector3(0, 0, -speed * 100 * Time.deltaTime));
                yield return null; // 매 프레임마다 회전하도록 함
                
            }
            isAttacking = false;
            ResetWeaponPosition(); 
        }
        public bool ResetWeaponPosition()
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
                return false;
            }
            return true;
        }
        public void RangedAttack() //원거리 공격
        {
            if(!ResetWeaponPosition()) return;

            Vector3 direction = new Vector3(player.rote.x, player.rote.y, 0).normalized;

            transform.position = player.transform.position + direction * 0.75f;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            if (canFire)
            {
               
                StartCoroutine(BulletFiring(angle)) ;
            }
        }

       
        private IEnumerator BulletFiring(float angle)
        {
            AudioPlay(1);
            canFire = false; // 발사를 잠시 비활성화
            ObjectPoolingManager.Instance.GetBullet(transform.GetChild(0)); // 총알 생성
            yield return fireDelay; // 설정된 시간만큼 대기
            canFire = true; // 발사 가능 상태로 변경
        }
        public void ExWeapoonMove() // 회전시 대상 포지션을 본인으로 하여 무기에 특이한 움직임 구현
        {
            Vector3 direction = new Vector3(player.rote.x, player.rote.y, 0).normalized;

            transform.position = transform.position + direction * 0.75f;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void WeaponChange(Slot slot) // 무기 교체시 하단 무기 이미지 교체
        {         
            if (slot.itemData != null)
            {              
                if(slot.itemData.itemType == ItemType.HealthPotion) return;
                
                TypeChange(slot.itemData.waponType);
                int weaponIndex = (int)slot.itemData.waponType;
                SetActiveObject(weaponIndex);
                equippedWeaponList[weaponIndex].GetComponent<CollisionNotifier>().SetImage(slot.itemData);
                fireDelay = new WaitForSeconds(5.5f - player.playerData.AdditionalAttackSpeed);
            }
            else
            {
                int weaponIndex = -1;
                SetActiveObject(weaponIndex);
            }
            
           
        }

        public void SetActiveObject(int index)//선택한 오브젝트 제외 비활성화 
        {
            for(int i =0; i < equippedWeaponList.Count; i++)
            {
                if(i == index)
                {
                    equippedWeaponList[i].SetActive(true);
                }else
                {
                    equippedWeaponList[i].SetActive(false);
                }
            }
        }

        public void TypeChange(WeaponType _waponType)
        {
            currentType = _waponType;
        }
        public void AudioPlay(int index)
        {
            AudioManager.Instance.EffectsSourcePlay(index);
        }
    }


}
