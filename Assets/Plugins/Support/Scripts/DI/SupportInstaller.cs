using NaughtyAttributes;
using Support.Audio;
using Support.Inputs;
using Support.LevelManagement;
using Support.SLS;
using UnityEngine;
using Zenject;

namespace Support.DI
{
    public class SupportInstaller : MonoInstaller
    {
        [Required, SerializeField] private TouchScreenInputService touchScreenInputService;
        [Required, SerializeField] private SaveLoadService saveLoadService;
        [Required, SerializeField] private LevelService levelService;
        [Required, SerializeField] private AudioService audioService;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PcInputService>()
                .FromNew()
                .AsSingle()
                .NonLazy();

            Container.Bind<TouchScreenInputService>()
                .FromInstance(touchScreenInputService)
                .AsSingle()
                .NonLazy();

            Container.Bind<SaveLoadService>()
                .FromInstance(saveLoadService)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<LevelService>()
                .FromInstance(levelService)
                .AsSingle()
                .NonLazy();

            Container.Bind<AudioService>()
                .FromInstance(audioService)
                .AsSingle()
                .NonLazy();
        }
    }
}