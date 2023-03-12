using TMPro;
using UnityEngine;

/*
 * TODO: this is temporary, for testing/debugging.
 */
public class PlayerStatusDisplay : MonoBehaviour
{
    public static PlayerStatusDisplay Instance;

    public TMP_Text healthText;
    public TMP_Text energyText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one PlayerStatusDisplay");
            Destroy(this);
        }
        Instance = this;
    }

    public void updateHP(int currentHP, int maximumHP)
    {
        Debug.Log("Updating hp");
        // healthText.text = $"{currentHP}/{maximumHP}";
    }

    public void updateEnergy(int currentEnergy, int maximumEnergy)
    {
        // energyText.text = $"{currentEnergy}/{maximumEnergy}";
    }
}
