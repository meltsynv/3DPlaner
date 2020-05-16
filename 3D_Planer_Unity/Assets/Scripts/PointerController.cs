using UnityEngine;
using UnityEngine.UI;

public class PointerController : MonoBehaviour
{
    private bool isActive;
    [SerializeField] private LayerMask layerMaskInteract;
    private GameObject raycastedObj;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private Image uiCrosshair;

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        var fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("Object"))
            {
                raycastedObj = hit.collider.gameObject;
                SetCrosshairActive();

                if (Input.GetKeyDown("e"))
                {
                    isActive = true;
                    Debug.Log("Interaction succesful");
                    Destroy(raycastedObj);
                }
            }
        }
        else if (isActive)
        {
            SetCrosshairNormal();
        }
    }

    private void SetCrosshairActive()
    {
        uiCrosshair.color = Color.red;
    }

    private void SetCrosshairNormal()
    {
        uiCrosshair.color = Color.white;
    }
}