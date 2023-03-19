using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnim : MonoBehaviour
{
    public Sprite[] charaterSprites;

    [HideInInspector] public Vector2 moveDirection=new Vector2(0,-1);
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public int animIndex = 3;

    private Animator animator; 
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        SetDirection(moveDirection);
        //if (animator != null)
        //    StartCoroutine(UpdateSprite());

    }

    public void SetDirection(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
        animator.SetFloat("HorizontalMoverment", this.moveDirection.x);
        animator.SetFloat("VerticalMoverment", this.moveDirection.y);
    }  
    
    public void SetMove(bool isMoving)
    {
        this.isMoving = isMoving;
        animator.SetBool("IsMoving", this.isMoving);
    }

    

    private IEnumerator UpdateSprite()
    {
        while (true)
        {
            yield return null;
            spriteRenderer.sprite=charaterSprites[animIndex];
        }
    }    
}
