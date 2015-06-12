using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameplayScript : MonoBehaviour {
    public Transform peasantPrefab;
    public static Game currentGame;
    public static int playerOnTurn = 1;
    public static GameObject fieldPanel;
    public GameObject editorFieldPanel;
    public GameObject editorFriendlyCharacterStatsPanel;
    public GameObject editorEnemyCharacterStatsPanel;
    public static GameObject friendlyPanel;
    public static GameObject enemyPanel;
    public GameObject editorFriendlyLabel;
    public GameObject editorEnemyLabel;
    public static GameObject FriendlyLabel;
    public static GameObject EnemyLabel;
    public GameObject editorFriendlyHLabel;
    public GameObject editorEnemyHLabel;
    public static GameObject FriendlyHLabel;
    public static GameObject EnemyHLabel;
    public GameObject editorFriendlyDLabel;
    public GameObject editorEnemyDLabel;
    public static GameObject FriendlyDLabel;
    public static GameObject EnemyDLabel;
    public GameObject editorFriendlyALabel;
    public GameObject editorEnemyALabel;
    public static GameObject FriendlyALabel;
    public static GameObject EnemyALabel;
    public GameObject editorBlueUnitsLabel;
    public GameObject editorRedUnitsLabel;
    public static GameObject BlueUnitsLabel;
    public static GameObject RedUnitsLabel;
    public GameObject editorVictory;
    public GameObject editorDefeat;
    public static GameObject VictoryButton;
    public static GameObject DefeatButton;
    public GameObject editorHider;
    public static GameObject hiderPanel;
    public static Dictionary<string, GameObject> unitGameObjects = new Dictionary<string, GameObject>();
    public static int friendlyUnitsCount;
    public static int enemyUnitsCount;

	// Use this for initialization
	void Start () {
        fieldPanel = editorFieldPanel;
        friendlyPanel = editorFriendlyCharacterStatsPanel;
        enemyPanel = editorEnemyCharacterStatsPanel;
        FriendlyLabel = editorFriendlyLabel;
        EnemyLabel = editorEnemyLabel;
        FriendlyHLabel = editorFriendlyHLabel;
        EnemyHLabel = editorEnemyHLabel;
        FriendlyDLabel = editorFriendlyDLabel;
        EnemyDLabel = editorEnemyDLabel;
        FriendlyALabel = editorFriendlyALabel;
        EnemyALabel = editorEnemyALabel;
        BlueUnitsLabel = editorBlueUnitsLabel;
        RedUnitsLabel = editorRedUnitsLabel;
        VictoryButton = editorVictory;
        DefeatButton = editorDefeat;
        hiderPanel = editorHider;
        Player player1 = new Player("Pesheto", new UserControlled());
        player1.Gold = PlayerPrefs.GetInt("PlayerGold");
        player1.Units = UserDataScript.currentUserdata.getUserArmy();
        GameUtils.Shuffle<Creature>(player1.Units);
        Player player2 = new Player("NePesheto", new AIControlled());
        player2.Units = GameUtils.generateArmy();
        GameUtils.Shuffle<Creature>(player2.Units);
        currentGame = new Game(player1, player2);
        for (int i = 1; i <= player1.Units.Count; i++)
        {
            Vector3 position = newFieldPosition(1, i);
            GameObject newSoldier = Instantiate(peasantPrefab, position, Quaternion.Euler(new Vector3(0, 90, 0))) as GameObject;
            newSoldier.GetComponent<GameUnitScript>().currentUnit = new GameUnit(player1.Units[i - 1]);
            newSoldier.GetComponent<GameUnitScript>().currentUnit.X = 1;
            newSoldier.GetComponent<GameUnitScript>().currentUnit.Y = i;
            newSoldier.GetComponent<GameUnitScript>().currentUnit.Player = 1;
            string instanceId = newSoldier.GetInstanceID().ToString();
            newSoldier.GetComponent<GameUnitScript>().currentUnit.Id = instanceId;
            unitGameObjects.Add(instanceId, newSoldier);
            currentGame.Field[1,i] = newSoldier.GetComponent<GameUnitScript>().currentUnit;
        }
        for (int i = 1; i <= player2.Units.Count; i++)
        {
            Vector3 position = newFieldPosition(10, i);
            GameObject newSoldier = Instantiate(peasantPrefab, position, Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;
            newSoldier.GetComponent<GameUnitScript>().currentUnit = new GameUnit(player2.Units[i - 1]);
            newSoldier.GetComponent<GameUnitScript>().currentUnit.X = 10;
            newSoldier.GetComponent<GameUnitScript>().currentUnit.Y = i;
            newSoldier.GetComponent<GameUnitScript>().currentUnit.Player = 2;
            string instanceId = newSoldier.GetInstanceID().ToString();
            newSoldier.GetComponent<GameUnitScript>().currentUnit.Id = instanceId;
            unitGameObjects.Add(instanceId, newSoldier);
            currentGame.Field[10, i] = newSoldier.GetComponent<GameUnitScript>().currentUnit;
        }
        friendlyUnitsCount = player1.Units.Count;
        enemyUnitsCount = player2.Units.Count;
        BlueUnitsLabel.GetComponent<Text>().text = friendlyUnitsCount.ToString();
        RedUnitsLabel.GetComponent<Text>().text = enemyUnitsCount.ToString();
	}

    public static void showFriendlyUnitInfo(GameUnit unit)
    {
        FriendlyLabel.GetComponent<Text>().text = unit.Creature.Name;
        FriendlyALabel.GetComponent<Text>().text = unit.Creature.Damage.ToString();
        FriendlyDLabel.GetComponent<Text>().text = unit.Creature.Defence.ToString();
        FriendlyHLabel.GetComponent<Text>().text = unit.Creature.Health.ToString();
        friendlyPanel.SetActive(true);
    }

    public static void showEnemyUnitInfo(GameUnit unit)
    {
        EnemyLabel.GetComponent<Text>().text = unit.Creature.Name;
        EnemyALabel.GetComponent<Text>().text = unit.Creature.Damage.ToString();
        EnemyDLabel.GetComponent<Text>().text = unit.Creature.Defence.ToString();
        EnemyHLabel.GetComponent<Text>().text = unit.Creature.Health.ToString();
        enemyPanel.SetActive(true);
    }

    public static Vector3 newFieldPosition(int newX, int newY)
    {
        float X = ((newX - 1) * 2) + (-9);
        float Y = ((10 - newY) * 2) + (-9);
        return new Vector3(X, 0f, Y);
    }

    public static GameUnit selectedUnit;

    public void HidePanel()
    {
        fieldPanel.SetActive(false);
    }

    public void MoveButtonHandler()
    {
        moveGameUnit();
    }

    public static void moveGameUnit()
    {
        if ((selectedUnit.Creature.CanMove(selectedUnit.X, selectedUnit.Y, tempX, tempY)) && (currentGame.Field[tempX, tempY] == null))
        {
            GameObject objectToMove;
            unitGameObjects.TryGetValue(selectedUnit.Id, out objectToMove);
            objectToMove.transform.position = newFieldPosition(tempX, tempY);
            currentGame.Field[selectedUnit.X, selectedUnit.Y] = null;
            currentGame.Field[tempX, tempY] = selectedUnit;
            selectedUnit.X = tempX;
            selectedUnit.Y = tempY;
            fieldPanel.SetActive(false);
            changeTurn();
            selectedUnit = null;
        }
    }

    public void endGame()
    {
        Application.LoadLevel(0);
    }

    public static void changeTurn()
    {
        if (friendlyUnitsCount == 0)
        {
            hiderPanel.SetActive(true);
            DefeatButton.SetActive(true);
            UserDataScript.currentUserdata.gold -= 100;
            PlayerPrefs.SetInt("PlayerGold", UserDataScript.currentUserdata.gold);
        }
        else if (enemyUnitsCount == 0)
        {
            hiderPanel.SetActive(true);
            VictoryButton.SetActive(true);
            UserDataScript.currentUserdata.gold += 200;
            PlayerPrefs.SetInt("PlayerGold", UserDataScript.currentUserdata.gold);
        }
        if (playerOnTurn == 1)
        {
            playerOnTurn = 2;
        }
        else
        {
            playerOnTurn = 1;
        }
        enemyPanel.SetActive(false);
        selectedUnit = null;
        friendlyPanel.SetActive(false);
    }

    public void AttackButtonHandler()
    {
        attackUnit();
    }

    public static void attackUnit()
    {
        if ((currentGame.Field[tempX, tempY] != null) && (currentGame.Field[tempX, tempY].Player != playerOnTurn))
        {
            if (selectedUnit.Creature.CanAttack(selectedUnit.X, selectedUnit.Y, tempX, tempY))
            {
                currentGame.Field[tempX, tempY].Creature.TakeDamage(selectedUnit.Creature.Attack());
                if (currentGame.Field[tempX, tempY].Creature.Health <= 0)
                {
                    GameObject objectToDestroy;
                    unitGameObjects.TryGetValue(currentGame.Field[tempX, tempY].Id, out objectToDestroy);
                    objectToDestroy.SetActive(false);
                    if (currentGame.Field[tempX, tempY].Player == 1)
                    {
                        currentGame.Player1.Units.Remove(currentGame.Field[tempX, tempY].Creature);
                        friendlyUnitsCount--;
                    }
                    else
                    {
                        currentGame.Player2.Units.Remove(currentGame.Field[tempX, tempY].Creature);
                        enemyUnitsCount--;
                    }
                    enemyPanel.SetActive(false);
                    currentGame.Field[tempX, tempY] = null;
                    BlueUnitsLabel.GetComponent<Text>().text = friendlyUnitsCount.ToString();
                    RedUnitsLabel.GetComponent<Text>().text = enemyUnitsCount.ToString();
                }
                else
                {
                    showEnemyUnitInfo(currentGame.Field[tempX, tempY]);
                }
                fieldPanel.SetActive(false);
                selectedUnit = null;
                changeTurn();
            }
        }
    }

    public static int tempX;
    public static int tempY;

    public static void fieldButtonPress(int x, int y)
    {
        if (playerOnTurn == 1)
        {
            if (selectedUnit != null)
            {
                if (currentGame.Field[x, y] != null)
                {
                    if (currentGame.Field[x, y].Player != 1)
                    {
                        showEnemyUnitInfo(currentGame.Field[x, y]);
                        tempX = x;
                        tempY = y;
                        fieldPanel.SetActive(true);
                        Vector3 panelPosition = newFieldPosition(x, y);
                        panelPosition.y = 2.5f;
                        fieldPanel.transform.position = panelPosition;
                    }
                    else
                    {
                        enemyPanel.SetActive(false);
                    }
                }
                else
                {
                    tempX = x;
                    tempY = y;
                    fieldPanel.SetActive(true);
                    enemyPanel.SetActive(false);
                    Vector3 panelPosition = newFieldPosition(x, y);
                    panelPosition.y = 2.5f;
                    fieldPanel.transform.position = panelPosition;
                }
            }
            if ((currentGame.Field[x, y] != null) && (currentGame.Field[x, y].Player == 1)) 
            {
                selectedUnit = currentGame.Field[x, y];
                showFriendlyUnitInfo(selectedUnit);
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if(playerOnTurn == 2)
        {
            currentGame.Player2.makeTurn();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }
	}
}
