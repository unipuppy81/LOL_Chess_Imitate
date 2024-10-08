using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    private Vector2 _lastTouchPos = Vector2.zero;
    private Vector2 _currentTouchPos = Vector2.zero;
    private Vector3 _prePos = Vector3.zero;

    private Vector3 _beforePosition;
    private Vector3 _offset = new Vector3(0.0f, 0.3f, 0.0f);

    [SerializeField] private GameObject _itemObj;
    [SerializeField] private GameObject currentItemTile;

    private const string _itemTag = "Item";
    private bool _isReturning = false;

    public List<GameObject> _items = new List<GameObject>(10);
    public GameObject itemPrefeb; // ������ Ÿ�� ������
    //[SerializeField] private bool isitem = false;

    private void Start()
    {
        GameObject parentObject = GameObject.Find("ItemPush");
        if (parentObject != null)
        {
            // �θ� ������Ʈ�� ��� �ڽ� ������Ʈ ��������
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                GameObject child = parentObject.transform.GetChild(i).gameObject;

                // ����Ʈ ũ�Ⱑ 8���� ���� ���� �߰�
                if (_items.Count < 10)
                {
                    _items.Add(child);
                }
            }
        }
        else
        {
            Debug.LogError("Parent object 'ItemPush' not found!");
        }
        //_items.Add
        GameObject item1 = Instantiate(itemPrefeb, _items[0].transform.position,
            Quaternion.identity, this.transform);
        item1.tag = "Item";
        HexTile itempushtile = _items[0].GetComponent<HexTile>();
        itempushtile.isItemTile = true;
    }

    private void Update()
    {
        _lastTouchPos = _currentTouchPos;
        _currentTouchPos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            TouchBeganEvent();
            FindcurrentTile();
            _prePos = Input.mousePosition;
            Debug.Log(_beforePosition);
        }

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition != _prePos)
            {
                TouchMovedEvent();
            }
            else
            {
                TouchStayEvent();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            TouchEndedEvent();
        }
        ItemReturn();
        // ĳ���� Ray Ȯ�ο� 
        if (_itemObj != null)
        {
            Ray ray = new Ray(_itemObj.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        }

        // TEST Code
        Vector3 touchPos = Input.mousePosition;
        Ray test_ray = Camera.main.ScreenPointToRay(touchPos);
        Debug.DrawRay(test_ray.origin, test_ray.direction * 1000, Color.blue);
    }

    private void TouchBeganEvent()
    {
        _itemObj = OnClickObjUsingTag(_itemTag);

        if (_itemObj != null)
        {
            _beforePosition = _itemObj.transform.position;
        }
    }

    private void TouchMovedEvent()
    {
        if (_itemObj != null)
        {
            Vector3 touchPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 10));

            _itemObj.transform.position = worldPos;
        }
    }

    private void TouchStayEvent()
    {
        // ���콺�� �������� ���� ���� ������ �ʿ��ϸ� ���⿡ �ۼ�
    }

    private void TouchEndedEvent()
    {
        if (_itemObj != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer("ItemPush"));

            if (hitInfo.collider != null && hitInfo.collider.gameObject.GetComponent<HexTile>().isItemTile == false)
            {
                Debug.Log("hit info : " + hitInfo.collider.gameObject.name);
                _itemObj.transform.position = hitInfo.collider.gameObject.transform.position;
                _itemObj.transform.position += _offset;
                hitInfo.collider.gameObject.GetComponent<HexTile>().isItemTile = true;
                currentItemTile.GetComponent<HexTile>().isItemTile = false;
            }
            else
            {
                _isReturning = true;
            }
        }
    }

    private GameObject OnClickObjUsingTag(string tag)
    {
        Vector3 touchPos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.collider != null)
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject != null && hitObject.gameObject.tag.Equals(tag))
            {
                return hitObject;
            }
        }
        return null;
    }
    private GameObject FindcurrentTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("ItemPush")))
        {
            currentItemTile = hit.collider.gameObject;
            Debug.Log(hit.collider.gameObject);
        }
        return null;
    }
    private void ItemReturn()
    {
        // ��ü�� ���� ��ġ�� �ε巴�� ���ư��� �ϱ� ���� ����
        if (_isReturning && _itemObj != null)
        {
            _itemObj.transform.position = Vector3.MoveTowards(_itemObj.transform.position, _beforePosition, Time.deltaTime * 30f);

            // ���� ��ġ�� �����ϸ� ��ȯ ������ ����
            if (Vector3.Distance(_itemObj.transform.position, _beforePosition) < 0.01f)
            {
                _isReturning = false;
                currentItemTile.GetComponent<HexTile>().isItemTile = false;
            }
        }
    }
}
