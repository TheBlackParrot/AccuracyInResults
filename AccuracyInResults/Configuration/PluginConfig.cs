using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using JetBrains.Annotations;

// ReSharper disable RedundantDefaultMemberInitializer

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace AccuracyInResults.Configuration;

[UsedImplicitly]
internal class PluginConfig
{
    public static PluginConfig Instance { get; set; } = null!;

    public virtual bool EnableFullComboAccDisplay { get; set; } = false;
    public virtual int DecimalPrecision { get; set; } = 2;
    public virtual string AccuracyText { get; set; } = "accuracy";
    public virtual int FullComboAccVerticalSpacing { get; set; } = -40;
}