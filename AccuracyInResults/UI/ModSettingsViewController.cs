using System;
using AccuracyInResults.Configuration;
using JetBrains.Annotations;
using Zenject;

namespace AccuracyInResults.UI;

[UsedImplicitly]
internal class ModSettingsViewController : IInitializable, IDisposable
{
    private static PluginConfig Config => PluginConfig.Instance;
    
    public void Initialize()
    {
        BeatSaberMarkupLanguage.Settings.BSMLSettings.Instance.AddSettingsMenu("AccuracyInResults",
            "AccuracyInResults.UI.BSML.ModSettings.bsml", this);
    }

    public void Dispose()
    {
        BeatSaberMarkupLanguage.Settings.BSMLSettings.Instance?.RemoveSettingsMenu(this);
    }

    protected bool EnableFullComboAccDisplay
    {
        get => Config.EnableFullComboAccDisplay;
        set => Config.EnableFullComboAccDisplay = value;
    }
    protected int DecimalPrecision
    {
        get => Config.DecimalPrecision;
        set => Config.DecimalPrecision = value;
    }
    protected int FullComboAccVerticalSpacing
    {
        get => Config.FullComboAccVerticalSpacing;
        set => Config.FullComboAccVerticalSpacing = value;
    }
}