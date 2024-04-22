using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UI {
    public class AlertController : MonoBehaviour {
        [SerializeField] private GameObject alertTitle;
        [SerializeField] private GameObject alertBody;
        [SerializeField] private GameObject alertAction;
        [SerializeField] private GameObject alertCancel;
        
        private TextMeshProUGUI _alertTitleTmp;
        private TextMeshProUGUI _alertBodyTmp;
        private TextMeshProUGUI _alertActionTmp;
        private TextMeshProUGUI _alertCancelTmp;

        private bool _configured;
        private UnityEvent callback;

        private void OnEnable() {
            configure();
        }
     

        public void dismiss() {
            // print("Dismiss");
            // if (GameManager.activeLevel == 0)
            // {
            //     if (_alertBodyTmp.text.StartsWith("Hoşgeldin"))
            //     {
            //         alert("Deprem Simülasyonu ", "Şuan lobidesin. İlerde sol tarafta bulunan odalardan belirli seviyelerde deprem simülasyonuna katılabilirsin.", "Tamam", "Ayrıl", callback);
            //     }
            // }
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            callback?.Invoke();
            // print($"Callback fired = {callback}");
            if (GameManager.localPlayer != null) {
                GameManager.localPlayer.m_MouseLook.m_cursorIsLocked = true;
                GameManager.localPlayer.m_MouseLook.SetCursorLock(true);
            }
        }
        
        public void dismissWithoutCancel() {
            // print("Dismiss");
            // if (GameManager.activeLevel == 0)
            // {
            //     if (_alertBodyTmp.text.StartsWith("Hoşgeldin"))
            //     {
            //         alert("Deprem Simülasyonu ", "Şuan lobidesin. İlerde sol tarafta bulunan odalardan belirli seviyelerde deprem simülasyonuna katılabilirsin.", "Tamam", "Ayrıl", callback);
            //     }
            // }
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            // print($"Callback fired = {callback}");
            if (GameManager.localPlayer != null) {
                GameManager.localPlayer.m_MouseLook.m_cursorIsLocked = true;
                GameManager.localPlayer.m_MouseLook.SetCursorLock(true);
            }
        }

        private void configure() {
            if (!_configured) {
                if (_alertActionTmp == null) _alertActionTmp = alertAction.GetComponent<TextMeshProUGUI>();
                if (_alertBodyTmp == null) _alertBodyTmp = alertBody.GetComponent<TextMeshProUGUI>();
                if (_alertTitleTmp == null) _alertTitleTmp = alertTitle.GetComponent<TextMeshProUGUI>();
                if (alertCancel != null) {
                    if (_alertCancelTmp == null) _alertCancelTmp = alertCancel.GetComponent<TextMeshProUGUI>();
                }
                
                _configured = true;
            }
        }

        private void configureGUI(bool withCancel)
        {
            if (withCancel)
            {
                var _cancel = alertCancel.transform.parent.gameObject.GetComponent<RectTransform>();
                _cancel.gameObject.SetActive(true);
                _cancel.anchorMin = new Vector2(0.25f, _cancel.anchorMin.y);
                _cancel.anchorMax = new Vector2(0.45f, _cancel.anchorMax.y);
                var _alert = alertAction.transform.parent.gameObject.GetComponent<RectTransform>();
                _alert.gameObject.SetActive(true);
                _alert.anchorMin = new Vector2(0.55f, _alert.anchorMin.y);
                _alert.anchorMax = new Vector2(0.75f, _alert.anchorMax.y);
            }
            else
            {
                alertCancel.transform.parent.gameObject.SetActive(false);
                var _alert = alertAction.transform.parent.gameObject.GetComponent<RectTransform>();
                _alert.gameObject.SetActive(true);
                _alert.anchorMin = new Vector2(0.4f, _alert.anchorMin.y);
                _alert.anchorMax = new Vector2(0.6f, _alert.anchorMax.y);
            }
        }

        public void alert(string title, string body, string action, string cancel = null, UnityEvent dismissCallbackAction = null) {
            if (GameManager.localPlayer != null) {
                GameManager.localPlayer.m_MouseLook.SetCursorLock(false);
                GameManager.localPlayer.m_MouseLook.m_cursorIsLocked = false;
            }
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            configure();
            configureGUI(cancel != null);
            _alertTitleTmp.text = title;
            _alertBodyTmp.text = body;
            _alertActionTmp.text = action;
            if (alertCancel != null) {
                _alertCancelTmp.text = cancel;
            }

            if (dismissCallbackAction != null) {
                callback = dismissCallbackAction;
            } else {
                callback = null;
            }
        }

        public void reloadGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
