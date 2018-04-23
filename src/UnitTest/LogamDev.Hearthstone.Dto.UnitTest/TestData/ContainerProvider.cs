using Unity;

namespace LogamDev.Hearthstone.Dto.UnitTest.TestData
{
    public static class ContainerProvider
    {
        private static IUnityContainer originalContainer = null;

        public static IUnityContainer OriginalContainer
        {
            get
            {
                if (originalContainer == null)
                {
                    originalContainer = new UnityContainer();
                    UnityConfig.Register(originalContainer);
                }

                return originalContainer;
            }
        }
    }
}
