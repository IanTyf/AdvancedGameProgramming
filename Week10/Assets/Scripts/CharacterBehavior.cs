using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CharacterBehavior : MonoBehaviour
{
    public ReferenceKeeper rk;

    private string myName;
    private float speed;
    private float size;
    private float predLevel;
    private Material mat;

    public float timer;

    private Tree<CharacterBehavior> _tree;

    // Start is called before the first frame update
    void Start()
    {
        rk = GameObject.Find("ReferenceKeeper").GetComponent<ReferenceKeeper>();

        int randInd = Random.Range(0, rk.characterDB.characters.Length);
        Character c = rk.characterDB.characters[randInd];

        myName = c.name;
        speed = c.speed;
        size = c.size;
        predLevel = c.predLevel;
        mat = c.material;

        timer = 0;

        GetComponent<MeshRenderer>().material = mat;
        transform.localScale = 0.2f * size * Vector3.one;

        var goLeft = new Tree<CharacterBehavior>(
            new Sequence<CharacterBehavior>(
                new Rand(4),
                new Move("left")
            )
        );
        var goRight = new Tree<CharacterBehavior>(
            new Sequence<CharacterBehavior>(
                new Rand(3),
                new Move("right")
            )
        );
        var goUp = new Tree<CharacterBehavior>(
            new Sequence<CharacterBehavior>(
                new Rand(2),
                new Move("up")
            )
        );
        var goDown = new Tree<CharacterBehavior>(
            new Sequence<CharacterBehavior>(
                new Move("down")
            )
        );

        _tree = new Tree<CharacterBehavior>(
            new Selector<CharacterBehavior>(
                goLeft,
                goRight,
                goUp,
                goDown
            )
        );
    }

    // Update is called once per frame
    void Update()
    {
        _tree.Update(this);
        transform.rotation = Quaternion.identity;
    }

    public class Rand : Node<CharacterBehavior>
    {
        private int n;

        public Rand(int n)
        {
            this.n = n;
        }

        public override bool Update(CharacterBehavior context)
        {
            int randNum = Random.Range(0, this.n);
            return randNum == 0;
        }
    }

    public class Move : Node<CharacterBehavior>
    {
        private string dir;

        public Move(string dir)
        {
            this.dir = dir;
        }

        public override bool Update(CharacterBehavior context)
        {
            context.timer += Time.deltaTime;
            if (context.timer > 2 - context.speed/2f)
            {
                context.timer = 0;
                switch (dir)
                {
                    case "left": context.transform.position = context.transform.position + new Vector3(-0.1f * context.speed, 0, 0);
                        break;
                    case "right":
                        context.transform.position = context.transform.position + new Vector3(0.1f * context.speed, 0, 0);
                        break;
                    case "up":
                        context.transform.position = context.transform.position + new Vector3(0, 0, 0.1f * context.speed);
                        break;
                    case "down":
                        context.transform.position = context.transform.position + new Vector3(0, 0, -0.1f * context.speed);
                        break;
                }
            }

            return true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Character") return;

        CharacterBehavior other = collision.gameObject.GetComponent<CharacterBehavior>();
        if (size < other.size)
        {
            other.size++;
            other.gameObject.transform.localScale = 0.2f * other.size * Vector3.one;
            Destroy(this.gameObject);
        }
    }
}
