using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static int score = 0;
    public TMP_Text manaText;
    public TMP_Text healthText;
    public TMP_Text scoreText;
    public GameObject playerObject;
    private Wizard wizardObject;

    void Start()
    {
        // Wizard-Referenz holen, falls nicht im Inspector gesetzt
        if (playerObject == null)
            playerObject = GameObject.FindWithTag("Player");
        if (playerObject == null)
            playerObject = GameObject.Find("Wizard");

        if (playerObject != null)
            wizardObject = playerObject.GetComponent<Wizard>();

        // TMP_Text-Referenzen prüfen (falls nicht im Inspector gesetzt)
        if (manaText == null || healthText == null || scoreText == null)
        {
            // Annahme: Die Text-Objekte sind die ersten drei Kinder dieses UI-Objekts
            manaText = transform.GetChild(0).GetComponent<TMP_Text>();
            healthText = transform.GetChild(1).GetComponent<TMP_Text>();
            scoreText = transform.GetChild(2).GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        if (wizardObject != null)
        {
            manaText.text = $"Mana: {(int)wizardObject.mana}";
            healthText.text = $"Health: {(int)wizardObject.health}";
        }
        scoreText.text = $"Score: {score}";
    }
}
