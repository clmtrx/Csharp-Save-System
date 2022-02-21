using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace ClimatrixSave{
    public static class SaveSystem
    {
        //Enter Your Save Path here vv
        private static string savePath => "here!";
        public static List<string> saveContent { get => GetLines(); }

    public static void SaveData<T>(string key, T data){

        if (saveContent.FirstOrDefault(str => str.StartsWith(key + ":")) == null){
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
        if (saveContent.FirstOrDefault(str => str.StartsWith(key + ":")) == null){
            var newSave = saveContent;
            newSave.Add(key+":"+def.ToString());

            File.WriteAllLines(savePath, newSave);
            return def;
        }

        for(int i = 0; i < saveContent.Count; i++)
            if (saveContent[i].StartsWith(key + ":")) return GetKeyContent<T>(saveContent, key, i);

        return def;
    }

    private static T GetKeyContent<T>(List <string> sav, string key, int line){
        return (T)Convert.ChangeType(sav[line].Split(key + ":")[1].Split("\n")[0], typeof(T));
    }

    private static List<string> GetLines(){
        if (!File.Exists(savePath)) File.Create(savePath).Dispose();
        return File.ReadAllLines(savePath).ToList<string>();
    }
}
