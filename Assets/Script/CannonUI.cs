using UnityEngine;
using UnityEngine.UI;

public class CannonUI : MonoBehaviour
{
    [Header("UI")]
    public Slider angleSlider;
    public Slider forceSlider;
    public Slider massSlider;
    public Text angleLabel;
    public Text statusText;
    public Button shootButton;

    [Header("Lógica")]
    public CannonController cannon;

    void Start()
    {
          if (cannon != null)
        {
            forceSlider.value = cannon.forceImpulse;
            massSlider.value = cannon.mass;
        }
             
        angleSlider.onValueChanged.AddListener(OnAngleChanged);
        forceSlider.onValueChanged.AddListener(OnForceChanged);
        massSlider.onValueChanged.AddListener(OnMassChanged);
        shootButton.onClick.AddListener(OnShootClicked);

        UpdateStatus("Listo");
        UpdateAngleLabel(angleSlider.value);
    }

    void OnAngleChanged(float value)
    {
        if (cannon == null) return;
        cannon.SetAngle(value);
        UpdateAngleLabel(value);
    }

    void OnForceChanged(float value)
    {
        if (cannon == null) return;
        cannon.forceImpulse = value;
        UpdateStatus($"Fuerza: {value:0.##}");
    }

    void OnMassChanged(float value)
    {
        if (cannon == null) return;
        cannon.mass = Mathf.Max(0.01f, value);
        UpdateStatus($"Masa: {value:0.##} kg");
    }

    void OnShootClicked()
    {
        if (cannon == null) return;
        cannon.Shoot();
        UpdateStatus("Disparo realizado");
    }

    void UpdateStatus(string msg)
    {
        if (statusText != null) statusText.text = msg;
    }

    void UpdateAngleLabel(float angle)
    {
        if (angleLabel != null)
            angleLabel.text = $"Ángulo: {angle:0.#}°";
    }
}