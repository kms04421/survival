
using UnityEngine;
using System.Collections.Generic;
namespace MainSSM
{
    public class SelectSoltOnClickEvent : MonoBehaviour
    {
        private Player player;
        public List<Slot> equipmentList; // 장비슬롯 리스트
        void Start()
        {
            player = FindAnyObjectByType<Player>();
        }

        public void ApplyEquipmentStats(Slot slot) // 선택 창에서 선택시 해당 아이템 공격력 적용
        {
            player.playerData.AdditionalStateReset(); // 추가 능력치 초기화
            for (int i = 0; i < equipmentList.Count; i++)
            {
                player.playerData.AdditionalStateUP(equipmentList[i].itemData);
            }
            if(slot.itemData != null)
            {

                switch(slot.itemData.itemType)
                {
                    case ItemType.Weapon:
                        player.playerData.AdditionalStateUP(slot.itemData);
                        break;
                    case ItemType.HealthPotion:
                        player.playerData.Heal(slot.itemData.healthRecovery);
                        UIManager.Instance.HpBarSet();
                        slot.DecCountQuantity(1);
                        break;
                }
              
            }

            UIManager.Instance.MenuTextUpdate();

        }
      

       

    }



}
