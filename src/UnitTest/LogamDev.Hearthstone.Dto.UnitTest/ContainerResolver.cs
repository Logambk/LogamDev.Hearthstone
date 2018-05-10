using Unity;

namespace LogamDev.Hearthstone.Dto.UnitTest
{
    public static class ContainerResolver
    {
        private static IUnityContainer originalContainer = null;

        public static T Resolve<T>()
        {
            if (originalContainer == null)
            {
                originalContainer = new UnityContainer();
                UnityConfig.Register(originalContainer);
            }

            return originalContainer.Resolve<T>();
        }
    }
}
