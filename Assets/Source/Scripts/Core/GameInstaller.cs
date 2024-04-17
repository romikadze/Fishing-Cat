using Source.Scripts.Core.Data;
using Source.Scripts.Core.Services;
using Source.Scripts.Fishing;
using Source.Scripts.InputSystems;
using Source.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GestureReceiver _gestureReceiver;
        [SerializeField] private FishingRod _fishingRod;
        [SerializeField] private FishingMinigame _fishingMinigame;
        [SerializeField] private Swimmer _swimmer;

        public override void InstallBindings()
        {
            Container.BindFactory<string, Fish, FishFactory>().FromFactory<PrefabResourceFactory<Fish>>();
            Container.Bind<PauseService>()
                .FromComponentInNewPrefabResource(PathService.Services.PAUSE_SERVICE)
                .AsSingle();
            Container.Bind<SaveService>()
                .FromComponentInNewPrefabResource(PathService.Services.SAVE_SERVICE)
                .AsSingle();
            Container.Bind<GestureReceiver>()
                .FromInstance(_gestureReceiver)
                .AsSingle();
            Container.Bind<FishingRod>()
                .FromInstance(_fishingRod)
                .AsSingle();
            Container.Bind<FishingMinigame>()
                .FromInstance(_fishingMinigame)
                .AsSingle();
            Container.Bind<Swimmer>()
                .FromInstance(_swimmer)
                .AsSingle();
        }
    }
}