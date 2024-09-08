using UnityEngine;
using System.Collections.Generic;

public class DeveloperMetrics : MonoBehaviour
{
    public static DeveloperMetrics Instance;

    public bool Show;
    
    private class DisplayData
    {
        public string DataName;
        public string DataValue;
        public int Priority;
    }

    private List<DisplayData> dataToDisplay = new List<DisplayData>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddOrUpdateData(string dataName, string dataValue, int priority)
    {
        // Ищем, существует ли запись с таким же именем данных
        var existingData = dataToDisplay.Find(data => data.DataName == dataName);
        
        if (existingData != null)
        {
            // Если данные уже есть, обновляем значение и приоритет
            existingData.DataValue = dataValue;
            existingData.Priority = priority;
        }
        else
        {
            // Если данных еще нет, добавляем новую запись
            var newData = new DisplayData
            {
                DataName = dataName,
                DataValue = dataValue,
                Priority = priority
            };
            dataToDisplay.Add(newData);
        }
        
        dataToDisplay.Sort((x, y) => x.Priority.CompareTo(y.Priority));
    }
    
    private void OnGUI()
    {
        if (!Show) return;
        
        var style = new GUIStyle
        {
            fontSize = 24,
            normal = { textColor = Color.green }
        };

        for (int i = 0; i < dataToDisplay.Count; i++)
        {
            var rect = new Rect(10, 10 + i * 30, 400, 20);
            string text = $"{dataToDisplay[i].DataName}: {dataToDisplay[i].DataValue}";
            GUI.Label(rect, text, style);
        }
    }
}