using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public TMP_InputField playername1, playername2, playername3, playername4;
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
        bool[] playersaktiv = new bool[4];
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
    public void StoreName()
    {
        PlayerPrefs.SetString("username",playername1.text);
    }
}

