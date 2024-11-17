using MainSSM;
using UnityEngine;

public class BulletMovent : MonoBehaviour
{
    public float speed = 5;        // 총알의 이동 속도
    public float lifeTime = 5f;     // 총알의 생명 주기
    public int hitCount = 1;
    public Player player;
    

    public void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }
    // 총알 초기화
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

        // 방향으로 이동
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌 시 데미지 처리
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
