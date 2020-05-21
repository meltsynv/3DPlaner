using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    /**
     * Der FurnitureContorller wird für alle Möbel verwendet um ein einheitliches Verhalten zu gewähleisten.
     */
    public GameObject obj;


    public void InstanciateObject()
    {
        Instantiate(obj);
    }
}