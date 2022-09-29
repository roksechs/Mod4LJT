using System.Collections.Generic;

namespace Mod4LJT.Localisation
{
    public static class LocalisationFile
    {
        static readonly string locatisationFilePath = "Resources/Mod4LJTLocalizastion.csv";
        static string[] lines;
        public static string[] languages;
        public static int languageInt;
        public static Dictionary<string, string[]> localisationDic = new Dictionary<string, string[]>();

        public static void ReadLocalisationFile()
        {
            localisationDic.Clear();
            lines = Modding.ModIO.ReadAllLines(locatisationFilePath);
            languages = lines[0].Split(',');
            for (int i = 1; i < lines.Length; i++)
            {
                string[] strs = lines[i].Split(',');
                localisationDic.Add(strs[0], strs);
            }
        }

        public static string GetTranslatedString(string key)
        {
            if (localisationDic.TryGetValue(key, out string[] strs))
                return strs[languageInt];
            Mod.Warning("Cannot find in the localisation file: " + key);
            return key;
        }
    }
}
