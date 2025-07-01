using AccuracyInResults.Configuration;
using JetBrains.Annotations;
using SiraUtil.Affinity;
using TMPro;

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
        __instance._rankText.paragraphSpacing = Config.EnableFullComboAccDisplay ? Config.FullComboAccVerticalSpacing : 0;
        __instance._rankText.verticalAlignment = Config.EnableFullComboAccDisplay ? VerticalAlignmentOptions.Bottom : VerticalAlignmentOptions.Geometry;
        
        int score = __instance._levelCompletionResults.modifiedScore;
        float acc = score / (float)_maxScore;
        
        Plugin.DebugMessage($"Score: {score}");
        Plugin.DebugMessage($"Max Score: {_maxScore}");
        Plugin.DebugMessage($"Determined Acc: {acc * 100}%");
        
        __instance._rankText.text = $"{(acc * 100).ToString("F" + Config.DecimalPrecision)}<alpha=#A0><size=67%>%</size><alpha=#FF>";

        if (!Config.EnableFullComboAccDisplay)
        {
            return;
        }
        
        int fcScore = CutScorePatchManager.ScoreWithoutMisses;
        float fcAcc = fcScore / (float)CutScorePatchManager.MaxScoreWithoutMisses;
            
        Plugin.DebugMessage($"FC Score: {fcScore}");
        Plugin.DebugMessage($"FC Max Score: {CutScorePatchManager.MaxScoreWithoutMisses}");
        Plugin.DebugMessage($"Determined FC Acc: {fcAcc * 100}%");
            
        __instance._rankText.text +=
            $"\n<size=3><alpha=#A0>FC <alpha=#FF>{(fcAcc * 100).ToString("F" + Config.DecimalPrecision)}<alpha=#A0><size=2>%</size></size>";
    }
}