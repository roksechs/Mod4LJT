using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;

namespace Mod4LJT.Localisation
{
    public static class LocalisationFile
    {
        static readonly string locatisationFilePath = "Resources/Mod4LJTLocalizastion.csv";
        static string[] lines;
        public static string[] languages;
        public static Dictionary<string, string[]> localisationDic = new Dictionary<string, string[]>();

        public static void ReadLocalisationFile()
        {
            localisationDic.Clear();
            lines = ModIO.ReadAllLines(locatisationFilePath);
            languages = lines[0].Split(',');
            for(int i = 1; i < lines.Length; i++)
            {
                string[] strs = lines[i].Split(',');
                localisationDic.Add(strs[0], strs);
            }
        }

        public static string GetTranslatedString(string key, int languageInt)
        {
            localisationDic.TryGetValue(key, out string[] strs);
            return strs[languageInt];
        }
    }
}
