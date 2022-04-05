using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AIBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public bool jumping;
    public GameObject player;

    private BehaviorTree.Tree<AIBehavior> _tree;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        jumping = false;
        rb = GetComponent<Rigidbody>();
        // two sequences
        var jumpTree = new Tree<AIBehavior>(
            new Sequence<AIBehavior>(
                new IsCloseToPlayer(),
                new NotJumping(),
                new Jump()
            )
        );

        var walkTree = new Tree<AIBehavior>(
            new Sequence<AIBehavior>(
                new Walk()
            )
        );

        _tree = new Tree<AIBehavior>(
            new Selector<AIBehavior> (
                jumpTree,
                walkTree
            )
        );
    }

    // Update is called once per frame
    void Update()
    {
        _tree.Update(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            jumping = false;
        }
    }

    public class IsCloseToPlayer : Node<AIBehavior>
    {
        public override bool Update(AIBehavior context)
        {
            return Vector3.Distance(context.player.transform.position, context.transform.position) < 2.0;
        }
    }

    public class NotJumping : Node<AIBehavior>
    {
        public override bool Update(AIBehavior context)
        {
            return !context.jumping;
        }
    }

    public class Jump : Node<AIBehavior>
    {
        public override bool Update(AIBehavior context)
        {
            context.rb.AddForce(Vector3.up * context.jumpSpeed, ForceMode.Impulse);
            context.jumping = true;
            return true;
        }
    }

    public class Walk : Node<AIBehavior>
    {
        public override bool Update(AIBehavior context)
        {
            context.transform.position = context.transform.position + Vector3.right * context.moveSpeed * Time.deltaTime;
            if (context.transform.position.x >= 6) context.moveSpeed = -1 * Mathf.Abs(context.moveSpeed);
            if (context.transform.position.x <= -6) context.moveSpeed = Mathf.Abs(context.moveSpeed);
            return true;
        }
    }
}
