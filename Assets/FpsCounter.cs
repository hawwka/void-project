using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    public string dataName;
    public int priority;

    private DeveloperMetrics developerMetrics;

    private float updateRate = .1f;
    private float lastUpdate;
    
    
    private void Start()
    {
        developerMetrics = FindFirstObjectByType<DeveloperMetrics>();

        if (developerMetrics != null)
        {
            developerMetrics.AddOrUpdateData(dataName, GetDataValue(), priority);
        }
    }

    private void Update()
    {
        if (Time.time - lastUpdate < updateRate) return;
        lastUpdate = Time.time;
        
        if (developerMetrics != null)
        {
            developerMetrics.AddOrUpdateData(dataName, GetDataValue(), priority);
        }
    }

    private string GetDataValue()
    {
        float fps = 1.0f / Time.deltaTime;
        return fps.ToString("0.0");
    }
}