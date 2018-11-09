/**
 * UISafeArea.cs
 * Author: Luke Holland (http://lukeholland.me/)
 */

namespace UnityEngine.UI
{

    [RequireComponent(typeof(RectTransform))]
    public class UISafeArea : MonoBehaviour
    {

        [SerializeField] private bool _ignoreTop = false;
        [SerializeField] private bool _ignoreBottom = false;
        [SerializeField] private bool _ignoreLeft = false;
        [SerializeField] private bool _ignoreRight = false;

        private RectTransform _rectTransform;
        private Vector2 _lastMin, _lastMax;

        protected void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            UpdateSafeArea();
        }

        protected void OnEnable()
        {
            UpdateSafeArea();
        }

        protected void OnRectTransformDimensionsChange()
        {
            if (!gameObject.activeInHierarchy) return;

            StartCoroutine(RoutineDelayUpdate());
        }

        public void UpdateSafeArea(bool force = false)
        {
            if (_rectTransform == null || !gameObject.activeInHierarchy) return;

            Canvas canvas = GetComponentInParent<Canvas>();

            if (canvas != null && _rectTransform.parent != null)
            {
                Rect safeArea = Screen.safeArea;
                RectTransform parentTransform = _rectTransform.parent.GetComponent<RectTransform>();
                Rect parentRect = parentTransform.rect;

                // convert screen rect to canvas space, relative to the parent
                Vector2 min, max;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentTransform, safeArea.min, canvas.worldCamera, out min);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentTransform, safeArea.max, canvas.worldCamera, out max);

                // apply
                Vector2 offsetMin = new Vector2(min.x - parentRect.xMin, min.y - parentRect.yMin);
                Vector2 offsetMax = new Vector2(max.x - parentRect.xMax, max.y - parentRect.yMax);

                if (offsetMin != _lastMin || offsetMax != _lastMax || force)
                {
                    // validate change before applying, prevents recursion caused by 'OnRectTransformDimensionsChange'
                    _lastMin = offsetMin;
                    _lastMax = offsetMax;

                    if (_ignoreLeft) offsetMin.x = _rectTransform.offsetMin.x;
                    if (_ignoreRight) offsetMax.x = _rectTransform.offsetMax.x;
                    if (_ignoreBottom) offsetMin.y = _rectTransform.offsetMin.y;
                    if (_ignoreTop) offsetMax.y = _rectTransform.offsetMax.y;

                    _rectTransform.offsetMin = offsetMin;
                    _rectTransform.offsetMax = offsetMax;
                }
            }
        }

        private System.Collections.IEnumerator RoutineDelayUpdate()
        {
            yield return new WaitForEndOfFrame();

            UpdateSafeArea(false);
        }

    }

}