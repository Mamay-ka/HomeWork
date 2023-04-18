using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehaviour : MonoBehaviour, IManager
{
    
    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;

    public Stack<string> lootStack = new Stack<string>();

    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public bool showLostScreen = false;
    public bool showWinScreen = false;


    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;
    private int _itemCollected = 0;


    public int ItemCollected
    {
        get { return _itemCollected; }
        set
        {
            _itemCollected = value;
            Debug.LogFormat("Items : {0}", _itemCollected);

            if (_itemCollected >= maxItems)
            {

                LoseWinCondition("You collected all the items!", showWinScreen = true);
            }
            else
            {
                labelText = "Items found only " + (maxItems - _itemCollected) + "more to go!";
            }
        }
    }

    private int _playerHP = 3;
    public int PlayerHP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives : {0}", _playerHP);

            if (_playerHP <= 0)
            {

                LoseWinCondition("You want another life with that?", showLostScreen = true);

            }
            else
            {
                labelText = "Ouch... that’s got hurt.";
            }
        }
    }

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.Item);

        GameObject player = GameObject.Find("Player");
        PlayerBehaviour playerBehavior = player.GetComponent<PlayerBehaviour>();
        playerBehavior.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }

    public void Initialize()
    {
        _state = "Manager initialized..";
        debug(_state);
        _state.FancyDebug();

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mithril Bracers");

        LogWithDelegate(debug);

    }

    private void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player health: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items collected: " + _itemCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50,
                200, 100), "YOU WON!"))
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level startet sucsesfully...");
                }
                catch(System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " + e.ToString());
                }
                finally
                {
                    debug("Restart handled...");
                }
            
        }

        if (showLostScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50,
             200, 100), "YOU LOSE!"))
            {
                //RestertLevel();
                Utilities.RestartLevel(0);
            }
        }
    }

    /*void RestertLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }*/

    private void LoseWinCondition(string labelLoseWin, bool screen)
    {
        labelText = labelLoseWin;

        Time.timeScale = 0;
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("You got a {0}! You`ve got a chance of finding a {1} next", currentItem, nextItem);

        Debug.LogFormat("There are {0} random loot items waiting for you", lootStack.Count);

    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    

}
       

    
    

