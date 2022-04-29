using NaughtyAttributes;
using Support.SLS;
using UnityEngine;
using Zenject;

namespace Support.DI
{
    public class SupportInstaller : MonoInstaller
    {
        [Required] 
        [SerializeField] private GameController gameController;
        [Required] 
        [SerializeField] private SaveLoadSystem saveLoadSystem;
        [Required] 
        [SerializeField] private TouchScreenInputSystem touchScreenInputSystem;
        [Required] 
        [SerializeField] private VFXController vfxController;

        public override void InstallBindings()
        {
            Container.Bind<GameController>()
                .FromInstance(gameController)
                .AsSingle()
                .NonLazy();

            Container.Bind<TouchScreenInputSystem>()
                .FromInstance(touchScreenInputSystem)
                .AsSingle()
                .NonLazy();

            Container.Bind<VFXController>()
                .FromInstance(vfxController);

            Container.Bind<SaveLoadSystem>()
                .FromInstance(saveLoadSystem)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<LevelManager>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}