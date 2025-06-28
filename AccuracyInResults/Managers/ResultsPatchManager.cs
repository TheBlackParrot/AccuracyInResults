using AccuracyInResults.Configuration;
using JetBrains.Annotations;
using SiraUtil.Affinity;

namespace AccuracyInResults.Managers;

[UsedImplicitly]
internal class ResultsPatchManager : IAffinity
{
    private static PluginConfig Config => PluginConfig.Instance;
    
    private static int _maxScore;

    [AffinityPostfix]
    [AffinityPatch(typeof(PrepareLevelCompletionResults), nameof(PrepareLevelCompletionResults.FillLevelCompletionResults))]
    // ReSharper disable once InconsistentNaming
    private void FillLevelCompletionResultsPatch(PrepareLevelCompletionResults __instance)
    {
        _maxScore = __instance._scoreController.immediateMaxPossibleModifiedScore;
    }

    [AffinityPostfix]
    [AffinityPatch(typeof(ResultsViewController), nameof(ResultsViewController.SetDataToUI))]
    // ReSharper disable once InconsistentNaming
    private void SetDataToUIPatch(ResultsViewController __instance)
    {
        int score = __instance._levelCompletionResults.modifiedScore;
        float acc = score / (float)_maxScore;
        
        Plugin.DebugMessage($"Score: {score}");
        Plugin.DebugMessage($"Max Score: {_maxScore}");
        Plugin.DebugMessage($"Determined Acc: {acc * 100}%");
        
        __instance._rankText.text = $"{(acc * 100).ToString("F" + Config.DecimalPrecision)}<alpha=#A0><size=67%>%</size>";
    }
}