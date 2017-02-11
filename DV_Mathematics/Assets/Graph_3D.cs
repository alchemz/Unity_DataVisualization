using UnityEngine;

public class Graph_3D : MonoBehaviour
{
    ParticleSystem m_system;
     void Start()
    {
        CreatePoints();
        m_system = GetComponent<ParticleSystem>();
    }
    public enum FunctionOption
    {
        Linear,
        Exponential,
        Parabola,
        Sine,
        Ripple
    }

    private delegate float FunctionDelegate(Vector3 p, float t);
    private static FunctionDelegate[] functionDelegates = {
        Linear,
        Exponential,
        Parabola,
        Sine,
        Ripple
    };

    public FunctionOption function;

    [Range(10, 100)]
    public int resolution = 10;

    private int currentResolution;
    private ParticleSystem.Particle[] points;

    private void CreatePoints()
    {
        currentResolution = resolution;
        points = new ParticleSystem.Particle[resolution * resolution];
        float increment = 1f / (resolution - 1);
        int i = 0;
        for (int x = 0; x < resolution; x++)
        {
            for (int z = 0; z < resolution; z++)
            {
                Vector3 p = new Vector3(x * increment, 0f, z * increment);
                points[i].position = p;
                points[i].color = new Color(p.x, 0f, p.z);
                points[i++].size = 0.1f;
            }
        }
    }

    void Update()
    {
        if (currentResolution != resolution || points == null)
        {
            CreatePoints();
        }
        FunctionDelegate f = functionDelegates[(int)function];
        float t = Time.timeSinceLevelLoad;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 p = points[i].position;
            p.y = f(p, t);
            points[i].position = p;
            Color c = points[i].color;
            c.g = p.y;
            points[i].color = c;
        }
        m_system.SetParticles(points, points.Length);
    }

    private static float Linear(Vector3 p, float t)
    {
        return p.x;
    }

    private static float Exponential(Vector3 p, float t)
    {
        return p.x * p.x;
    }

    private static float Parabola(Vector3 p, float t)
    {
        p.x = 2f * p.x - 1f;
        p.z = 2f * p.z - 1f;
        return 1f - p.x * p.x * p.z * p.z;
    }

    private static float Sine(Vector3 p, float t)
    {
        return 0.50f +
            0.25f * Mathf.Sin(4 * Mathf.PI * p.x + 4 * t) * Mathf.Sin(2 * Mathf.PI * p.z + t) +
            0.10f * Mathf.Cos(3 * Mathf.PI * p.x + 5 * t) * Mathf.Cos(5 * Mathf.PI * p.z + 3 * t) +
            0.15f * Mathf.Sin(Mathf.PI * p.x + 0.6f * t);
    }

    private static float Ripple(Vector3 p, float t)
    {
        p.x -= 0.5f;
        p.z -= 0.5f;
        float squareRadius = p.x * p.x + p.z * p.z;
        return 0.5f + Mathf.Sin(15f * Mathf.PI * squareRadius - 2f * t) / (2f + 100f * squareRadius);
    }
}