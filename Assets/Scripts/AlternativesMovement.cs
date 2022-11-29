using UnityEngine;
using System.Collections;

public class AlternativesMovement : MonoBehaviour
{
    //public static float lastQ = 0; //time to move alternatives
    private Vector3 pos; //alternatives' start position
    public static GameObject[] enemies;

    void Start ()
    {
        pos = transform.position;
        enemies = new GameObject[5];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = GameObject.Find("enemy" + i);
            enemies[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("en_" + (GameManager.operation - 1));
        }
        StartCoroutine(MoveAlternatives());
    }

    void Update()
    {
        GameManager.run = Input.GetKey(KeyCode.UpArrow);

        //only happens when the player is not dead
        if (!GameManager.death)
        {
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

    private IEnumerator MoveAlternatives()
    {
        while (!GameManager.death)
        {
            if (GameManager.run)
                yield return new WaitForSeconds(.25f);
            else
                yield return new WaitForSeconds(1);
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        }
    }
}
