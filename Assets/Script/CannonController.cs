using TMPro;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CannonController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform cannonBarrel;
    public Transform firePoint;
    public Rigidbody projectilePrefab;

    [Header("Parámetros")]
    public float forceImpulse = 10f;
    public float mass = 1f;

    [Header("Rotación")]
    public float rotationSpeed = 180f;
    private float targetAngleDeg = 45f;

    [Header("Trayectoria")]
    public int trajectoryPoints = 30;
    public float timeStep = 0.1f;
    private LineRenderer lineRenderer;

    [Header("Estado")]
    public Rigidbody lastProjectile;

    [Header("Debug UI")]
    public TextMeshProUGUI debugText; 


    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = trajectoryPoints;
    }

    void Update()
    {
        if (cannonBarrel == null) return;

        float currentAngle = cannonBarrel.localEulerAngles.z;
        if (currentAngle > 180f) currentAngle -= 360f;
        float newAngle = Mathf.MoveTowards(currentAngle, targetAngleDeg, rotationSpeed * Time.deltaTime);
        cannonBarrel.localEulerAngles = new Vector3(0f, 0f, newAngle);

        DrawTrajectory();
    }

    public void SetAngle(float angleDeg)
    {
        targetAngleDeg = Mathf.Clamp(angleDeg, 0f, 90f);
    }

    public void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Rigidbody proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Destroy(proj.gameObject, 65f);

        proj.mass = Mathf.Max(0.01f, mass);
        proj.linearVelocity = Vector3.zero;
        proj.angularVelocity = Vector3.zero;

        Vector3 dir = firePoint.up.normalized;
        proj.AddForce(dir * forceImpulse, ForceMode.Impulse);

        lastProjectile = proj;
        ShowDebugInfo(proj, dir);
    }

    void ShowDebugInfo(Rigidbody proj, Vector3 dir)
    {
        if (debugText == null) return;

        string info = $"📤 Disparo realizado\n" +
                      $"→ Posición: {firePoint.position}\n" +
                      $"→ Dirección: {dir}\n" +
                      $"→ Fuerza: {forceImpulse:0.##} N\n" +
                      $"→ Masa: {mass:0.##} kg\n" +
                      $"→ Velocidad inicial: {(dir * (forceImpulse / mass)).magnitude:0.##} m/s\n" +
                      $"→ Ángulo: {targetAngleDeg:0.#}°";

        debugText.text = info;
    }

    void DrawTrajectory()
    {
        if (lineRenderer == null || firePoint == null) return;

        Vector3 startPos = firePoint.position;
        Vector3 startVel = firePoint.up.normalized * (forceImpulse / mass);

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeStep;
    
            Vector3 pos = startPos + startVel * t + 0.5f * Physics.gravity * t * t;
            lineRenderer.SetPosition(i, pos);
        }
    }
}