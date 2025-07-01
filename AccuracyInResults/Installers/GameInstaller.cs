using AccuracyInResults.Managers;
using JetBrains.Annotations;
using Zenject;

namespace AccuracyInResults.Installers;

[UsedImplicitly]
internal class GameInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CutScorePatchManager>().AsSingle();
    }
}