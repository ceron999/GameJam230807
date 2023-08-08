using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class JsonManager
{
    public static T ResourceDataLoad<T>(string name)
    {
        T gameData;

        string directory = "JsonData/";
        string appender1 = name;

        StringBuilder builder = new StringBuilder(directory);
        builder.Append(appender1);
        TextAsset jsonString = Resources.Load<TextAsset>(builder.ToString());
        gameData = JsonUtility.FromJson<T>(jsonString.ToString());

        return gameData;
    }

    public static void SaveJson<T>(T saveData, string name)
    {
        string jsonText;
        //�ȵ���̵忡���� ���� ��ġ�� �ٸ��� ���־�� �Ѵ�

        string savePath = Application.dataPath;
        string appender = "/UserData/";
        string nameString = name + ".json";
#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
        savePath = Application.persistentDataPath;
        
#endif
        StringBuilder builder = new StringBuilder(savePath);
        builder.Append(appender);
        if (!Directory.Exists(builder.ToString()))
        {
            //���丮�� ���°�� ������ش�
            Directory.CreateDirectory(builder.ToString());

        }
        builder.Append(nameString);

        jsonText = JsonUtility.ToJson(saveData, true);
        //�̷����� �ϴ� �����Ͱ� �ؽ�Ʈ�� ��ȯ�� �ȴ�
        //jsonUtility�� �̿��Ͽ� data�� json������ text�� �ٲپ��ش�

        //���Ͻ�Ʈ���� �̷��� �������ְ� �������ָ�ȴ� ��
        FileStream fileStream = new FileStream(builder.ToString(), FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public static SaveDataClass LoadSaveData()
    {
        //���� �츮�� ������ �����ߴ� �����͸� �������Ѵ�
        SaveDataClass gameData;
        string loadPath = Application.dataPath;
        string directory = "/UserData";
        string appender = "/UserData";

        string dotJson = ".json";
#if UNITY_EDITOR_WIN

#endif

#if UNITY_ANDROID
        loadPath = Application.persistentDataPath;


#endif
        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(directory);
        //�������� ���̺�� �Ȱ���
        //���Ͻ�Ʈ���� ������ش�. ���ϸ�带 open���� �ؼ� �����ش�. �� ���۸��̴�
        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            //���丮�� ���°�� ������ش�
            Directory.CreateDirectory(builderToString);

        }
        builder.Append(appender);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            //���̺� ������ �ִ°��

            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);

            //�ؽ�Ʈ�� string���� �ٲ۴����� FromJson�� �־��ָ��� �츮�� �� �� �ִ� ��ü�� �ٲ� �� �ִ�
            gameData = JsonUtility.FromJson<SaveDataClass>(jsonData);
        }
        else
        {
            //���̺������� ���°��
            gameData = null;
            //    = new SaveDataClass();
            //gameData.AddMedicineBySymptom(medicineDataWrapper, Symptom.water, Symptom.fire);
        }
        return gameData;
        //�� ������ ���ӸŴ�����, �ε����� �Ѱ��ִ� ���̴�
    }
}