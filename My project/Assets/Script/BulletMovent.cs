using MainSSM;
using UnityEngine;

public class BulletMovent : MonoBehaviour
{
    public float speed = 5;        // �Ѿ��� �̵� �ӵ�
    public float lifeTime = 5f;     // �Ѿ��� ���� �ֱ�
    public int hitCount = 1;
    public Player player;
    

    public void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }
    // �Ѿ� �ʱ�ȭ
    public void Initialize()
    {

        ObjectPoolingManager.Instance.bulletPool.Enqueue(gameObject);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        hitCount = 1;
        speed = 5 * (player.playerData.BaseAttackSpeed / 1.5f);
        Invoke("Initialize", 5f);
    }
   
    private void Update()
    {

        // �������� �̵�
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �浹 �� ������ ó��
        if (collision.gameObject.layer == 10)
        {

            collision.GetComponent<IHitListener>().OnHit(player.playerData.BaseAttackPower);
            hitCount--;
            if (hitCount == 0)
            {

                CancelInvoke("Initialize");
                Initialize();
            }

        }
    }


}
