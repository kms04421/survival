
using UnityEngine;
namespace MainSSM
{
    public class Player : MonoBehaviour, IHitListener
    {
        public FloatingJoystick movejoy;
        public FloatingJoystick rotejoy;

        public GameObject weapoon;
        private SpriteRenderer weapoonSprite;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Rigidbody2D rigid;
        private Vector2 movement;
        private Vector2 rote;
        private Vector2 vector2Zero;
        float isMoving = 0;
        [HideInInspector] public WeaponController weaponController;
        static public bool ActionPlayer = true;
        public PlayerData playerData;
        private PlayerState currentState; // 현재 상태
        private Inventory inventory;
        private enum PlayerState
        {
            Idle,// 기본
            Menu, // 메뉴 오픈상태
            Die // 사망
        }
        // Start is called before the first frame update
        void Awake()
        {
            playerData = new PlayerData(10, 3.5f, 100);
            vector2Zero = new Vector2(0, 0);
            spriteRenderer = GetComponent<SpriteRenderer>();
            weapoonSprite = weapoon.transform.GetChild(0).GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody2D>();
            inventory = GetComponent<Inventory>();
        }
        private void Update()
        {

            UpdateState();

        }
        private void FixedUpdate()
        {
            PerformAction();

        }
        private void LateUpdate()
        {
            if (currentState == PlayerState.Die) return;
            if (movement.x > 0)
            {
                spriteRenderer.flipX = false; // 오른쪽 방향
            }
            else if (movement.x < 0)
            {
                spriteRenderer.flipX = true; // 왼쪽 방향
            }

            isMoving = movement.magnitude;
            animator.SetFloat("Speed", isMoving);
        }
        private void UpdateState()
        {
            switch (currentState)
            {
                case PlayerState.Idle:
                    movement.x = movejoy.Horizontal;
                    movement.y = movejoy.Vertical;
                    rote.x = rotejoy.Horizontal;
                    rote.y = rotejoy.Vertical;
                    break;
                case PlayerState.Menu:
                    break;
                case PlayerState.Die:
                    animator.SetTrigger("Dead");
                    weapoon.SetActive(false);
                    rigid.bodyType = RigidbodyType2D.Kinematic;
                    break;
            }
        }
        private void PerformAction()
        {
            switch (currentState)
            {
                case PlayerState.Idle:
                    rigid.MovePosition(rigid.position + movement * playerData.Speed * Time.fixedDeltaTime);
                    WeapoonMove();
                    break;
                case PlayerState.Menu:
                    break;
                case PlayerState.Die:
                    break;
            }


        }

        private void WeapoonMove()
        {

            if (rote == vector2Zero)
            {
                if (rote.x == 0 && rote.y == 0)
                {
                    weapoon.transform.position = transform.position;
                    ActionPlayer = false;
                }
                return;
            }
            ActionPlayer = true;
            Vector3 direction = new Vector3(rote.x, rote.y, 0).normalized;
            weapoon.transform.position = transform.position + direction * 0.75f;
        }

        public void OnHit(int dam)
        {
            if (currentState == PlayerState.Die) return;
            playerData.TakeDamage(dam);
            UIManager.Instance.HpBarSet(playerData);
            if (playerData.Hp <= 0)
            {
                currentState = PlayerState.Die;
            }
        }
      
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 6)
            {
                Item item = collision.GetComponent<Item>();
                if (item != null)
                {
                    inventory.AddItem(item.PickupItem());
                }
            }
        }

    }
}