using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public TMP_InputField playername1, playername2, playername3, playername4;
    public GameController con;
    public Toggle rule;
    public Toggle[] toggles;
    public TMP_Dropdown[] dropdowns;
    public Button submit;
    public GameObject playground, nature, menu, rulewindow, rules, ruleee, errortextfield;
    void Start()
    {
        {
            playground.SetActive(false);
            nature.SetActive(false);
            rulewindow.SetActive(false);
            menu.SetActive(true);
            errortextfield.SetActive(false);
        }
    }
    public void PlayerKIOut(bool[] a)
    {
        switch (dropdowns[0].value)
        {
            case 0:
                StoreName(0, playername1.text);
                break;
            case 1:
                StoreName(0, playername1.text);
                con.players[0].isAi = true;
                break;
        }
        switch (dropdowns[1].value)
        {
            case 0:
                StoreName(1, playername2.text);
                break;
            case 1:
                StoreName(1, playername2.text);
                con.players[2].isAi = true;
                break;
        }
        switch (dropdowns[2].value)
        {
            case 0:
                StoreName(2, playername3.text);
                break;
            case 1:
                StoreName(2, playername3.text);
                con.players[3].isAi = true;
                break;
        }
        switch (dropdowns[3].value)
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
        int togglerson = 0;
        foreach (Toggle t in toggles)
        {
            if (t.isOn)
            {
                togglerson++;
            }
        }
        int dropdownbot = 0;
        foreach (TMP_Dropdown d in dropdowns)
        {
            if (d.value == 1)
            {
                dropdownbot++;
            }
        }
        if (togglerson >= 2)
        {
            if(dropdownbot <= 3)
            {
                playground.SetActive(true);
                nature.SetActive(true);
                menu.SetActive(false);
                rulewindow.SetActive(true);
                ruleee.SetActive(false);

                IsOn();
            }
            else
            {
                StartCoroutine(DisplayErrors(2));
            }
        }
        else
        {
            StartCoroutine(DisplayErrors(1));
        }
    }
    IEnumerator DisplayErrors(int error)
    {
        errortextfield.SetActive(true);
        switch (error)
        {
            case 1:
                errortextfield.GetComponent<TMP_Text>().text = "Bitte wählen Sie mehr als einen Spieler aus!";
                break;
            case 2:
                errortextfield.GetComponent<TMP_Text>().text = "Sie können das Spiel nicht nur mit Computergegnern starten!";
                break;
        }
        yield return new WaitForSeconds(2f);
        errortextfield.SetActive(false);
    }
    public void IsOn()
    {        
        bool[] playersaktiv = new bool[4];
        if (toggles[0].isOn)
        {
            playersaktiv[0] = true;
        }
        if (toggles[1].isOn)
        {
            playersaktiv[1] = true;
        }
        if (toggles[2].isOn)
        {
            playersaktiv[2] = true;
        }
        if (toggles[3].isOn)
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