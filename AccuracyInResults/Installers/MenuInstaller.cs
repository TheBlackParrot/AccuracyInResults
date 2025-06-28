using AccuracyInResults.Managers;
using AccuracyInResults.UI;
using JetBrains.Annotations;
using Zenject;

namespace AccuracyInResults.Installers;

[UsedImplicitly]
internal class MenuInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<ModSettingsViewController>().AsSingle();
        Container.BindInterfacesAndSelfTo<ResultsTextChanges>().AsSingle();
    }
}