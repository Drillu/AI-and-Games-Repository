using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public UnityEvent<Collider> OnTriggerEnter_;

    public UnityEvent<Collider> OnTriggerStay_;

    public UnityEvent<Collider> OnTriggerExit_;

    private void OnTriggerEnter(Collider collision)
    {
        OnTriggerEnter_.Invoke(collision);
    }

    private void OnTriggerStay(Collider collision)
    {
        OnTriggerStay_.Invoke(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        OnTriggerExit_.Invoke(collision);
    }
}
