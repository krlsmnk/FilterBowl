using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CAVS.ProjectOrganizer.Project
{

    public static class ProjectFactory
    {

        private static char lineSeperater = '\n'; // It defines line seperate character
        private static char fieldSeperator = ','; // It defines field seperate chracter

        public static Item[] buildItemsFromCSV(string csvPath)
        {
            string fileData = System.IO.File.ReadAllText(csvPath);
            string[] records = fileData.Split(lineSeperater);
            string[] titles = records[0].Split(fieldSeperator);

			Item[] items = new Item[records.Length-1];

            for (int i = 1; i < records.Length; i++)
            {
                Dictionary<string, string> valuesOfItem = new Dictionary<string, string>();
                string[] fields = records[i].Split(fieldSeperator);
                for (int fieldIndex = 0; fieldIndex < fields.Length; fieldIndex++)
                {
					valuesOfItem.Add(titles[fieldIndex], fields[fieldIndex]);
                }
				items[i-1] = new TextItem(fields[5], "", valuesOfItem);
            }


            // foreach (string record in records)
            // {
            //     string[] fields = record.Split(fieldSeperator);
            //     foreach (string field in fields)
            //     {
            //         //contentArea.text += field + "\t";
            //     }
            //     //contentArea.text += '\n';
            // }

            return items;

            // string fileData = System.IO.File.ReadAllText (csvPath);
            // string[] lines = fileData.Split ("\n"[0]);
            // foreach (string line in lines) {
            // 	Debug.Log (line);
            // }
            // return null;
        }

    }

}