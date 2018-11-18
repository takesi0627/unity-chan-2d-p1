using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    #region ATTRIBUTE
    [SerializeField] float moveSpeed = 0.6f;

    #endregion

    #region ANIMATION_RELATED
    static int hashSpeed = Animator.StringToHash("Speed");
    static int hashFallSpeed = Animator.StringToHash("FallSpeed");
    static int hashGroundDistance = Animator.StringToHash("GroundDistance");
    static int hashIsCrouch = Animator.StringToHash("IsCrouch");
    static int hashAttack1 = Animator.StringToHash("Attack1");
    static int hashAttack2 = Animator.StringToHash("Attack2");
    static int hashAttack3 = Animator.StringToHash("Attack3");


    static int hashDamage = Animator.StringToHash("Damage");
    static int hashIsDead = Animator.StringToHash("IsDead");

    [SerializeField] private float characterHeightOffset = 0.2f;
    [SerializeField] LayerMask groundMask;

    [SerializeField, HideInInspector] Animator animator;
    [SerializeField, HideInInspector] SpriteRenderer spriteRenderer;
    [SerializeField, HideInInspector] Rigidbody2D rig2d;
    #endregion

    public int hp = 4;
    static readonly float RAY_CAST_DISTANCE = 100.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rig2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float axis = Input.GetAxisRaw("Horizontal");
        bool isDown = Input.GetAxisRaw("Vertical") < 0;


        // move right/left

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            var touchDelta = Input.GetTouch(0).deltaPosition.normalized;
            axis = touchDelta.x;
            transform.Translate(new Vector3(axis * moveSpeed, 0, 0));
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).tapCount == 1 && IsGrounded())
        {
            rig2d.velocity = new Vector2(rig2d.velocity.x, 5);
        }

        var distanceFromGround = Physics2D.Raycast(transform.position, Vector3.down, 1, groundMask);

        // update animator parameters
        animator.SetBool(hashIsCrouch, isDown);
        animator.SetFloat(hashGroundDistance, distanceFromGround.distance.Equals(0) ? 99 : distanceFromGround.distance - characterHeightOffset);
        animator.SetFloat(hashFallSpeed, rig2d.velocity.y);
        animator.SetFloat(hashSpeed, Mathf.Abs(axis));
        if (Input.GetKeyDown(KeyCode.Z)) { animator.SetTrigger(hashAttack1); }
        if (Input.GetKeyDown(KeyCode.X)) { animator.SetTrigger(hashAttack2); }
        if (Input.GetKeyDown(KeyCode.C)) { animator.SetTrigger(hashAttack3); }

        // flip sprite
        if (!axis.Equals(0))
            spriteRenderer.flipX = axis < 0;
    }

    bool IsGrounded () {
        var distanceFromGround = Physics2D.Raycast(transform.position, Vector3.down, RAY_CAST_DISTANCE, groundMask);
        return distanceFromGround.distance - characterHeightOffset <= 0;
    }
}
