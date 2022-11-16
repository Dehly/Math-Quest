using UnityEngine;
using System.Collections;

public class AlternativesMovement : MonoBehaviour
{
    public static float lastQ = 0; //time to move alternatives
    private Vector3 pos; //alternatives' start position
    private GameObject[] enemies;
    private GameObject[] bats;

    void Start ()
    {
        pos = transform.position;
        enemies = new GameObject[5];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = GameObject.Find("enemy" + i);
            //enemies[i].SetActive(false);
        }
        //bats = new GameObject[5];
        //for (int i = 0; i < bats.Length; i++)
        //{
        //    bats[i] = GameObject.Find("enemy" + (i + 5));
        //    bats[i].SetActive(false);
        //}
    }

    void Update ()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            //enemies[i].SetActive(true);
            enemies[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("en_" + GameManager.operation);
            //bats[i].SetActive(false);
        }
        //else if (GameManager.operation == 2)
        //{
        //    for (int i = 0; i < bats.Length; i++)
        //    {
        //        slimes[i].SetActive(false);
        //        bats[i].SetActive(true);
        //    }
        //}
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GameManager.run = true; //using run
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            GameManager.run = false; //stop using run
        }

        //only happens when the player is not dead
        if (!GameManager.death)
        {
            if(GameManager.run)
            {
                if (Time.time - lastQ >= .25f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                    lastQ = Time.time;
                }
            }
            else
            {
                if (Time.time - lastQ >= 2)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                    lastQ = Time.time;
                }
            }
            //return alternatives to the start position
            if (GameManager.next)
            {
                transform.position = pos;
                GameManager.run = false;
            }
        }
        //if player dies the alternatives return to the start position
        else
        {
            transform.position = pos;
        }
    }
}
