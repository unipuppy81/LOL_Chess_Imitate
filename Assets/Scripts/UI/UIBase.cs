using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    #region Fields

    private readonly Dictionary<Type, Dictionary<string, UnityEngine.Object>> _objects = new();

    #endregion

    #region Init

    private void Start()
    {
        Init();
    }

    protected virtual void Init() { }

    #endregion

    #region Properties

    protected void SetUI<T>() where T : UnityEngine.Object => Binding<T>(gameObject);
    protected T GetUI<T>(string componentName) where T : UnityEngine.Object => GetComponent<T>(componentName);

    #endregion

    #region Binding

    /// <summary>
    /// UnityEngine.Object Ÿ���� ������Ʈ���� �θ� ������Ʈ�� �ڽĵ� �߿��� ã�Ƽ� ��ųʸ��� ����
    /// </summary>
    /// <typeparam name="T">������Ʈ</typeparam>
    public void Binding<T>(GameObject parent) where T : UnityEngine.Object
    {
        T[] objects = parent.GetComponentsInChildren<T>(true);

        // �ߺ��� �̸��� ���� ������Ʈ���� �ϳ��� Ű�� ����
        // �� �׷쿡�� ù ��°�� �����ϴ� ������Ʈ�� �����Ͽ� ��ųʸ��� ����
        Dictionary<string, UnityEngine.Object> objectDict = objects
            .GroupBy(comp => comp.name)
            .ToDictionary(group => group.Key, group => group.First() as UnityEngine.Object);

        _objects[typeof(T)] = objectDict;
        AssignComponentsDirectChild<T>(parent);
    }

    /// <summary>
    /// parent ������ ������Ʈ�� �̸��� ��ġ�ϴ� ������Ʈ�� ���� ���, 
    /// �ش� �ڽ��� ã�Ƽ� _objects ��ųʸ��� �ִ� ������Ʈ���� �Ҵ�
    /// </summary>
    /// <typeparam name="T">������Ʈ</typeparam>
    private void AssignComponentsDirectChild<T>(GameObject parent) where T : UnityEngine.Object
    {
        if (!_objects.TryGetValue(typeof(T), out var objects)) return;

        // �� ������Ʈ�� ���� �ݺ�
        foreach (var key in objects.Keys.ToList())
        {
            // �̹� �Ҵ�� ��� ��ŵ
            if (objects[key] != null) continue;

            // GameObject Ÿ������ Ȯ�� ��, ������ FindComponent �޼��� ȣ��
            UnityEngine.Object component = typeof(T) == typeof(GameObject)
                ? FindComponentDirectChild<GameObject>(parent, key)
                : FindComponentDirectChild<T>(parent, key);

            // ã�� ������Ʈ�� null�� �ƴ϶�� �Ҵ��ϰ�, �׷��� �ʴٸ� ���� �α� ���
            if (component != null)
            {
                objects[key] = component;
            }
            else
            {
                Debug.Log($"Binding failed for Object : {key}");
            }
        }
    }

    /// <summary>
    /// ���� �ڽĵ� �߿��� �̸��� Ư���� ���ǰ� ��ġ�ϴ� ������Ʈ ��ȯ
    /// </summary>
    /// <typeparam name="T">������Ʈ</typeparam>
    /// <param name="name">�־��� �̸��� ��ġ�ϴ� ù° �ڽ� �̸�</param>
    private T FindComponentDirectChild<T>(GameObject parent, string name) where T : UnityEngine.Object
    {
        return parent.transform
            .Cast<Transform>()
            .FirstOrDefault(child => child.name == name)
            ?.GetComponent<T>();
    }

    /// <summary>
    /// �Լ��� ����� ��ųʸ����� Ư�� Ÿ�԰� �̸��� �ش��ϴ� ������Ʈ�� �������� ����
    /// </summary>
    /// <typeparam name="T">������Ʈ</typeparam>
    public T GetComponent<T>(string componentName) where T : UnityEngine.Object
    {
        if (_objects.TryGetValue(typeof(T), out var components) && components.TryGetValue(componentName, out var component))
        {
            return component as T;
        }

        return null;
    }

    #endregion

    #region Action Binding

    /// <summary>
    /// ��ư�� �Լ� �����ϱ�
    /// </summary>
    protected Button SetButtonEvent(string buttonName, UIEventType uIEventType, Action<PointerEventData> action)
    {
        Button button = GetUI<Button>(buttonName);
        button.gameObject.SetEvent(uIEventType, action);
        Debug.Log(button.transform.ToString());
        return button;
    }

    #endregion
}

