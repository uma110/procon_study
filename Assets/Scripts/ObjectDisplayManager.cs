using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectDisplayManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefav on a plane at the touch location")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
    ARPlaneManager m_PlaneManager;

    [SerializeField] EventTrigger touchSheet;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();

        //タッチシートにタッチされた時のイベントを登録
        //Input系は画面全体にタッチ判定がついてしまう
        //その場合、他のUIを触った時も判定してしまうので、この実装をする
        var entryEvent = new EventTrigger.Entry();
        entryEvent.eventID = EventTriggerType.PointerDown;
        entryEvent.callback.AddListener((eventData) =>
        {
            var touchPosition = ((PointerEventData)eventData).position;
            Debug.Log(touchPosition);
            OnDisplayTouched(touchPosition);
        });
        touchSheet.triggers.Add(entryEvent);
    }

    private void OnDisplayTouched(Vector2 touchPosition)
    {
        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;

            if (spawnedObject == null)
            {
                //一度のオブジェクトを生成してない場合、生成
                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                //ver0
                //オブジェクト生成済みなら、位置のみ調整
                spawnedObject.transform.position = hitPose.position;


                //取得したヒットオブジェクトのidを取得
                TrackableId planeId = s_Hits[0].trackableId;
                if (planeId != TrackableId.invalidId)
                {
                    //ver1
                    //平面に垂直にオブジェクトを回転
                    var plane = m_PlaneManager.GetPlane(planeId);
                    var direction = plane.normal;
                    spawnedObject.transform.up = direction;

                    //ver2
                    //オブジェクトがカメラを向くように調整 y軸は変更しないように注意
                    var cameraPos = Camera.main.transform.position;
                    var lookDirection = new Vector3(cameraPos.x, spawnedObject.transform.position.y, cameraPos.z);
                    spawnedObject.transform.LookAt(lookDirection);
                }
            }
        }
    }

    public void SaveObjectPose()
    {
        if (spawnedObject == null) return;

        //オブジェクトのPose（位置と向き）をローカルファイルに保存
        PoseDataManager.SavePoseData(new Pose(spawnedObject.transform.position, spawnedObject.transform.rotation));
    }

    public void RestoreObjectPose()
    {
        Pose pose = PoseDataManager.LoadPoseData();
        if (spawnedObject != null)
        {
            spawnedObject.transform.position = pose.position;
            spawnedObject.transform.rotation = pose.rotation;
        }
    }
}
