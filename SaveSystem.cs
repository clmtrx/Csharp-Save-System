using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


public static class SaveSystem
{
    private static string savePath => Application.persistentDataPath + "beat_crash.dat";
    private static List<string> saveContent { get { return File.ReadAllLines(savePath).ToList<string>(); } }

    public static bool downScroll => ReadData<int>("downScroll", 1) > 0;

    public static void SaveData<T>(string key, T data){
        if (!saveContent.Contains(key + ":")){
            var newSave = saveContent;
            newSave.Add(key+":"+data.ToString());

            File.WriteAllLines(savePath, newSave);
        }
        else
            for(int i = 0; i < saveContent.Count; i++)
                if (saveContent[i].StartsWith(key + ":")){
                    var newSave = saveContent;
                    newSave[i] = key + ":" + data.ToString();
                    File.WriteAllLines(savePath, newSave);
                }    
    }
    
    public static T ReadData<T>(string key, T def){
        for(int i = 0; i < saveContent.Count; i++){
            if (!saveContent.Contains(key + ":")){
                var newSave = saveContent;
                newSave.Add(key+":"+def.ToString());

                File.WriteAllLines(savePath, newSave);
                return def;
            }

            if (saveContent[i].StartsWith(key + ":")) return (T)GetKeyContent(saveContent, key, i);
        }

        return def;
    }

    private static object GetKeyContent(List <string> sav, string key, int line){
        return sav[line].Split(key + ":")[1].Split("\n")[0];
    }
}

