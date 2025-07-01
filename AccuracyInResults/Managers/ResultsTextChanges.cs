using AccuracyInResults.Configuration;
using HMUI;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Zenject;

namespace AccuracyInResults.Managers;

[UsedImplicitly]
internal class ResultsTextChanges : IInitializable
{
    private static PluginConfig Config => PluginConfig.Instance;
    private static ResultsViewController? _resultsViewController;

    internal ResultsTextChanges(ResultsViewController resultsViewController)
    {
        _resultsViewController = resultsViewController;
    }
    
    internal static VerticalAlignmentOptions? InitialVerticalAlignmentOptions;

    public void Initialize()
    {
        if (_resultsViewController == null)
        {
            return;
        }
        
        InitialVerticalAlignmentOptions ??= _resultsViewController._rankText.verticalAlignment;
        
        _resultsViewController._rankText.enableWordWrapping = false;
        _resultsViewController._rankText.richText = true;
        _resultsViewController._rankText.color = Color.white;
        _resultsViewController._rankText.fontSize = _resultsViewController._goodCutsPercentageText.fontSize;

        Transform? rankTitle = _resultsViewController.transform.FindChildRecursively("RankTitle");
        if(rankTitle?.TryGetComponent(out BGLib.Polyglot.LocalizedTextMeshProUGUI localizedTextMeshProUGUI) ?? false)
        {
            Object.DestroyImmediate(localizedTextMeshProUGUI);
        }
        // ReSharper disable once InvertIf
        if (rankTitle?.TryGetComponent(out CurvedTextMeshPro textMeshPro) ?? false)
        {
            textMeshPro.rectTransform.offsetMin = new Vector2(-26.5f, textMeshPro.rectTransform.offsetMin.y);
            textMeshPro.text = Config.AccuracyText;
        }
    }
}