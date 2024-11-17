using MainSSM;
using UnityEngine;

public class CollisionNotifier : MonoBehaviour
{
    public Player player;
    private ItemData itemData;
    public SpriteRenderer spriteRenderer;
    private void Start()
    {     
        player = FindAnyObjectByType<Player>();
    }

    public void SetImage(ItemData itemData)
    {
        if (itemData.icon == null)
        {
            spriteRenderer.sprite = null;
        }
        spriteRenderer.sprite = itemData.icon;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if(player.rote.x != 0 || player.rote.y != 0)
            {
                collision.GetComponent<IHitListener>().OnHit(player.playerData.BaseAttackPower);
            }
        }
    }
}
