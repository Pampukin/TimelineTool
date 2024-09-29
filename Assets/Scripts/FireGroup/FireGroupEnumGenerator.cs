using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Timeline;

public static class FireGroupEnumGenerator
{
    //Defineを作成する場所
    const string  DEFINE_FILE_PATH = "Assets/Scripts/FireGroup/FireGroupEnum.cs";
    public static void GenerateFireGroupEnum()
    {
        var fireGroupTimelineHandle = Addressables.LoadAssetAsync<TimelineAsset>("FireGroupTimeline");

        var fireGroupTimeline = fireGroupTimelineHandle.WaitForCompletion();

        Generate(fireGroupTimeline.GetRootTracks());

        Addressables.Release(fireGroupTimelineHandle);
    }

    private static void Generate(IEnumerable<TrackAsset> trackAssets)
    {
        // StringBuilderを使用してEnumクラスを生成
        var sb = new StringBuilder();
        sb.AppendLine("public enum FireGroupEnum");
        sb.AppendLine("{");

        foreach (var track in trackAssets)
        {
            if (track is GroupTrack groupTrack)
            {
                sb.AppendLine($"    {groupTrack.name},");
            }
        }
        sb.AppendLine("}");

        Debug.Log(sb.ToString());
        //作成しようとするファイルが既に存在しているかのチェック．
        string directoryName = Path.GetDirectoryName(DEFINE_FILE_PATH);
        if(Directory.Exists(directoryName) == false)
        {
            if (directoryName != null)
            {
                Directory.CreateDirectory(directoryName);
            }
        }
        File.WriteAllText(DEFINE_FILE_PATH, sb.ToString(), Encoding.UTF8);

        AssetDatabase.ImportAsset(DEFINE_FILE_PATH);
    }
}
