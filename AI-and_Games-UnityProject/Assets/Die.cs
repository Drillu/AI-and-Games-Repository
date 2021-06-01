using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    public LayerMask DieOnCollide;
    public void ForceDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == DieOnCollide)
        {
            ForceDie();
        }
    }
}
