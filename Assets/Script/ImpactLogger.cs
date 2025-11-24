using UnityEngine;

public class ImpactLogger : MonoBehaviour
{
    private float spawnTime;

    void Awake()
    {
        spawnTime = Time.time;
    }

    void OnCollisionEnter(Collision c)
    {
        float flightTime = Time.time - spawnTime;
        float relativeSpeed = c.relativeVelocity.magnitude;
        float impulseMag = c.impulse.magnitude;
        Vector3 point = c.contacts[0].point;

        Debug.Log($"Impacto con: {c.gameObject.name}");
        Debug.Log($"Tiempo de vuelo: {flightTime:0.###} s");
        Debug.Log($"Punto de impacto: {point}");
        Debug.Log($"Velocidad relativa: {relativeSpeed:0.###} m/s");
        Debug.Log($"Impulso: {impulseMag:0.###} N·s");
    }
}