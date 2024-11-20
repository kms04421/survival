
using UnityEngine;
using System.Collections.Generic;
namespace MainSSM
{
    public class SelectSoltOnClickEvent : MonoBehaviour
    {
        private Player player;
        public List<Slot> equipmentList; // ��񽽷� ����Ʈ
        void Start()
        {
            player = FindAnyObjectByType<Player>();
        }

        public void ApplyEquipmentStats(Slot slot) // ���� â���� ���ý� �ش� ������ ���ݷ� ����
        {
            player.playerData.AdditionalStateReset(); // �߰� �ɷ�ġ �ʱ�ȭ
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
