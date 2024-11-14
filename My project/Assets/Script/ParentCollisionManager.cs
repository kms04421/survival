using MainSSM;
using UnityEngine;

public class ParentCollisionManager : MonoBehaviour
{
    WeaponController weaponController;
    Player player;

    private void Start()
    {
        weaponController = transform.parent.GetComponent<WeaponController>();
        player = transform.parent.parent.GetComponent<Player>();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if(player.rote.x != 0 || player.rote.y != 0)
            {
                collision.GetComponent<IHitListener>().OnHit(player.playerData.Damage);
            }
        }
    }
}
