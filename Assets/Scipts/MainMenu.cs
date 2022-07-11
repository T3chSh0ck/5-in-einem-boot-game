using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public GameObject menu, menu1, menu2, menu3; 
    public Button button1, button2, button3, button4, choosecolorblue, choosecolorgreen, choosecolorred, choosecoloryellow, submit;
    public Player player1, player2, player3, player4;
    public TMP_InputField playername;
    // Start is called before the first frame update
    void Start()
    {
        {
            submit.onClick.AddListener(()=> StoreName());
            menu2.SetActive(false);
            menu3.SetActive(false);
            button1.onClick.AddListener(() => Spawner(1));
            button2.onClick.AddListener(() => Spawner(2));
            button3.onClick.AddListener(() => Spawner(3));
            button4.onClick.AddListener(() => Spawner(4));
            choosecolorblue.onClick.AddListener(() => Spawner(5));
            choosecolorgreen.onClick.AddListener(() => Spawner(6));
            choosecolorred.onClick.AddListener(() => Spawner(7));
            choosecoloryellow.onClick.AddListener(() => Spawner(8));

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StoreName()
    {
        Debug.Log(playername.text);
    }

    public void Spawner(int a)
    {
        menu1.SetActive(false);
        menu2.SetActive(true);
        if (a == 5)
           
        {

            foreach (SpawnPoint p in player1.spawnpoints)
            {
                p.Spawn();

            }
            menu2.SetActive(false);
            menu3.SetActive(true);


        }
        if (a == 6)
        {
            foreach (SpawnPoint p in player2.spawnpoints)
            {
                p.Spawn();
            }
            menu2.SetActive(false);

        }
        if (a == 7)
        {
            foreach (SpawnPoint p in player3.spawnpoints)
            {
                p.Spawn();
            }
            menu2.SetActive(false);
        }
        if (a == 8)
        {
            foreach (SpawnPoint p in player4.spawnpoints)
            {
                p.Spawn();
            }
            menu2.SetActive(false);
        }
    } 
}

