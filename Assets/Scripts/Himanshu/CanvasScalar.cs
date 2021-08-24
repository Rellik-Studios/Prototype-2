using UnityEngine;

namespace Himanshu
{
    public class CanvasScalar : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (gameSettings.Instance.gameMode == gameSettings.eGameModes.localMultiplayer)
            {
                var localPosition = transform.Find("MiniMapBG").gameObject.GetComponent<RectTransform>().localPosition;
                transform.Find("MiniMapBG").gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, localPosition.y, 0f);
            }
            if (gameSettings.Instance.gameMode == gameSettings.eGameModes.timeTrial)
            {
                var localPosition = transform.Find("MiniMapBG").gameObject.GetComponent<RectTransform>().localPosition;
                transform.Find("MiniMapBG").gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-720f, localPosition.y, 0f);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
