using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    public Tower[] towers;
    [SerializeField] public PopUpManager popupManager;
    private GameVariables gameVariables;

    private int selectedTower = 0;
    public int placedTowerCount = 0;

    private void Awake()
    {
        main = this;
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
    }

    public Tower GetSelectedTower()
    {
        if (placedTowerCount < 10)
        {
            gameVariables.resourcesInfo.remainingTower = gameVariables.resourcesInfo.remainingTower - 1;
            return towers[selectedTower];
        }
            
        else return null;
    }

    public void SetSelectedTower(int _selectedTower)
    {
        if(placedTowerCount == 10)
        {
            popupManager.ShowMessage("Defender has reached the tower limit");
            return;
        }

        if (towers[_selectedTower].cost <= gameVariables.resourcesInfo.defenseMoney)
        {
            selectedTower = _selectedTower;

        }
        else
        {
            popupManager.ShowMessage("Not enough currency to spawn " + (towers[_selectedTower].name));
        }
    }
}
