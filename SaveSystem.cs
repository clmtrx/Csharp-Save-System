using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace ClimatrixSave{
    public static class SaveSystem
    {
        //Enter Your Save Path here vv
        private static string savePath => "here!";
        private static List<string> saveContent { get {
            if (!File.Exists(savePath)) File.Create(savePath);
            return File.ReadAllLines(savePath).ToList<string>();
        } }

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
}
