using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : Singleton<DataManager>, IManager
{
    public readonly string ADDRESS = "https://docs.google.com/spreadsheets/d/1XnYIk7xsyaXOV8ZlURqPKc2CVfqWNUtT42XZ7hGddUg";
    public readonly string RANGE = "A2:D";
    public readonly long SHEET_ID = 0;

    [SerializeField] private List<StageData> stageData;

    public void Init()
    {
        // Computer
        StartCoroutine("LoadData");
    }

    private IEnumerator LoadData()
    {
        UnityWebRequest www = UnityWebRequest.Get(ReadSpreadSheet(ADDRESS, RANGE, SHEET_ID));
        yield return www.SendWebRequest();

        stageData = ParsingSheet.GetDatas<StageData>(www.downloadHandler.text);
    }

    public string ReadSpreadSheet(string address, string range, long sheetID)
    {
        return $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }

    public StageData GetStageData(int id)
    {
        return stageData[id];
    }

    public int GetStageCount()
    {
        return stageData.Count;
    }
}
