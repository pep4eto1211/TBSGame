using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mainmenuScript : MonoBehaviour {
    public GameObject shopPanel;
    public GameObject peasantPrice;
    public GameObject footmanPrice;
    public GameObject archerPrice;
    public GameObject griffinPrice;
    public GameObject goldLabel;
    Shop currentShop;

	// Use this for initialization
	void Start () {
        if (UserDataScript.currentUserdata.isFirstGame)
        {
            PlayerPrefs.SetInt("PlayerGold", 300);
            UserDataScript.currentUserdata.isFirstGame = false;
        }
        currentShop = new Shop();
        if (!PlayerPrefs.HasKey("PlayerGold"))
        {
            PlayerPrefs.SetInt("PlayerGold", 300);
            UserDataScript.currentUserdata.gold = 300;
        }
        else
        {
            UserDataScript.currentUserdata.gold = PlayerPrefs.GetInt("PlayerGold");
        }

        goldLabel.GetComponent<Text>().text = UserDataScript.currentUserdata.gold.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startGameButtonClick()
    {
        if (UserDataScript.currentUserdata.userArmy.Count > 0)
        {
            Application.LoadLevel(1);
            UserDataScript.currentUserdata.gold += UserDataScript.currentUserdata.spendings;
            UserDataScript.currentUserdata.spendings = 0;
        }
    }

    public void quitGameButtonClick()
    {
        Application.Quit();
    }

    public void shopButtonclick()
    {
        int price;
        currentShop.shopItems.TryGetValue("Peasant", out price);
        peasantPrice.GetComponent<Text>().text = price.ToString() + " G";
        currentShop.shopItems.TryGetValue("Footman", out price);
        footmanPrice.GetComponent<Text>().text = price.ToString() + " G";
        currentShop.shopItems.TryGetValue("Archer", out price);
        archerPrice.GetComponent<Text>().text = price.ToString() + " G";
        currentShop.shopItems.TryGetValue("Griffin", out price);
        griffinPrice.GetComponent<Text>().text = price.ToString() + " G";
        shopPanel.SetActive(true);
    }

    public void closeShopPanelButtonClick()
    {
        shopPanel.SetActive(false);
    }

    public bool canAddMoreUnits()
    {
        if (UserDataScript.currentUserdata.userArmy.Count < 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void buyPeasantButtonClick()
    {
        int currentPlayerGold = PlayerPrefs.GetInt("PlayerGold");
        int unitPrice;
        currentShop.shopItems.TryGetValue("Peasant", out unitPrice);
        if ((currentPlayerGold >= unitPrice) && canAddMoreUnits())
        {
            UserDataScript.currentUserdata.userArmy.Add(new Peasant());
            PlayerPrefs.SetInt("PlayerGold", currentPlayerGold - unitPrice);
            UserDataScript.currentUserdata.gold = currentPlayerGold - unitPrice;
            UserDataScript.currentUserdata.spendings += unitPrice;
            goldLabel.GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerGold").ToString() + " G";
        }
    }

    public void buyFootmanButtonClick()
    {
        int currentPlayerGold = PlayerPrefs.GetInt("PlayerGold");
        int unitPrice;
        currentShop.shopItems.TryGetValue("Footman", out unitPrice);
        if ((currentPlayerGold >= unitPrice) && canAddMoreUnits())
        {
            UserDataScript.currentUserdata.userArmy.Add(new Footman());
            PlayerPrefs.SetInt("PlayerGold", currentPlayerGold - unitPrice);
            UserDataScript.currentUserdata.gold = currentPlayerGold - unitPrice;
            UserDataScript.currentUserdata.spendings += unitPrice;
            goldLabel.GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerGold").ToString() + " G";
        }
    }

    public void buyArcherButtonClick()
    {
        int currentPlayerGold = PlayerPrefs.GetInt("PlayerGold");
        int unitPrice;
        currentShop.shopItems.TryGetValue("Archer", out unitPrice);
        if ((currentPlayerGold >= unitPrice) && canAddMoreUnits())
        {
            UserDataScript.currentUserdata.userArmy.Add(new Archer());
            PlayerPrefs.SetInt("PlayerGold", currentPlayerGold - unitPrice);
            UserDataScript.currentUserdata.gold = currentPlayerGold - unitPrice;
            UserDataScript.currentUserdata.spendings += unitPrice;
            goldLabel.GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerGold").ToString() + " G";
        }
    }

    public void buyGriffinButtonClick()
    {
        int currentPlayerGold = PlayerPrefs.GetInt("PlayerGold");
        int unitPrice;
        currentShop.shopItems.TryGetValue("Griffin", out unitPrice);
        if ((currentPlayerGold >= unitPrice) && canAddMoreUnits())
        {
            UserDataScript.currentUserdata.userArmy.Add(new Griffin());
            PlayerPrefs.SetInt("PlayerGold", currentPlayerGold - unitPrice);
            UserDataScript.currentUserdata.gold = currentPlayerGold - unitPrice;
            UserDataScript.currentUserdata.spendings += unitPrice;
            goldLabel.GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerGold").ToString() + " G";
        }
    }
}
