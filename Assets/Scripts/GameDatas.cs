using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

public class GameDatas : MonoBehaviour
{
    public static GameDatas game;

    void Awake()
    {
        if (game == null)
        {
            DontDestroyOnLoad(gameObject);
            game = this;
        }
        else if (game != this)
        {
            Destroy(gameObject);
        }
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.mq");

        PlayerData data = new PlayerData
        {
            points = GameManager.points,
            life = GameManager.life,
            sp = GameManager.sp,
            factor = GameManager.factor,
            timeD = (int)GameManager.timeD,
            timeM = (int)GameManager.timeM,
            statsLevel = GameManager.statsLevel,
            level = GameManager.level,
            mathValue = GameManager.mathValue,
            magic = GameManager.magic,
            cast = GameManager.cast,
            now = DateTime.Now
        };

        bf.Serialize(file, data);
        file.Close();
    }

    public static void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/savedGame.mq"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.mq", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            GameManager.points = data.points;
            GameManager.life = data.life;
            GameManager.sp = data.sp;
            GameManager.factor = data.factor;
            GameManager.statsLevel = data.statsLevel;
            GameManager.level = data.level;
            GameManager.mathValue = data.mathValue;
            GameManager.timeD = data.timeD + (int)DateTime.Now.Subtract(data.now).TotalSeconds;
            GameManager.timeM = data.timeM + (int)DateTime.Now.Subtract(data.now).TotalSeconds;
            GameManager.magic = data.magic;
            GameManager.cast = data.cast;
        }
        else
        {
            GameManager.points = 0;
            GameManager.life = 1;
            GameManager.sp = 0;
            GameManager.factor = 1;
            GameManager.statsLevel = new int[2];
            GameManager.level = new int[10];
            GameManager.mathValue = new int[3];
            GameManager.timeD = 0;
            GameManager.timeM = 0;
            GameManager.magic = false;
            GameManager.cast = false;
        }
    }

    public static void Erase()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.mq"))
            File.Delete(Application.persistentDataPath + "/savedGame.mq");
    }
}

[Serializable]
class PlayerData
{
    public int points;
    public int life;
    public int sp;
    public int factor;
    public int timeM;
    public int timeD;
    public int[] statsLevel;
    public int[] level;
    public int[] mathValue;
    public bool magic;
    public bool cast;
    public DateTime now;
}
