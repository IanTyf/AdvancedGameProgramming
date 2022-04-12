using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject character;

    public float cd;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cd)
        {
            timer = 0;
            GameObject c = Instantiate(character);
            c.transform.position = new Vector3(Random.Range(-4f, 4f), 2, Random.Range(-4f, 4f));
        }
    }
}
