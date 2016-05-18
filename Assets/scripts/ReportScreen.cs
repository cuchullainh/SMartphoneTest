using UnityEngine;
using System.Collections;

public class ReportScreen : MonoBehaviour {

    GameObject topTier;
    GameObject upgradeHarborSelect;
    GameObject repairUpgradeSelect;
    GameObject tierUpgrades;
    GameObject shipRepair;
    GameObject billSelect;
    GameObject reportScreen;

    void Start ()
    {
        reportScreen = GameObject.Find("ReportScreen");
        topTier = GameObject.Find("ButtonsTopSwitch");
        upgradeHarborSelect = GameObject.Find("UpgradeHarborSelection");
        repairUpgradeSelect = GameObject.Find("RepairHArborSelection");
        tierUpgrades = GameObject.Find("TierUpgrades");
        shipRepair = GameObject.Find("SelectShipRepair");
        billSelect = GameObject.Find("SelectBillsToPAy");

        upgradeHarborSelect.SetActive(false);
        repairUpgradeSelect.SetActive(false);
        tierUpgrades.SetActive(false);
        shipRepair.SetActive(false);
        billSelect.SetActive(false);

        
        reportScreen.SetActive(false);
    }

   public void Report()
    {
        Time.timeScale = 0;
        reportScreen.SetActive(true);
    }

    public void EndReport()
    {
        Time.timeScale = 1;
        reportScreen.SetActive(false);
    }


    void Update ()
    {
	
	}

    public void Back()
    {
        topTier.SetActive(true);
        upgradeHarborSelect.SetActive(false);
        repairUpgradeSelect.SetActive(false);
        tierUpgrades.SetActive(false);
        shipRepair.SetActive(false);
        billSelect.SetActive(false);
    }

    public void GoToUpradeHarborSelect()
    {
        topTier.SetActive(false);
        upgradeHarborSelect.SetActive(true);
        repairUpgradeSelect.SetActive(false);
        tierUpgrades.SetActive(false);
        shipRepair.SetActive(false);
        billSelect.SetActive(false);
    }

    public void GoToUpradeTierSelect()
    {
        topTier.SetActive(false);
        upgradeHarborSelect.SetActive(false);
        repairUpgradeSelect.SetActive(false);
        tierUpgrades.SetActive(true);
        shipRepair.SetActive(false);
        billSelect.SetActive(false);
    }

    public void GoToRepairHarborSelect()
    {
        topTier.SetActive(false);
        upgradeHarborSelect.SetActive(false);
        repairUpgradeSelect.SetActive(true);
        tierUpgrades.SetActive(false);
        shipRepair.SetActive(false);
        billSelect.SetActive(false);
    }

    public void GoToRepairShipSelect()
    {
        topTier.SetActive(false);
        upgradeHarborSelect.SetActive(false);
        repairUpgradeSelect.SetActive(false);
        tierUpgrades.SetActive(false);
        shipRepair.SetActive(true);
        billSelect.SetActive(false);
    }

    public void GoToBillSelect()
    {
        topTier.SetActive(false);
        upgradeHarborSelect.SetActive(false);
        repairUpgradeSelect.SetActive(false);
        tierUpgrades.SetActive(false);
        shipRepair.SetActive(false);
        billSelect.SetActive(true);
    }
}
