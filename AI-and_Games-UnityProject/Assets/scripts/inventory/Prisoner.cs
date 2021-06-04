using UnityEngine;

public class Prisoner : MonoBehaviour
{
    private bool isDialogActive = false;

    [Header("Interaction Settings")]
    public KeyCode dialogKey;

    public Item requiredItem;
    public int requiredAmount;
    public Item rewardItem;
    public int rewardAmount;

    [Header("Vision Settings")]
    [SerializeField]
    private GameObject visionGO;
    [HideInInspector]
    public float range;

    private Inventory currentInv;

    private void Start()
    {
        visionGO.transform.localScale = new Vector3(range, range, range);
    }

    private void Update()
    {
        if (isDialogActive)
        {
            if (Input.GetKeyDown(dialogKey))
            {
                ConfirmTrade();
            }
        }
    }

    private void ConfirmTrade()
    {
        var res = currentInv.RemoveItem(requiredItem, requiredAmount);
        if (res == 0)
        {
            currentInv.AddItem(rewardItem, rewardAmount);

            UIManager.Instance.AddInfo("Trade was successful!\n" + 
                    "Goodbye!");
        }
        else
        {
            UIManager.Instance.AddInfo($"Ohh no, you need {res} more {requiredItem.itemName}!\n" + 
                    "Come back later!");
        }
    }

    public void NewPlayerNearby(Collider collider)
    {
        if (collider.tag == "Player")
        {
            isDialogActive = true;

            currentInv = collider.GetComponent<Inventory>();

            UIManager.Instance.AddInfo("Hello prisonner!\n" +
                   $"I need {requiredAmount} {requiredItem.itemName} for {rewardAmount} {rewardItem.itemName}\n" +
                   $"Are you in? Press {dialogKey} to accept.");
        }
    }

    public void NearbyPlayerLeft(Collider collider)
    {
        if (collider.tag == "Player")
        {
            UIManager.Instance.AddInfo("Goodbye player.");

            isDialogActive = false;
        }
    }
}
