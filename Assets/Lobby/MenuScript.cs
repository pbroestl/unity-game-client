﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    public bool menuOpen = false;
    public GameObject whosOnlineMenu;
    private Object activeGuiObject;
    public GameObject statusContainer;
    public Text[] playerArray;
    public bool statusOpen;
    public Text player1box;
    private string name;
    private string score;
    public ShopPopUP shopPopUp;

    void awake(){
        statusOpen = false;
        playerArray = new Text[18];
        //shopPopUp.UpdateTextFields ();
    }
    void update(){
        // Debug.Log ("IN MENU SCRIPT");
    }

    public void OpenWhosOnline(){
        CloseAllMenus ();

        //Call method to request players 
        CurrentlyOnline online = this.GetComponent<CurrentlyOnline>();
        Debug.Log("Currently Online instantiated");
        online.requestOnlinePlayers(handleOnlinePlayers);
        Debug.Log ("You Pressed WHOS ONLINE?");
        //EventSystemManager sets this item to take priority over bckground objects (mouseEvents)
        EventSystem.current.SetSelectedGameObject(whosOnlineMenu);
        whosOnlineMenu.SetActive (true);
        menuOpen = true;
    }

    private void handleOnlinePlayers(Dictionary<int, Player> playerList) {
        //Display the onlinePlayer Response
        int i =0;
        foreach (KeyValuePair<int, Player> entry in playerList)
        {
            score = entry.Value.xp.ToString();
            name = entry.Value.name;
            playerArray [i].text = name + "            " +score;
            i++;
        }
    }

    public void OpenStatus() {
        CloseAllMenus ();
        statusContainer.SetActive (true);
        menuOpen = true;
        statusOpen = true;
    }

    public void OpenMiniGames() {
        CloseAllMenus ();
        Debug.Log ("You Pressed MiniGames");
        menuOpen = true;
    }

    public void OpenConvergence() {
        CloseAllMenus();
        Debug.Log("You Pressed Convergence");
        this.activeGuiObject =  (Object) gameObject.AddComponent <ConvergeGUI>();
        menuOpen=true;
    }

    public void OpenCardsfWild() {
        Debug.Log("You Pressed CardsOfWild");
        menuOpen=true;
    }

    public void OpenDontEatMe() {
        CloseAllMenus ();
        this.activeGuiObject =  (Object)gameObject.AddComponent <DontEatMeGUI>();
    }
    public void OpenMultiplayerGames() {
        CloseAllMenus();
        menuOpen = true;
        Debug.Log("You Pressed Open Multiplayer Games");
        this.activeGuiObject =  (Object) gameObject.AddComponent <MultiplayerGames>();
        this.enabled = false;
    }


    public void OpenClashOfSpecies() {
        CloseAllMenus ();
        this.activeGuiObject =  (Object) gameObject.AddComponent <ClashOfSpeciesGUI>();
        menuOpen=true;
    }
    

    public void CloseAllMenus() {
        if (this.activeGuiObject != null) {
          Destroy(this.activeGuiObject);
        }
        whosOnlineMenu.SetActive (false);
        statusContainer.SetActive(false);
        menuOpen = false;
        statusOpen = false;
    }

    public bool checkIfOpen() {
        Debug.Log ("STATUS WINDOW OPEN: "+statusOpen);
        return statusOpen;
    }
}
