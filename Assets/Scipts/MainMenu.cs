using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public GameObject menu, menu1, menu2, menu3;
    public Button button1, button2, button3, button4, choosecolorblue, choosecolorgreen, choosecolorred, choosecoloryellow, submit1;
    public TMP_InputField playername;
    public GameController con;
    // Start is called before the first frame update
    void Start()
    {
        {
            GameObject.Find("Playing Field").SetActive(false);
            GameObject.Find("Nature").SetActive(false);
            button1.onClick.AddListener(() => Asd(1));
            button2.onClick.AddListener(() => Asd(2));
            button3.onClick.AddListener(() => Asd(3));
            button4.onClick.AddListener(() => Asd(4));
            /*  choosecolorblue.onClick.AddListener(() => Spawner(5));
            choosecolorgreen.onClick.AddListener(() => Spawner(6));
            choosecolorred.onClick.AddListener(() => Spawner(7));
            choosecoloryellow.onClick.AddListener(() => Spawner(8));*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoSomething()
    {
        Debug.Log("HUSO");
        menu1.SetActive(false);
        foreach (Player player in con.players)
        {
            if (player.isActive == true)
            {
                Debug.Log("HUSO");

            }
            //foreach()
        }
    }
    public void StoreName()
    {
        PlayerPrefs.SetString("username",playername.text);
    }
    void Asd(int b)
    {
        if (b == 1)
        {
            bool[] playersaktiv = new bool[1];
            playersaktiv[0] = true; 
            con.InitializeGame(playersaktiv);
        }
        if (b == 2)
        {
            bool[] playersaktiv = new bool[2]!;
            playersaktiv[0] = true;
            playersaktiv[1] = true;
            con.InitializeGame(playersaktiv);
        }
        if (b == 3)
        {
            bool[] playersaktiv = new bool[3]!;
            con.InitializeGame(playersaktiv);
            playersaktiv[0] = true;
            playersaktiv[1] = true;
            playersaktiv[2] = true;
        }
        if (b == 4)
        {
            bool[] playersaktiv = new bool[4]!;
            con.InitializeGame(playersaktiv);
            playersaktiv[0] = true;
            playersaktiv[1] = true;
            playersaktiv[2] = true;
            playersaktiv[3] = true;
            
        }
    }
   /* public void Spawner(int a)
    {

        if(player1.isActive == true)
        if (a == 5)         
        {
            foreach (SpawnPoint p in player1.spawnpoints)
            {
                p.Spawn();
            }
        }
        if (a == 6)
        {
            foreach (SpawnPoint p in player2.spawnpoints)
            {
                p.Spawn();
            }
        }
        if (a == 7)
        {
            foreach (SpawnPoint p in player3.spawnpoints)
            {
                p.Spawn();
            }
        }
        if (a == 8)
        {
            foreach (SpawnPoint p in player4.spawnpoints)
            {
                p.Spawn();
            }
        }
    } */
}

