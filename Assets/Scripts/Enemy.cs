using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace takeshi
{
    public enum EnemyState
    {
        IDLE,
        TRACING,
        ATTACK,
        DIE
    };

    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
    public class Enemy : StatedObjectBase<Enemy, EnemyState>
    {
        [SerializeField] float moveSpeed = 0.06f;
        Rigidbody2D rb;
        SpriteRenderer spriteRenderer;
        Animator animator;

        static int hashSpeed = Animator.StringToHash("Speed");

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            stateList.Add(EnemyState.IDLE, new StateIdle(this));

            stateMachine = new StateMachine<Enemy>();

            ChangeState(EnemyState.IDLE);
        }

        // Use this for initialization
        void Start()
        {

        }

        protected override void Update()
        {
            base.Update();

            animator.SetFloat(hashSpeed, rb.velocity.magnitude);
            if (!rb.velocity.Equals(0f)){
                spriteRenderer.flipX = rb.velocity.x > 0;
            }
        }

        class StateIdle : State<Enemy>
        {
            public StateIdle(Enemy owner) : base(owner) { }

            public override void Enter() {
                owner.StartCoroutine(Idle());
            }

            public override void Exit() {}

            public override void Execute() {}

            IEnumerator MoveRight (float time) {
                owner.rb.velocity = new Vector2(owner.moveSpeed, 0);
                yield return new WaitForSeconds(time);
            }

            IEnumerator MoveLeft (float time) {
                owner.rb.velocity = new Vector2(-owner.moveSpeed, 0);
                yield return new WaitForSeconds(time);
            }

            IEnumerator Wait (float time) {
                owner.rb.velocity = Vector2.zero;
                yield return new WaitForSeconds(time);
            }

            IEnumerator Idle () {
                while (true) {
                    // move right for 3 seconds.
                    yield return MoveRight(3.0f);

                    // wait for 1 second.
                    yield return Wait(2.0f);

                    // move left for 3 seconds
                    yield return MoveLeft(3.0f);

                    // wait for 1 second
                    yield return Wait(2.0f);

                    // move left for 3 seconds
                    yield return MoveLeft(3.0f);

                    // wait for 1 second
                    yield return Wait(2.0f);

                    // move right for 3 seconds.
                    yield return MoveRight(3.0f);

                    // wait for 1 second
                    yield return Wait(2.0f);
                }
            }
        }
    }

   
}