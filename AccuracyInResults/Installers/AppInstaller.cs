using AccuracyInResults.Managers;
using JetBrains.Annotations;
using Zenject;

namespace AccuracyInResults.Installers;

[UsedImplicitly]
internal class AppInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<ResultsPatchManager>().AsSingle();
    }
}