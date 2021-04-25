using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIKKEN : MonoBehaviour
{
    [SerializeField] GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        Pose pose = new Pose(obj.transform.position, obj.transform.rotation);
        PoseDataManager.SavePoseData(pose);

        Invoke("Read", 0.5f);
    }

    void Read() {
        Debug.Log("READ");
        PoseDataManager.LoadPoseData();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
