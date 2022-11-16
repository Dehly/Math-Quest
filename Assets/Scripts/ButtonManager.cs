using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class ButtonManager : MonoBehaviour
{
    //load scene
    public void Scene(string scene)
    {
        SceneManager.LoadScene(scene);
        GameManager.death = false;
        AlternativesMovement.lastQ = Time.time;
    }

    //Quit game
    public void Quit()
    {
        Application.Quit();
    }

    //erase game
    public void Erase()
    {
        GameDatas.Erase();
        GameDatas.Load();
    }

    //buy stats: live or special
    public void Stats(int x)
    {
        if (GameManager.points >= GameManager.statsValue[x] * (GameManager.statsLevel[x] + 1))
        {
            GameManager.aS.PlayOneShot(GameManager.audios.sounds[4]);
            GameManager.points -= GameManager.statsValue[x] * (GameManager.statsLevel[x] + 1);
            GameManager.statsLevel[x]++;
        }
    }

    //stage level up
    public void StageUp()
    {
        if (GameManager.operation > 0)
        {
            if (GameManager.points >= GameManager.statsValue[2] * (GameManager.level[GameManager.operation - 1] + 1) && GameManager.level[GameManager.operation - 1] < 9)
            {
                GameManager.aS.PlayOneShot(GameManager.audios.sounds[4]);
                GameManager.points -= GameManager.statsValue[2] * (GameManager.level[GameManager.operation - 1] + 1);
                GameManager.level[GameManager.operation - 1]++;
            }
        }
    }
    //buy spells
    public void Spells(int x)
    {
        if (GameManager.points >= GameManager.spellValue[x])
        {
            GameManager.aS.PlayOneShot(GameManager.audios.sounds[4]);
            GameManager.points -= GameManager.spellValue[x];
            GameManager.spell = x + 1;
            GameManager.cast = true;
        }
    }

    //Run
    public void Run()
    {
        GameManager.run = !GameManager.run;
    }
    //choose difficulty
    public void Difficulty(int x)
    {
        GameManager.difficulty = x;
    }

    //use special
    public void Special()
    {
        if(GameManager.sp > 0 && GameManager.spellImage.gameObject.activeSelf)
        {
            GameManager.sp--;
            GameManager.rand = Random.Range(0, 4);
            if (GameManager.math.respostas[GameManager.rand].activeSelf == true && GameManager.math.respostas[GameManager.rand].tag == "Errada")
            {
                GameObject o = Instantiate(GameManager.thunder, GameManager.math.respostas[GameManager.rand].transform);
                o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y - 1.5f);
                //math.respostas[rand].SetActive(false);
            }
            else
            {
                GameManager.miss.SetActive(true);
            }
        }
    }

    //call text with font
    public void TextCall(string a)
    {
        for (int i = 0; i < FontCall.title.childCount; i++)
        {
            Destroy(FontCall.title.GetChild(i).gameObject);
        }
        for (int i = 0; i < a.Length; i++)
        {
            int x = a[i];
            if (a[i] == ' ')
                x = 26;
            else if (a[i] == '-')
                x = 44;
            else
                x -= 65;
            FontCall.letter.GetComponent<Image>().sprite = Numbers.letters[x];
            Instantiate(FontCall.letter, FontCall.title);
        }
    }

    public void Operation(int x)
    {
        Explanation.x = 0;
        Explanation.timeLetters = 0.05f;
        Explanation.timeT = 0;
        Explanation.index = 0;
        Explanation.explanation.text = "";
        Explanation.stop = false;
        Explanation.surprise.SetActive(false);
        for (int i = 0; i < Explanation.qtd.Length; i++)
        {
            Explanation.qtd[i] = 0;
        }
        for (int i = 0; i < Explanation.slimes.Length; i++)
        {
            Explanation.slimes[i].SetActive(false);
        }
        GameManager.operation = x;
        Explanation.change = true;
    }

    public void Page(int x)
    {
        if(Explanation.stop || Explanation.timeLetters == 0)
        {
            Explanation.x += x;
            Explanation.timeLetters = 0.05f;
            Explanation.timeT = 0;
            Explanation.index = 0;
            Explanation.explanation.text = "";
            Explanation.stop = false;
            Explanation.surprise.SetActive(false);
            for (int i = 0; i < Explanation.qtd.Length; i++)
            {
                Explanation.qtd[i] = 0;
            }
            if (GameManager.operation == 2)
            {
                Explanation.qtd[2] = 5;
            }

            for (int i = 0; i < Explanation.slimes.Length; i++)
            {
                Explanation.slimes[i].SetActive(false);
            }
            for (int i = 0; i < Explanation.bats.Length; i++)
            {
                Explanation.bats[i].SetActive(false);
            }
            if(GameManager.operation == 2 && Explanation.x == 2)
            {
                for (int i = 0; i < Explanation.bats.Length; i++)
                {
                    Explanation.bats[i].SetActive(true);
                }
            }
        }
        else
        {
            Explanation.timeLetters = 0;
        }
    }

    public void Text(int x)
    {
        GameManager.credits.text = GameManager.infos[x];
    }

    public void Pause(int x)
    {
        Time.timeScale = x;
    }
    public void Open(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void Close(GameObject panel)
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            GameManager.yesBtn.SetActive(false);
            GameManager.noBtn.SetActive(false);
        }
        panel.SetActive(false);
    }

    public void Options(bool op)
    {
        GameManager.yesBtn.SetActive(op);
        GameManager.noBtn.SetActive(op);
    }

    //public void ADS(int x)
    //{
    //    GameManager.ads = x;
    //    ShowOptions options = new ShowOptions();
    //    options.resultCallback = HandleShowResult;
    //    Advertisement.Show("rewardedVideo", options);
    //}

    //private void HandleShowResult(ShowResult result)
    //{
    //    if (result == ShowResult.Finished)
    //    {
    //        switch (GameManager.ads)
    //        {
    //            case 0:
    //                GameManager.factor *= 2;
    //                break;
    //            case 1:
    //                GameManager.magic = true;
    //                break;
    //        }

    //    }
    //    else if (result == ShowResult.Skipped)
    //    {
    //        Debug.LogWarning("Video was skipped - Do NOT reward the player");
    //    }
    //    else if (result == ShowResult.Failed)
    //    {
    //        Debug.LogError("Video failed to show");
    //    }
    //}

    public void Save()
    {
        GameDatas.Save();
    }
}
