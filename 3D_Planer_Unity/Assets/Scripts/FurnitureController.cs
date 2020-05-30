using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    /**
     * Der FurnitureContorller wird für alle Möbel verwendet um ein einheitliches Verhalten zu gewähleisten.
     */
    public GameObject obj;

    private GameObject player;

    public void InstanciateObject()
    {
        if (player == null) SetPlayer();

        // positioniert das Objekt vor dem Player
        var pos = player.transform.position + player.transform.forward * 2 + Vector3.up * 0.5f;
        Instantiate(obj, pos, player.transform.rotation);
    }

    private void SetPlayer()
    {
        player = GameObject.Find("Player");
    }
}