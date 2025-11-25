using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using TMPro;

public class FirebaseRestSender : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI debugText;
    public Button sendButton;

    [Header("Firebase")]
    public string firebaseURL = "https://canonsimulator-default-rtdb.firebaseio.com/";

    void Start()
    {
        if (sendButton != null)
            sendButton.onClick.AddListener(SendDebugInfo);
    }

    void SendDebugInfo()
    {
        if (debugText == null || string.IsNullOrEmpty(firebaseURL)) return;

        string info = debugText.text;
        string timestamp = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

        var data = new
        {
            mensaje = info,
            fecha = timestamp
        };

        RestClient.Post(firebaseURL + "/disparos.json", data)
            .Then(response => Debug.Log("✅ Datos enviados a Firebase"))
            .Catch(error => Debug.LogError("❌ Error al enviar: " + error.Message));
    }
}