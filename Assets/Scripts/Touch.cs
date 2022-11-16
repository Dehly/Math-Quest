using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Personagem");
    }
    private void OnMouseDown()
    {
        if(!GameManager.death)
        {
            if (Input.mousePosition.x < Screen.width / 2)
                player.transform.position = new Vector3(player.transform.position.x - 3, player.transform.position.y, player.transform.position.z);
            else
                player.transform.position = new Vector3(player.transform.position.x + 3, player.transform.position.y, player.transform.position.z);
        }
    }
}
