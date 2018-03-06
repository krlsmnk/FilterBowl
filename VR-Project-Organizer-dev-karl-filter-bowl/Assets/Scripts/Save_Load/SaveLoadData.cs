using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class SaveLoadData  {

    public static void savePostion(cube Cube){
        // Create BinaryFormater
        BinaryFormatter bf = new BinaryFormatter();
        // Create file to save to
        FileStream stream = new FileStream(Application.persistentDataPath + "/position.cav", FileMode.Create);

        // Set data to be stored
        CubePosition data = new CubePosition(Cube);

        // Serialize data
        bf.Serialize(stream, data);

        stream.Close();
    }

    public static float[] loadPosition()
    {
        if (File.Exists(Application.persistentDataPath + "/position.cav"))
        {
            // Create BinaryFormater
            BinaryFormatter bf = new BinaryFormatter();
            // Open file to save to
            FileStream stream = new FileStream(Application.persistentDataPath + "/position.cav", FileMode.Open);

            CubePosition data = bf.Deserialize(stream) as CubePosition;

            stream.Close();
            return data.stats;



        }
        else
        {
            return null;
        }


    }



}
