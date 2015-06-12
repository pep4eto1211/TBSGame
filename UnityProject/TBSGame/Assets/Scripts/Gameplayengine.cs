using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Creature
{
    protected int damage;
    protected int defence;
    protected int health;
    protected int mana;
    protected int stamina;
    protected int critChance;
    protected int attackRange;
    protected string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Attack()
    {
        if (Random.Range(1, 101) <= this.critChance)
        {
            return damage * 2;
        }
        else
        {
            return damage;
        }
    }
    public bool CanMove(int sourceX, int sourceY, int targetX, int targetY)
    {
        if (((Mathf.Abs(targetX - sourceX)) <= this.stamina) && ((Mathf.Abs(targetY - sourceY)) <= this.stamina))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanAttack(int sourceX, int sourceY, int targetX, int targetY)
    {
        if (((Mathf.Abs(targetX - sourceX)) <= this.attackRange) && ((Mathf.Abs(targetY - sourceY)) <= this.attackRange))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (defence >= damage)
        {
            defence -= damage;
        }
        else
        {
            damage -= defence;
            health -= damage;

        }
    }
    public int CritChance
    {
        get { return critChance; }
        set { critChance = value; }
    }

    protected int AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    public int Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Defence
    {
        get { return defence; }
        set { defence = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
}

public class Hero
{
    private int damage;
    private int defence;
    private int health;
    private int mana;
    private int critChance;

    public int Attack()
    {
        if (Random.Range(1, 101) < this.critChance)
        {
            return damage * 2;
        }
        else
        {
            return damage;
        }
    }

    public int CritChance
    {
      get { return critChance; }
      set { critChance = value; }
    }
    
    public int Mana
    {
      get { return mana; }
      set { mana = value; }
    }
    
    public int Health
    {
      get { return health; }
      set { health = value; }
    }
    
    public int Defence
    {
      get { return defence; }
      set { defence = value; }
    }
    
    public int Damage
    {
      get { return damage; }
      set { damage = value; }
    }
}

public class Peasant : Creature
{
    public Peasant()
    {
        this.stamina = 3;
        this.damage = 1;
        this.defence = 2;
        this.health = 6;
        this.critChance = 2;
        this.name = "Peasant";
        this.attackRange = 1;
    }
}

public class Footman : Creature
{
    public Footman()
    {
        this.stamina = 4;
        this.damage = 3;
        this.defence = 4;
        this.health = 8;
        this.critChance = 10;
        this.name = "Footman";
        this.attackRange = 1;
    }
}

public class Archer : Creature
{
    public Archer()
    {
        this.stamina = 3;
        this.damage = 2;
        this.defence = 3;
        this.health = 8;
        this.critChance = 7;
        this.name = "Archer";
        this.attackRange = 11;
    }
}

public class Griffin : Creature
{
    public Griffin()
    {
        this.stamina = 5;
        this.damage = 4;
        this.defence = 1;
        this.health = 10;
        this.critChance = 12;
        this.name = "Griffin";
        this.attackRange = 2;
    }
}

public class Player
{
    private string name;
    private int gold = 300;
    private List<Creature> units = new List<Creature>();
    private GameController playerController;

    public Player(string Name, GameController controller)
    {
        this.name = Name;
        this.playerController = controller;
    }

    public void makeTurn()
    {
        playerController.makeMove();
    }

    public GameController PlayerController
    {
        get { return playerController; }
        set { playerController = value; }
    }

    public List<Creature> Units
    {
        get { return units; }
        set { units = value; }
    }

    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}

public class Shop
{
    public Dictionary<string, int> shopItems = new Dictionary<string,int>();
    public Shop()
    {
        shopItems.Add("Peasant", 30);
        shopItems.Add("Footman", 90);
        shopItems.Add("Archer", 50);
        shopItems.Add("Griffin", 150);
    }
}

public class Game
{
    private Player player1;
    private Player player2;
    private GameUnit[,] field = new GameUnit[11, 11];

    public Game(Player player1, Player player2)
    {
        this.player1 = player1;
        this.player2 = player2;
    }

    public Player Player2
    {
        get { return player2; }
        set { player2 = value; }
    }

    public Player Player1
    {
        get { return player1; }
        set { player1 = value; }
    }

    public GameUnit[,] Field
    {
        get { return field; }
        set { field = value; }
    }
}

public interface GameController
{
    void makeMove();
}

public class UserControlled : GameController
{
    public void makeMove()
    {

    }
}

public class AIControlled : GameController
{
    public void makeMove()
    {
        bool isRunning = true;
        for (int i = 1; i <= 10; i++)
        {
            if (!isRunning)
            {
                break;
            }
            for (int j = 1; j <= 10; j++)
            {
                if (GameplayScript.currentGame.Field[i, j] != null)
                {
                    if (GameplayScript.currentGame.Field[i, j].Player == 2)
                    {
                        if ((i > 1) && (GameplayScript.currentGame.Field[i - 1, j] == null))
                        {
                            GameplayScript.selectedUnit = GameplayScript.currentGame.Field[i, j];
                            GameplayScript.tempX = i - 1;
                            GameplayScript.tempY = GameplayScript.currentGame.Field[i, j].Y;
                            GameplayScript.moveGameUnit();
                            isRunning = false;
                            break;
                        }
                        else
                            if ((i > 1) && (GameplayScript.currentGame.Field[i - 1, j].Player == 1))
                            {
                                GameplayScript.selectedUnit = GameplayScript.currentGame.Field[i, j];
                                GameplayScript.tempX = i - 1;
                                GameplayScript.tempY = GameplayScript.currentGame.Field[i, j].Y;
                                GameplayScript.attackUnit();
                                isRunning = false;
                                break;
                            }
                    }
                }
            }
        }
        if (isRunning)
        {
            GameplayScript.changeTurn();
        }
    }
}

public class GameUnit
{
    private Creature creature;
    private int x;
    private int y;
    private int player;
    private string id;

    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public int Player
    {
        get { return player; }
        set { player = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public GameUnit(Creature creature)
    {
        this.creature = creature;
    }

    public Creature Creature
    {
        get { return creature; }
        set { creature = value; }
    }
}

public static class GameUtils
{
    public static List<Creature> generateArmy()
    {
        List<Creature> toReturn = new List<Creature>();
        for (int i = 0; i < 3; i++)
        {
            toReturn.Add(new Peasant());
        }
        for (int i = 0; i < 3; i++)
        {
            toReturn.Add(new Footman());
        }
        for (int i = 0; i < 3; i++)
        {
            toReturn.Add(new Archer());
        }
        toReturn.Add(new Griffin());

        return toReturn;
    }

    public static void Shuffle<T>(this List<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) 
        {  
            n--;  
            int k = Random.Range(0, n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}