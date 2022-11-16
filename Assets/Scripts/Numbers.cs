using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    public static Sprite[] sprite; //sprite numbers quantity
    public static Sprite[] spell; //sprites spells
    public static Sprite[] special; //sprites special bar
    public static Sprite[] heart; //sprites lives
    public static Sprite[] stages; //sprites numbers stages and stats
    public static Sprite[] letters; //sprites titles' letters

    void Start ()
    {
        DontDestroyOnLoad(this);
        //Load all images from Resources's folder
        sprite = Resources.LoadAll<Sprite>("NumImg");
        spell = Resources.LoadAll<Sprite>("SpellImg");
        special = Resources.LoadAll<Sprite>("SpImg");
        heart = Resources.LoadAll<Sprite>("HpImg");
        stages = Resources.LoadAll<Sprite>("NumStages");
        letters = Resources.LoadAll<Sprite>("fontsheet001");
    }
}
