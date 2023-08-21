using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;

public class JsonParser
{
    public static string ObjectToJson(object obj)
    {
        return JsonMapper.ToJson(obj);
    }

    public static T JsonToObject<T>(string jsonData)
    {
        return JsonMapper.ToObject<T>(jsonData);
    }

    public static T LoadGameData<T>(string fileName)
    {
        TextAsset jsonData = Resources.Load(fileName) as TextAsset;

        return JsonUtility.FromJson<T>(jsonData.ToString());
    }

    public static void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public static T LoadJsonFileAES<T>(string loadPath, string fileName, string key)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        jsonData = AESParser.Decrypt256(jsonData, key);

        return JsonUtility.FromJson<T>(jsonData);
    }
}
