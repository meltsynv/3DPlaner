using System;
using UnityEngine;
using UnityEngine.UI;

public class PointerController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // selectedBox = null;
        // // Setzt die Farbe des Pointers auf weiß.
        // uiCrosshair.color = Color.white;
        DisableMovement();
    }

    // Update is called once per frame
    private void Update()
    {
        // Falls eine Box aufgehoben wurde wird die Gravitation für diese Box abgestellt.
        // Sie fällt also zwischen den einzelnen Update-Zyklen nicht mehr runter
        if (isPickecUp && selectedBox != null)
        {
            selectedBox.transform.position =
                transform.position + transform.forward * 2 + Vector3.up * 0.5f;
            selectedBox.transform.localEulerAngles = transform.localEulerAngles;
            selectedBox.gameObject.GetComponent<Rigidbody>().useGravity = false;

            tooltip.text = "Re. Maus = Element ablegen";
            // Wenn rechte Maustaste gedrückt wird wird Objekt abgelegt
            if (Input.GetMouseButtonDown(1))
            {
                selectedBox.gameObject.GetComponent<Rigidbody>().useGravity = true;
                SetSelectedBox(null);
                isPickecUp = false;
            }
        }
        else
        {
            RaycastHit hit;
            // Sendet einen Ray aus um zu gucken, ob ein Objekt angewählt werden kann
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
            {
                if (hit.transform.gameObject.GetComponent<FurnitureController>() != null)
                {
                    // setzt Pointer auf rot
                    if (!isActive) SetCrosshairActive();

                    // Falls keine Box rumgetragen wird
                    if (selectedBox == null)
                    {
                        // zeigt Tooltip an
                        tooltip.text = "Re. Maus = Element aufnehmen \n" +
                                       "Taste T = Element drehen \n" +
                                       "Taste L = Element löschen \n" +
                                       "Taste ESC = Cursor freigeben \n" +
                                       "Li. Maus = Cursor fangen";

                        // Mit Taste 'L' lassen sich Objekte löschen
                        if (Input.GetKeyDown("l")) Destroy(GetSelectedBox(hit).gameObject);

                        // Mit Taste 'T' lassen sich Objekte drehen
                        if (Input.GetKeyDown("t"))
                        {
                            GetSelectedBox(hit).transform.Rotate(Vector3.up, rotationAngle);
                            SetSelectedBox(null);
                        }

                        // Mit rechter Maustaste lassen sich Objekte bewegen
                        if (Input.GetMouseButtonDown(1))
                        {
                            SetSelectedBox(GetSelectedBox(hit));
                            isPickecUp = true;
                        }
                    }
                }
            }
            else if (isActive) // setzt den Pointer zurück auf normal, wenn kein Objekt in der Nähe ist
            {
                SetCrosshairNormal();
            }

            //Beim Drücken von 'esc' wird der Mauszeiger freigegeben (kann im Debugmodus benutzt werden um diesen zu beenden)
            if (Input.GetKeyDown("escape")) Cursor.lockState = CursorLockMode.None;

            // Mit Taste 'C' soll sich das Charakterfenster/Möbelfenster öffnen.
            // Die Bewegung des Spielers soll währenddessen ausgeschaltet sein
            if (Input.GetKeyDown("c")) DisableMovement();
        }
    }

    #region Variables

    private FurnitureController selectedBox;
    private bool isActive;
    private bool isPickecUp;
    public PlayerController player;

    [SerializeField] private float rayLength = 5f;
    public int rotationAngle = 15;
    public Text tooltip;
    public Image uiCrosshair;
    public GameObject uiOverlay;

    #endregion

    #region helperMethods

    // Ändert die Farbe des Pointers auf rot. Dadurch wird gezeigt, dass ein Element benutzt werden kann.
    private void SetCrosshairActive()
    {
        isActive = true;
        uiCrosshair.color = Color.red;
    }

    // Ändert die Farbe des Pointers auf weiß. Dies ist der Default-Wert.
    private void SetCrosshairNormal()
    {
        isActive = false;
        uiCrosshair.color = Color.white;
        tooltip.text = "Nähere dich einem Objekt um mit ihm zu interagieren.";
    }

    // gibt die Box zurück, welche vom Hit erfasst wurde
    private FurnitureController GetSelectedBox(RaycastHit hit)
    {
        return hit.transform.gameObject.GetComponent<FurnitureController>();
    }

    // Setter für die selectedBox
    private void SetSelectedBox(FurnitureController box)
    {
        selectedBox = box;
    }

    public void EnableMovement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.canMove = true;
        uiOverlay.SetActive(false);
    }

    public void DisableMovement()
    {
        Cursor.lockState = CursorLockMode.Confined;
        player.canMove = false;
        uiOverlay.SetActive(true);
    }

    public void SetMatieral(Material material)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
        {
            // ändert das Material der Kindobjekte
            selectedBox = hit.collider.GetComponent<FurnitureController>();
            var renderers = selectedBox.GetComponentsInChildren<Renderer>();
            try
            {
                foreach (var renderer in renderers) renderer.material = material;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            selectedBox = null;
        }
    }

    #endregion
}