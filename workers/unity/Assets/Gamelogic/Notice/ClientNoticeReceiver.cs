using Improbable.Notes;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.Notes
{
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientNoticeReceiver : MonoBehaviour
    {
        [Require] private Notice.Reader NoticeReader;

				public Text noticeText;

				void Start() {
						noticeText = transform.Find("Message/Canvas/Text").GetComponent<Text>();
            noticeText.text = NoticeReader.Data.text;
    		}
    }
}
