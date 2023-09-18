// 日本語対応
using System;
using System.Collections.Generic;

public static class CSVReader
{
    /// <summary>  </summary>
    /// <param name="csvText"> 読み込まれたCSV形式のテキスト。 </param>
    /// <param name="ignoreRows"> 無視する行の数。（不要なヘッダー行を読み込まない為。） </param>
    /// <returns></returns>
    public static List<string[]> ParseCsv(string csvText, int ignoreRows = 0)
    {
        List<string[]> csvData = new List<string[]>();

        string[] lines = csvText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = ignoreRows; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(',');
            csvData.Add(fields);
        }

        return csvData;
    }
}