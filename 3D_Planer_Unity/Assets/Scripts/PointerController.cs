using UnityEngine;
using UnityEngine.UI;

public class PointerController : MonoBehaviour
{
    private BoxController grabbingBox;
    private bool isActive;

    [SerializeField] private float rayLength = 2f;
    public int rotationAngle = 15;
    public Text tooltip;
    public Image uiCrosshair;

    // Start is called before the first frame update
    private void Start()
    {
        // Setzt die Farbe des Pointers auf weiß.
        uiCrosshair.color = Color.white;
        // Der Mauszeiger wird beim Start gelocked, damit er nicht stört.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        // Sendet einen Ray aus um zu gucken, ob ein Objekt angewählt werden kann
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
        {
            if (hit.transform.gameObject.GetComponent<BoxController>() != null)
            {
                // setzt Pointer auf rot
                if (!isActive) SetCrosshairActive();

                // Falls eine Box ausgewählt ist wird diese vor dem Player angeheftet um diese zu bewegen
                if (grabbingBox != null)
                {
                    grabbingBox.transform.position =
                        transform.position + transform.forward * 2 + Vector3.up * 0.5f;
                    grabbingBox.transform.localEulerAngles = transform.localEulerAngles;

                    tooltip.text = "Re. Maus = Element ablegen";
                    // Wenn rechte Maustaste gedrückt wird wird Objekt abgelegt
                    if (Input.GetMouseButtonDown(1)) grabbingBox = null;
                }
                else
                {
                    // zeigt Tooltip an
                    tooltip.text = "Re. Maus = Element aufnehmen" +
                                   "\n Taste T = Element drehen" +
                                   "\n Taste L = Element löschen" +
                                   "\n Taste ESC = Cursor freigeben" +
                                   "\n Li. Maus = Cursor fangen";

                    // Mit Taste 'L' lassen sich Objekte löschen
                    if (Input.GetKeyDown("l"))
                    {
                        grabbingBox = hit.transform.gameObject.GetComponent<BoxController>();
                        Destroy(grabbingBox.gameObject);
                    }

                    // Mit Taste 'T' lassen sich Objekte drehen
                    if (Input.GetKeyDown("t"))
                    {
                        grabbingBox = hit.transform.gameObject.GetComponent<BoxController>();
                        grabbingBox.transform.Rotate(Vector3.up, rotationAngle);
                        grabbingBox = null;
                    }

                    // Mit rechter Maustaste lassen sich Objekte bewegen
                    if (Input.GetMouseButtonDown(1))
                        grabbingBox = hit.transform.gameObject.GetComponent<BoxController>();
                }
            }
        }
        else if (isActive)
        {
            SetCrosshairNormal();
        }

        //Beim Drücken von 'esc' wird der Mauszeiger freigegeben (kann im Debugmodus benutzt werden um diesen zu beenden)
        if (Input.GetKeyDown("escape")) Cursor.lockState = CursorLockMode.None;
        // Beim Drücken der linken Maustaste wird der Cursor gelocked.
        if (Input.GetMouseButtonDown(0)) Cursor.lockState = CursorLockMode.Locked;
    }

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
        tooltip.text = "";
    }
}