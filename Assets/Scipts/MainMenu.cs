using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public TMP_InputField playername1, playername2, playername3, playername4;
    public TMP_Text activePlayerInfo, winnerDisplay;
    public GameController con;
    public Toggle rule;
    public Toggle[] toggles;
    public TMP_Dropdown[] dropdowns;
    public Button submit;
    public GameObject playground, nature, menu, rulewindow, victoryWindow, rules, ruleee, errortextfield, playerinfo;
    void Start()
    {
        {
            playground.SetActive(false);
            nature.SetActive(false);
            rulewindow.SetActive(false);
            victoryWindow.SetActive(false);
            menu.SetActive(true);
            errortextfield.SetActive(false);
        }
    }
    public void PlayerKIOut(bool[] a)
    {
        /*
        Description:
            checks if player or AI was selected and checks corresponding name input

        Parameters: 
            boo[] a: Array of Players depending on toggles Input

        Returns: N/A
        */
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
                con.players[1].isAi = true;
                break;
        }
        switch (dropdowns[2].value)
        {
            case 0:
                StoreName(2, playername3.text);
                break;
            case 1:
                StoreName(2, playername3.text);
                con.players[2].isAi = true;
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
        /*
        Description:
            Displays the GameRules, depending on Toggle.isOn

        Parameters: N/A

        Returns: N/A
        */
        if (rule.isOn)
        {
            ruleee.SetActive(true);
            playerinfo.SetActive(false);
        }
        else
        {
            ruleee.SetActive(false);
            playerinfo.SetActive(true);
        }
    }
    public void OnSubmit()
    {
        /*
        Description:
        Starts the Game on ButtonClick, checks Input

        Parameters: N/A

        Returns: int error
        */
        int dropdownplayer = 0;
        int activePlayers = 0;
        for (int i = 0; i < toggles.Length; i++)
        {
            if(toggles[i].isOn){
                activePlayers++;
                if(dropdowns[i].value == 0){
                    dropdownplayer++;
                }
            }         
        }
        //Checks if the amount of Players is viable
        if (activePlayers > 1)
        {
            //Checks whether one or more players are human
            if (dropdownplayer > 0)
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
        /*
        Description:
            Shows ErrorText depending on Input

        Parameters: 
            int erros: Number of InputError

        Returns: N/A
        */
        errortextfield.SetActive(true);
        switch (error)
        {
            case 1:
                errortextfield.GetComponent<TMP_Text>().text = "Bitte waehlen Sie mehr als einen Spieler aus!";
                break;
            case 2:
                errortextfield.GetComponent<TMP_Text>().text = "Sie koennen das Spiel nicht nur mit Computergegnern starten!";
                break;
        }
        yield return new WaitForSeconds(2f);
        errortextfield.SetActive(false);
    }
    public void IsOn()
    {
        /*
        Description:
            Shows which toogle is active, creates bool[] and fills it with appropriate inputs 

        Parameters: N/A

        Returns: N/A
        */
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
        /*
        Description:
            Saves the name input of the input fields according to the player number.

        Parameters: 
            int playerNr: Player number
            string name: Players names

        Returns: N/A
        */
        if (name != null && name != ""){
            con.players[playerNr].nickname = name;
        }else{
            con.players[playerNr].nickname = (playerNr + 1) +"";
        }
        
    }
    public void SetPlayerActiveText(string name, Color col)
    {
        /*
        Description:
            Shows which player's turn it is

        Parameters: 
            string name: name of each player
            Color col: color of each player
        Returns: N/A
        */
        activePlayerInfo.text = "Spieler " + name + " ist am Zug";
        activePlayerInfo.color = col;
    }
    public void AndTheWinnerIs(string name, Color col)
    {
        /*
        Description:
            Displays the Winners name

        Parameters: 
            string name: name of each player
            Color col: color of each player
        Returns: N/A
        */
        winnerDisplay.text = "Spieler " + name + " hat gewonnen!";
        winnerDisplay.color = col;
        rulewindow.SetActive(false);
        victoryWindow.SetActive(true);
    }
}