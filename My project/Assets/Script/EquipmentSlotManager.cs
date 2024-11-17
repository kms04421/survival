
using UnityEngine;
using System.Collections.Generic;
namespace MainSSM
{
    public class EquipmentSlotManager : MonoBehaviour
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
            if(slot.itemData.itemType == ItemType.Weapon)
            {
                player.playerData.AdditionalStateUP(slot.itemData);
            }
            

            UIManager.Instance.MenuTextUpdate();

        }
      

       

    }



}
