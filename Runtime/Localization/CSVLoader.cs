﻿using System.Collections.Generic;
using UnityEngine;

namespace Unisloth.Localization.CSV
{
    public static class CSVLoader
    {

        private static readonly char separator = ';';

        /// <summary>
        /// Loads a localization CSV into a dictionary.
        /// First dimension is locales, second is translation keys
        /// </summary>
        /// <param name="fileName">Name of the localization file, must be in Resources folder</param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string>> LoadDicoFromCSV(string fileName)
        {
            Dictionary<string, Dictionary<string, string>> res = new Dictionary<string, Dictionary<string, string>>();
            string[] rawLines = GetRawCSVLines(fileName);
            string[][] splitLines = CleanedLines(RawToSplitLines(rawLines));
            if (splitLines != null && splitLines.Length > 0)
            {
                string[] locales = splitLines[0];
                for (int i = 1; i < locales.Length; i++)
                {
                    if (locales[i] != "")
                    {
                        if (!res.ContainsKey(locales[i]))
                        {
                            Dictionary<string, string> newLocaleDic = new Dictionary<string, string>();
                            for (int j = 1; j < splitLines.Length; j++)
                            {
                                newLocaleDic.Add(splitLines[j][0], splitLines[j][i]);
                            }
                            res.Add(locales[i], newLocaleDic);
                        }
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Loads a localization CSV into a dictionary.
        /// First dimension is translations keys, second is locales
        /// </summary>
        /// <param name="fileName">Name of the localization file, must be in Resources folder</param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string>> LoadDicoFromCSVKeyFirst(string fileName)
        {
            Dictionary<string, Dictionary<string, string>> res = new Dictionary<string, Dictionary<string, string>>();
            string[] rawLines = GetRawCSVLines(fileName);
            string[][] splitLines = CleanedLines(RawToSplitLines(rawLines));
            if (splitLines != null && splitLines.Length > 0)
            {
                string[] locales = splitLines[0];
                int keyCount = splitLines.Length;

                for (int i = 1; i < keyCount; i++)
                {
                    string[] currentLine = splitLines[i];
                    Dictionary<string, string> keyDico = new Dictionary<string, string>();
                    for (int j = 1; j < locales.Length; j++)
                    {
                        keyDico.Add(locales[j], currentLine[j]);
                    }
                    res.Add(currentLine[0], keyDico);
                }
            }
            return res;
        }

        public static string[,] LoadRawCSVIntoMatrix(string fileName)
        {
            string[] rawLines = GetRawCSVLines(fileName);
            return RawToSplitLines2D(rawLines);
        }

        private static string[] GetRawCSVLines(string fileName)
        {
            TextAsset resObj = Resources.Load<TextAsset>(fileName);
            if (resObj != null)
            {
                return RawToLines(resObj.text);
            }
            Debug.LogError("Can't find localization file");
            return new string[] { };
        }

        private static string[] RawToLines(string rawText)
        {
            string[] separators = { "\n" };
            return rawText.Split(separators, System.StringSplitOptions.None);
        }

        private static string[][] RawToSplitLines(string[] rawLines)
        {
            string[][] splitLines = new string[rawLines.Length][];

            for (int i = 0; i < rawLines.Length; i++)
            {
                splitLines[i] = rawLines[i].Split(separator);
            }
            return splitLines;
        }

        private static string[,] RawToSplitLines2D(string[] rawLines)
        {
            int lineSize = rawLines[0].Split(separator).Length;
            string[,] splitLines = new string[lineSize, rawLines.Length];
            for (int i = 0; i < rawLines.Length; i++)
            {
                string[] splitLine = rawLines[i].Split(separator);
                for (int j = 0; j < splitLine.Length; j++)
                {
                    splitLines[j, i] = splitLine[j];
                }
            }
            return splitLines;
        }

        private static string[][] CleanedLines(string[][] splitLines)
        {
            List<string[]> listLines = new List<string[]>();
            foreach (string[] splitLine in splitLines)
            {
                if (splitLine[0] != "")
                {
                    listLines.Add(splitLine);
                }
            }
            return listLines.ToArray();
        }
    }
}
