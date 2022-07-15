using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public TMP_InputField playername1, playernam2, playername3, playername4;
    public GameController con;
    public Toggle toggle1, toggle2, toggle3, toggle4;
    public TMP_Dropdown dropdown1, dropdown2, dropdown3, dropdown4;
    public Button submit;
    // Start is called before the first frame update
    void Start()
    {
        {
            GameObject.Find("Playing Field").SetActive(false);
            GameObject.Find("Nature").SetActive(false);
            
            submit.onClick.AddListener(OnSubmit);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayerKIOut(bool[] a)
    {
        switch (dropdown1.value)
        {
            case 0:
                StoreName();
                break;
            case 1:
                break;
        }
        switch (dropdown1.value)
        {
            case 0:
                StoreName();
                break;
            case 1:
                break;
        }
        switch (dropdown1.value)
        {
            case 0:
                StoreName();
                break;
            case 1:
                break;
        }
        switch (dropdown1.value)
        {
            case 0:
                StoreName();
                break;
            case 1:
                break;
        }
        con.InitializeGame(a);
    }
    public void OnSubmit()
    {
        IsOn();
    }
    public void IsOn()
    {
        bool[] playersaktiv = new bool[3];
        if (toggle1.isOn)
        {
            playersaktiv[0] = true;                
        }
        if (toggle2.isOn)
        {
            playersaktiv[1] = true;
        }
        if (toggle3.isOn)
        {
            playersaktiv[2] = true;;
        }
        if (toggle4.isOn)
        {
            playersaktiv[3] = true;       
        }
        PlayerKIOut(playersaktiv);
    }
    public void DoSomething()
    {
        Debug.Log("HUSO");
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
        PlayerPrefs.SetString("username",playername1.text);
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

