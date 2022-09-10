using NaughtyAttributes;
using Support.Audio;
using Support.SLS;
using UnityEngine;
using Zenject;

namespace Support.DI
{
    public class SupportInstaller : MonoInstaller
    {
        [Required] 
        [SerializeField] private SaveLoadSystem saveLoadSystem;
        [Required] 
        [SerializeField] private TouchScreenInputSystem touchScreenInputSystem;
        [Required] 
        [SerializeField] private VFXController vfxController;
        [Required]
        [SerializeField] private AudioController audioController;

        public override void InstallBindings()
        {
            Container.Bind<TouchScreenInputSystem>()
                .FromInstance(touchScreenInputSystem)
                .AsSingle()
                .NonLazy();

            Container.Bind<VFXController>()
                .FromInstance(vfxController)
                .AsSingle()
                .NonLazy();

            Container.Bind<SaveLoadSystem>()
                .FromInstance(saveLoadSystem)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<LevelManager>()
                .FromNew()
                .AsSingle()
                .NonLazy();

            Container.Bind<AudioController>()
                .FromInstance(audioController)
                .AsSingle()
                .NonLazy();
        }
    }
}