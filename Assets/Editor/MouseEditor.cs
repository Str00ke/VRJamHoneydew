using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;

[ExecuteInEditMode, UnityEditor.InitializeOnLoad]
public class MouseEditor : MonoBehaviour, IPointerDownHandler
{
    //[SerializeField]
    //private List<GameObject> _holders = new List<GameObject>();

    private static MouseEditor instance;
    public static MouseEditor Instance { get; private set; }

    [SerializeField] private KeyCode placeObjectKey;
    [SerializeField] private KeyCode eraseObjectKey;

    [HideInInspector]
    public bool refresh = false;

    //public event System.Action<Vector2> onMouseClick;
    //public event System.Action<Vector2> onMouseUpdate;
    //public event System.Action onRefresh;

    private Transform main;

    private GameObject _selected;

    private Vector2 _prevMousePos;

    private LevelEditor editor;
    ///GUIContent iconContent = EditorGUIUtility.IconContent("sv_label_1");
    ///GUIContent iconContent2 = EditorGUIUtility.IconContent("sv_label_2");


    void Awake()
    {

        if (instance != null && instance != this)
            Destroy(gameObject);    

        instance = this;
    }
    void Start()
    {
        if (!Application.isEditor) return;

        //Debug.Log(Application.isEditor);
        
    }

    void OnDisable()
    {
        //SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }


    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("ClickEvent");
        GetComponent<LevelEditor>().OnClick(_prevMousePos);
        //instance.onMouseClick?.Invoke(_prevMousePos);
    }

    public void Click(/*int mouseButton*/KeyCode key)
    {
        /*
        if (mouseButton == 0)
            GetComponent<LevelEditor>().OnClick(_prevMousePos);
        else if (mouseButton == 1)
            GetComponent<LevelEditor>().TryErase();
        */
        if (key == placeObjectKey)
        {
            GetComponent<LevelEditor>().OnClick(_prevMousePos);
        }
        else if (key == eraseObjectKey)
            GetComponent<LevelEditor>().TryErase();
        //instance.onMouseClick?.Invoke(_prevMousePos);
    }

    void FetchHolders()
    {
        main = FindObjectOfType<EnemiesZone>().Holder.transform;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!Application.isEditor) return;

        if (editor != null && !editor.Enable)
        {
            //if (_selected != null) DestroyImmediate(_selected);
            return;
        }

        Vector3 mousePosScreen = Event.current.mousePosition;
        //mousePosScreen.y = sceneView.camera.pixelHeight - mousePosScreen.y;
        mousePosScreen.z = -10;
        Vector2 tmp = HandleUtility.GUIPointToScreenPixelCoordinate(mousePosScreen);
        Vector3 mousePosWorld = sceneView.camera.ScreenToWorldPoint(tmp);
        /*if (_prevMousePos != (Vector2)mousePosWorld)
        {
            CheckDist(mousePosWorld);
            _prevMousePos = mousePosWorld;
        }*/
        CheckDist(mousePosWorld);

        //Debug.Log("UpdateScene");
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        if (Event.current.GetTypeForControl(controlID) == EventType.KeyDown)
        {
            Click(/*Event.current.button*/Event.current.keyCode);
        }

        //if (Event.current.type == /*EventType.MouseDown*/EventType.KeyDown)
        //{
        //    Debug.Log(Event.current.keyCode);
        //    Click(/*Event.current.button*/Event.current.keyCode);
        //}
        //else if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
        //{
        //    Debug.Log("ClickRight");
        //}
        //if (Event.current.rawType == EventType.MouseUp) Click();

    }


    void Update()
    {
        //Debug.Log(Application.isPlaying);

        if (!Application.isEditor) return;

        if (editor != null && !editor.Enable)
        {
            //if (_selected != null) DestroyImmediate(_selected);
            return;
        } 
        else if (editor != null && editor.Enable)
        {
            if (_selected == null) refresh = true;
        }


        if (refresh)
        {


            editor = GetComponent<LevelEditor>();
            FetchHolders();
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;

            GetComponent<LevelEditor>().OnRefresh();

            //instance.onRefresh?.Invoke();
            refresh = false;
        }
        
    }

    void CheckDist(Vector2 mousePos)
    {

        //instance.onMouseUpdate?.Invoke(mousePos);
        GameObject _currSelect = _selected;
        float _minDist = _selected == null? 9999 : Vector2.Distance(mousePos, _currSelect.transform.position);
        for (int i = 0; i < main.childCount; i++)
        {
            float dist = Vector2.Distance(mousePos, main.GetChild(i).transform.position);
            if (dist < _minDist)
            {
                _minDist = dist;
                _currSelect = main.GetChild(i).gameObject;
            }
        }


        if(_selected != null) GetComponent<LevelEditor>().OnUpdate(/*mousePos*/_selected.transform.position);
        if (_currSelect == null) return;

        if (_selected != _currSelect)
        {

            var iconContent = EditorGUIUtility.IconContent("blendKey");
            var iconContent2 = EditorGUIUtility.IconContent("Collab");

            if (_selected == null)
            {
                EditorGUIUtility.SetIconForObject(_currSelect, (Texture2D)iconContent2.image);
                _selected = _currSelect;

            }
            else
            {
                EditorGUIUtility.SetIconForObject(_selected, (Texture2D)iconContent.image);
                EditorGUIUtility.SetIconForObject(_currSelect, (Texture2D)iconContent2.image);

                _selected = _currSelect;

            }



        }
    }
}
