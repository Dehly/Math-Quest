using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontCall : MonoBehaviour
{
    public static Transform title;
    public static GameObject letter;

	void Start ()
    {
        letter = Resources.Load<GameObject>("Letter");
        title = GameObject.Find("Title").transform;
    }

    void Update ()
    {

	}
}
