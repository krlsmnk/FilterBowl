using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class cube : MonoBehaviour {

    public float xPos, yPos, zPos;
    public float moveSpeed = 10f;

    public void save()
    {
        SaveLoadData.savePostion(this);
    }

    public void load(){
        float[] loadedData = SaveLoadData.loadPosition();


        transform.position = new Vector3(loadedData[0],loadedData[1], loadedData[2]);
    }

	// Use this for initialization
	void Start () {
        load();
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(0, 1, 0);

        // if up arrow pressed move cube up
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        // If down arrow pressed move cube down
        if(Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        // If right arrow pressed move cube forward
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        // If left arrow pressed move cube backward
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        // Get x,y, and z position of transform
        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;

        print(xPos + "," + yPos + "," + zPos + ",");
		
	}

    void OnApplicationQuit() {
        save();
    }


}

    [Serializable]
    public class CubePosition
    {
        public float[] stats;

        public CubePosition(cube Cube)
        {
            stats = new float[3];

            stats[0] = Cube.xPos;
            stats[1] = Cube.yPos;
            stats[2] = Cube.zPos;




        }
}
