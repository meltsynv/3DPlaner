using UnityEngine;
using UnityEngine.UI;

public class PointerController : MonoBehaviour
{
    [HideInInspector] private bool _isActive;

    private GameObject _raycastedObj;
    public Text _tooltip;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private Image uiCrosshair;

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        var fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Object"))
            {
                _raycastedObj = hit.collider.gameObject;
                SetCrosshairActive();

                _tooltip.text = "Mit der Taste 'E' können Sie das Element löschen.";

                if (Input.GetKeyDown("e"))
                {
                    Debug.Log("Interaction succesful");
                    Destroy(_raycastedObj);
                }
            }
        }
        else if (_isActive)
        {
            SetCrosshairNormal();
        }
    }

    private void SetCrosshairActive()
    {
        _isActive = true;
        uiCrosshair.color = Color.red;
    }

    private void SetCrosshairNormal()
    {
        _isActive = false;
        uiCrosshair.color = Color.white;
        _tooltip.text = "";
    }
}