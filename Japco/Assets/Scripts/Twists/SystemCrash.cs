using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Interactions;
using Outfrost;
using TMPro;
using UnityEngine.Assertions;

namespace Twists
{

    public class SystemCrash : CheckedMonoBehaviour
    {

        private const float secondsPerLine = 0.015f;

        [ExpectAttached] public ClickBox clickBox;
        [ExpectAttached] public TextMeshProUGUI textObject;
        [ExpectAttached] public TextMeshProUGUI textBackgroundObject;
        [ExpectAttached] public TextAsset segfault;

        private float? timeTriggered;
        private string[] segfaultLines;

        private void Start()
        {
            CheckReferences();
            segfaultLines = segfault.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }

        private void Update()
        {
            if (!Util.IsPrefab(gameObject))
            {
                if (!timeTriggered.HasValue)
                {
                    if (clickBox.Clicked)
                    {
                        timeTriggered = Time.unscaledTime;

                        Time.timeScale = 0.0f;

                        StartCoroutine(SceneLoader.NextSceneAfterAsync(5.0f));
                    }
                }
                else
                {
                    int lines = Util.Clamp((int)((Time.unscaledTime - timeTriggered.Value) / secondsPerLine), 0,
                            segfaultLines.Length);

                    textObject.text = firstSegfaultLines(lines);
                    textBackgroundObject.text = "<mark=#000000>" + firstSegfaultLines(lines);
                }
            }
        }

        private string firstSegfaultLines(int n)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                builder.Append(segfaultLines[i]);
                builder.Append('\n');
            }
            return builder.ToString();
        }

    }

}
