using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public Item item;

    public int amount;

    public UnityEvent OnItemPickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inv = collision.GetComponent<Inventory>();

        if (inv != null)
        {
            int remains = inv.AddItem(item, amount);
            UIManager.Instance.AddInfo($"You picked up {amount - remains} from {item.itemName}.");
            OnItemPickedUp.Invoke();
            amount = remains;
        }

        if (amount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
