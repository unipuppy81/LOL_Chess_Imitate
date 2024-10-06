using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    private Vector2 _lastTouchPos = Vector2.zero;
    private Vector2 _currentTouchPos = Vector2.zero;
    private Vector3 _prePos = Vector3.zero;

    private Vector3 _beforePosition;
    private Vector3 _offset = new Vector3(0.0f, 1.0f, 0.0f);

    [SerializeField] private GameObject _movableObj;

    private const string _moveableTAG = "Moveable";


    private void Update()
    {
#if UNITY_EDITOR
        _lastTouchPos = _currentTouchPos;
        _currentTouchPos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            TouchBeganEvent();
            _prePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition != _prePos) { TouchMovedEvent(); }
            else { TouchStayEvent(); }
        }

        if (Input.GetMouseButtonUp(0))
        {
            TouchEndedEvent();
        }
#else
            // Multi-touch 방지 
            if (Input.touchCount <= 0) { return; }

            Touch touchInfo = Input.GetTouch(0);
            _lastTouchPos = _currentTouchPos;
            _currentTouchPos = touchInfo.position;

            switch (touchInfo.phase)
            {
                case TouchPhase.Began:
                    TouchBeganEvent();
                    break;

                case TouchPhase.Moved:
                    TouchMovedEvent();
                    break;

                case TouchPhase.Stationary:
                    TouchStayEvent();
                    break;

                case TouchPhase.Ended:
                    TouchEndedEvent();
                    break;
            }
#endif

#if UNITY_EDITOR
        // 캐릭터 Ray 확인용 
        if (_movableObj != null)
        {
            Ray ray = new Ray(_movableObj.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        }


        // TEST Code
        Vector3 touchPos = new Vector3(0, 0, 0);

#if UNITY_EDITOR
        touchPos = Input.mousePosition;
#else
            touchPos = Input.GetTouch(0).position;
#endif

        Ray test_ray = Camera.main.ScreenPointToRay(touchPos);

        Debug.DrawRay(test_ray.origin, test_ray.direction * 1000, Color.blue);

#endif
    }

    private void TouchBeganEvent()
    {
        _movableObj = OnClickObjUsingTag(_moveableTAG);

        if (_movableObj != null)
        {
            _beforePosition = _movableObj.transform.position;
        }
    }

    private void TouchMovedEvent()
    {
        if (_movableObj != null)
        {
            Vector3 touchPos = Vector3.zero;
#if UNITY_EDITOR
            touchPos = Input.mousePosition;
#else
                touchPos = Input.GetTouch(0).position;
#endif
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 10));

            //_movableObj.transform.position = worldPos;
            _movableObj.transform.position = Vector3.MoveTowards(_movableObj.transform.position, worldPos, Time.deltaTime * 100f);
        }
    }

    private void TouchStayEvent()
    {

    }

    private void TouchEndedEvent()
    {
        if (_movableObj != null)
        {
            Ray ray = new Ray(_movableObj.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);

            if (hitInfo.collider != null)
            {
                Debug.Log("hit info : " + hitInfo.collider.gameObject.name);
                _movableObj.transform.position = hitInfo.collider.gameObject.transform.position;
                _movableObj.transform.position += _offset;
            }
            else
            {
                _movableObj.transform.position = _beforePosition;
            }
        }

        //_movableObj = null;
    }

    private GameObject OnClickObjUsingTag(string tag)
    {
        Vector3 touchPos = new Vector3(0, 0, 0);

#if UNITY_EDITOR
        touchPos = Input.mousePosition;
#else
            touchPos = Input.GetTouch(0).position;
#endif

        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.collider != null)
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject != null)
            {
                if (hitObject.gameObject.tag.Equals(tag))
                {
                    return hitObject;
                }
            }
        }

        return null;
    }
}
