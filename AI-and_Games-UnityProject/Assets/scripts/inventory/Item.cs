using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int ID
    {
        get
        {
            return _id;
        }
    }

    [SerializeField]
    private int _id;

    public string itemName;

    public string itemDesc;

    public Sprite itemIcon;

    public int stackSize;
}
