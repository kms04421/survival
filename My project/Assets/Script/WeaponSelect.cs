using UnityEngine;
namespace MainSSM
{
    public class WeaponSelect : MonoBehaviour
    {
        private Player player;
        private ItemData savedItemData; 
        void Start()
        {
            player = FindAnyObjectByType<Player>();
        }

        public void WeaponClick(Slot slot)
        {

            if(slot.itemData != null)
            {
                if (savedItemData == null || !savedItemData.itemCode.Equals(slot.itemData.itemCode) && slot.itemData != null)
                {
                    player.playerData.ItemStateUP(slot.itemData);
                    
                }
                if (savedItemData != null  )
                {
                    if (!savedItemData.itemCode.Equals(slot.itemData.itemCode))
                    {
                        player.playerData.ItemStateDown(savedItemData);
                    }
                   

                }
                savedItemData = slot.itemData;
            }
            else
            {
                if (savedItemData != null)
                {
                    player.playerData.ItemStateDown(savedItemData);
                    savedItemData = slot.itemData;
                }
            }


            
            transform.position = slot.transform.position;
           
            UIManager.Instance.MenuTextUpdate();

        }


    }

}
