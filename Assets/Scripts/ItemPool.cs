using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPool : MonoBehaviour
{
    private Inventory inventory;
    private CharacterHandler player;

    public List<ItemData> availableItems;
    public List<ItemData> unavailableItems;

    // an Option object holds the information of the item next level stats
    [System.Serializable]
    public class Option
    {
        public TextMeshProUGUI displayName;
        public TextMeshProUGUI displayDescription;
        public Image icon;
        public Button optionButton;
        public TextMeshProUGUI level;

        // static index to store
        [HideInInspector]
        public static int currentIndex = 0;
    }
    public List<Option> TableUI;

    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
        player = FindFirstObjectByType<CharacterHandler>();
    }

    public void ChooseItems()
    {
        List<ItemData> remainingItems = new(availableItems);
        List<ItemData> choosenItems = new();
        for (int i = 0; i < TableUI.Capacity && remainingItems.Count > 0; i++)
        {
            int randNum = Random.Range(0, remainingItems.Count);
            choosenItems.Add(remainingItems[randNum]);

            // Remove the selected item from remaining item list
            remainingItems.RemoveAt(randNum);
        }
        //Debug.Log($"ITEMS: {choosenItems.Count}");
        CreateItemTable(choosenItems);
    }

    public void CreateItemTable(List<ItemData> itemsData)
    {
        // Make a copy of the itemsData list so you don�t modify the original while iterating
        List<ItemData> remainingItems = new(itemsData);

        for (int a = 0; a < TableUI.Capacity; a++)
        {
            if (remainingItems.Count == 0) break; // Stop if no items are left

            // Select the first item from the remaining items
            ItemData itemData = remainingItems[0];

            // Check the item type and set up the UI accordingly
            if (itemData is WeaponData weaponData)
            {
                for (int i = 0; i < inventory.weaponSlots.Capacity; i++)
                {
                    TableUI[a].optionButton.onClick.RemoveAllListeners(); // Clear previous listeners to avoid listeners duplication
                    Weapon weapon = inventory.weaponSlots[i].item as Weapon;
                    if (weapon == null)
                    {
                        TableUI[a].optionButton.onClick.AddListener(() =>
                        {
                            player.AcquireWeapon(weaponData.controller);
                            GameManager.instance.EndLevelUpScreen();
                        });
                        TableUI[a].icon.sprite = weaponData.icon;
                        TableUI[a].displayName.text = weaponData.name;
                        TableUI[a].level.text = "*NEW*";
                        TableUI[a].displayDescription.text = weaponData.Descriptions[0];
                        TableUI[a].level.color = Color.yellow; // Yellow
                        break;
                    } else if (itemData.name == weapon.weaponData.name && inventory.weaponSlots[i] != null) // Check by name
                    {
                        TableUI[a].optionButton.onClick.AddListener(() =>
                        {
                            weapon.LevelUp();
                            GameManager.instance.EndLevelUpScreen();
                        });
                        TableUI[a].icon.sprite = weapon.weaponData.icon;
                        TableUI[a].displayName.text = weapon.weaponData.name;
                        TableUI[a].displayDescription.text = weapon.weaponData.Descriptions[weapon.currentLevel];
                        TableUI[a].level.text = $"Level {weapon.currentLevel + 1}"; // if not a new item then don't show *new*
                        TableUI[a].level.color = Color.black;
                        break;
                    }
                }
            } else if (itemData is PassiveItemData passiveItemData)
            {
                for (int i = 0; i < inventory.passiveItemSlots.Capacity; i++)
                {
                    TableUI[a].optionButton.onClick.RemoveAllListeners(); // Clear previous listeners to avoid listeners duplication
                    PassiveItem passiveItem = inventory.passiveItemSlots[i].item as PassiveItem;
                    if (passiveItem == null) // if the item is not in inventory yet
                    {
                        //Debug.Log($"new item iterator: {i}");
                        TableUI[a].optionButton.onClick.AddListener(() =>
                        {
                            //Debug.Log("adding");
                            player.AcquirePassiveItem(passiveItemData.prefab);
                            GameManager.instance.EndLevelUpScreen();
                        });
                        TableUI[a].icon.sprite = passiveItemData.icon;
                        TableUI[a].displayName.text = passiveItemData.name;
                        TableUI[a].level.text = "*NEW*";
                        TableUI[a].displayDescription.text = passiveItemData.Descriptions[0];
                        TableUI[a].level.color = Color.yellow; // Yellow
                        break;
                    } else if (itemData.name == passiveItem.passiveItemData.name && inventory.passiveItemSlots[i] != null) // if the item is in inventory
                    {
                        //Debug.Log($"existing item iterator: {i}");
                        TableUI[a].optionButton.onClick.AddListener(() =>
                        {
                            passiveItem.LevelUp();
                            GameManager.instance.EndLevelUpScreen();
                        });
                        TableUI[a].icon.sprite = passiveItem.passiveItemData.icon;
                        TableUI[a].displayName.text = passiveItem.passiveItemData.name;
                        TableUI[a].displayDescription.text = passiveItem.passiveItemData.Descriptions[passiveItem.currentLevel];
                        TableUI[a].level.text = $"Level {passiveItem.currentLevel + 1}"; // if not a new item then don't show *new*
                        TableUI[a].level.color = Color.black;
                        break;
                    }
                }
            }

            // Remove the selected item from the list to avoid duplicates
            remainingItems.RemoveAt(0);
        }
    }


}
