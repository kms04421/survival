using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FloatingJoystick movejoy;
    public FloatingJoystick rotejoy;
    public GameObject weapoon;
    public float speed = 5f;
    private SpriteRenderer weapoonSprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigid;
    private Vector2 movement;
    private Vector2 rote;
    private Vector2 vector2Zero;
    float isMoving = 0;
    public WeaponController weaponController;
    static public bool ActionPlayer = true;
    // Start is called before the first frame update
    void Awake()
    {
       
        vector2Zero = new Vector2(0, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        weapoonSprite = weapoon.transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movement.x = movejoy.Horizontal;
        movement.y = movejoy.Vertical;
        rote.x = rotejoy.Horizontal;
        rote.y = rotejoy.Vertical;

    }
    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + movement * speed * Time.fixedDeltaTime);
        WeapoonMove();
    }
    private void LateUpdate()
    {
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

    
}
