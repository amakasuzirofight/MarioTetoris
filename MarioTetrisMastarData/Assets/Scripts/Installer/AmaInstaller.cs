using UnityEngine;
using Zenject;
using Player;
using Test;
public class AmaInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<IPlayerAction>()
            .To<PlayerMove2D>()
            .AsCached();
        Container
            .Bind<IPlayerAction>()
            .To<PlayerJump>()
            .AsCached();
    }
}