using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class PoseDataManager
{
    readonly static string PATH = Path.Combine(Application.persistentDataPath, "my_session.txt");

    public static void SavePoseData(Pose pose)
    {
        string json = JsonUtility.ToJson(pose);

        Debug.Log(json);

        var writer = new StreamWriter(PATH, false);
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }

    public static Pose LoadPoseData()
    {
        string json = File.ReadAllText(PATH);

        Pose pose = JsonUtility.FromJson<Pose>(json);

        Debug.Log($"Pose => position:{pose.position} rotation:{pose.rotation}");

        return pose;
    }
}
