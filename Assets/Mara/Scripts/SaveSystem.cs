using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
  public static bool IsGameSaved()
  {
    return File.Exists(Application.persistentDataPath + "/playerData.roots");
  }

  public static void SaveGameLevel(int level)
  {
    BinaryFormatter formatter = new BinaryFormatter();

    string path = Application.persistentDataPath + "/playerData.roots";
    FileStream ifile = new FileStream(path, FileMode.Create);

    formatter.Serialize(ifile, level);
    ifile.Close();
  }

  public static int GetGameLevel()
  {
    string path = Application.persistentDataPath + "/playerData.roots";
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream ofile = new FileStream(path, FileMode.Open);

      int level = (int)formatter.Deserialize(ofile);
      ofile.Close();

      return level;
    }
    else
    {
      Debug.Log("Not a saved file");
      return -1;
    }
  }
}
