using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int _order = 10;
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    Debug.LogError("UIManager Instance Init Failed");
                }
            }

            return instance;
        }
    }

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("UI_Root");
            if (root == null)
                root = new GameObject { name = "UI_Root" };

            return root;
        }
    }
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        CanvasScaler canvasScaler = Utils.GetOrAddComponent<CanvasScaler>(go);
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1280, 1080);

        GraphicRaycaster graphicRaycaster = Utils.GetOrAddComponent<GraphicRaycaster>(go);


        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
            
    }

    // Scene, Popup이 아닌 UI를 만들기
    public T MakeSubUI<T>(string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Instantiate(Resources.Load<GameObject>($"Prefabs/UI/Sub/{name}"));

        return go.GetOrAddComponent<T>();
    }
    // 없어지지 않고 계속 배경으로 남는 UI 만들기

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject prefab = Instantiate(Resources.Load<GameObject>($"Prefabs/UI/Scene/{name}"));
        T sceneUI = Utils.GetOrAddComponent<T>(prefab);
        _sceneUI = sceneUI;

        prefab.transform.SetParent(Root.transform);

        return sceneUI;
    }
    // Popup UI 만들기
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject prefab = Instantiate(Resources.Load<GameObject>($"Prefabs/UI/Popup/{name}"));
        T popup = Utils.GetOrAddComponent<T>(prefab);
        _popupStack.Push(popup);

        prefab.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.LogError("Close Failed.");
            return;
        }

        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Destroy(popup.gameObject);
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }
}
