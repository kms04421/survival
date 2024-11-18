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
        private bool isAttacking = false; //���� ������ ����
        private int Dam = 100;
        private Quaternion targetRotation;
        private Vector3 weapoonPos; // ���� �⺻��ġ
        private Vector2 vector2Zero; //���°� ������� üũ��
        float speed = 0;
        private Player player;
        private bool canFire = true; // �߻� ���� ����
        private WaitForSeconds fireDelay; // ���� �ӵ�
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
                yield return null; // �� �����Ӹ��� ȸ���ϵ��� ��
                
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
        public void RangedAttack() //���Ÿ� ����
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
            canFire = false; // �߻縦 ��� ��Ȱ��ȭ
            ObjectPoolingManager.Instance.GetBullet(transform.GetChild(0)); // �Ѿ� ����
            yield return fireDelay; // ������ �ð���ŭ ���
            canFire = true; // �߻� ���� ���·� ����
        }
        public void ExWeapoonMove() // ȸ���� ��� �������� �������� �Ͽ� ���⿡ Ư���� ������ ����
        {
            Vector3 direction = new Vector3(player.rote.x, player.rote.y, 0).normalized;

            transform.position = transform.position + direction * 0.75f;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void WeaponChange(Slot slot) // ���� ��ü�� �ϴ� ���� �̹��� ��ü
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

        public void SetActiveObject(int index)//������ ������Ʈ ���� ��Ȱ��ȭ 
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
