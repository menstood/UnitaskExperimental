using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

public class TestBinding : MonoBehaviour
{
    [SerializeField]
    private Text label;
    [SerializeField]
    private Button button;
    private AsyncReactiveProperty<int> testProp = new AsyncReactiveProperty<int>(0);

    async UniTaskVoid Start()
    {
        testProp.BindTo(label);
        await Click();
        Debug.Log("Do it again.");
        await Click();
        label.text = "Done";
    }

    async UniTask Click()
    {
        await button.OnClickAsAsyncEnumerable()
            .Take(10)
            .ForEachAsync(_ =>
        {
            Debug.Log("Every clicked");
            testProp.Value += 1;
        });
        Debug.Log("Done. 10 times");
    }

}
