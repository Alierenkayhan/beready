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
        private UnityEvent actionCallback;
        private UnityEvent cancelCallback;

        private void OnEnable() {
            configure();
        }
        
        public void ActionDismiss() {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            actionCallback?.Invoke();
            if (GameManager.localPlayer != null) {
                GameManager.localPlayer.m_MouseLook.m_cursorIsLocked = true;
                GameManager.localPlayer.m_MouseLook.SetCursorLock(true);
            }
        }
        
        public void CancelDismiss() {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            cancelCallback?.Invoke();
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

        public void alert(string title, string body, string action, string cancel = "Ayrıl", UnityEvent actionCallback = null, UnityEvent cancelCallback = null) {
            if (GameManager.localPlayer != null) {
                GameManager.localPlayer.m_MouseLook.SetCursorLock(false);
                GameManager.localPlayer.m_MouseLook.m_cursorIsLocked = false;
            }
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            configure();
            _alertTitleTmp.text = title;
            _alertBodyTmp.text = body;
            _alertActionTmp.text = action;
            if (alertCancel != null) {
                _alertCancelTmp.text = cancel;
            }

            if (actionCallback != null) {
                this.actionCallback = actionCallback;
            } else {
                this.actionCallback = null;
            }
            
            if (cancelCallback != null) {
                this.cancelCallback = cancelCallback;
            } else {
                this.cancelCallback = null;
            }
        }

        public void reloadGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
