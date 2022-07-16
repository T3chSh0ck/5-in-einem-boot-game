using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public TMP_InputField playername1, playername2, playername3, playername4;
    public GameController con;
    public Toggle toggle1, toggle2, toggle3, toggle4, rule;
    public TMP_Dropdown dropdown1, dropdown2, dropdown3, dropdown4;
    public Button submit;
    public GameObject playground, nature, menu, rulewindow, rules, ruleee;
    // Start is called before the first frame update
    void Start()
    {
        {
            playground.SetActive(false);
            nature.SetActive(false);
            rulewindow.SetActive(false);
            menu.SetActive(true);
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
                StoreName(0, playername1.text);
                break;
            case 1:
                StoreName(0, playername1.text);
                con.players[0].isAi = true;
                break;
        }
        switch (dropdown2.value)
        {
            case 0:
                StoreName(1, playername2.text);
                break;
            case 1:
                StoreName(1, playername2.text);
                con.players[1].isAi = true;
                break;
        }
        switch (dropdown3.value)
        {
            case 0:
                StoreName(2, playername3.text);
                break;
            case 1:
                StoreName(2, playername3.text);
                con.players[2].isAi = true;
                break;
        }
        switch (dropdown4.value)
        {
            case 0:
                StoreName(3, playername4.text);
                break;
            case 1:
                StoreName(3, playername4.text);
                con.players[3].isAi = true;
                break;
        }
        con.InitializeGame(a);
    }
    public void RuleButton()
    {
        if (rule.isOn)
        {
            ruleee.SetActive(true);
        }
        else
        {
            ruleee.SetActive(false);
        }
    }
    public void OnSubmit()
    {
        playground.SetActive(true);
        nature.SetActive(true);
        menu.SetActive(false);
        rulewindow.SetActive(true);
        ruleee.SetActive(false);
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
    public void StoreName(int playerNr, string name)
    {
        con.players[playerNr].nickname = name;
    }
}

