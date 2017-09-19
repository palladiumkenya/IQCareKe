using StructureMap;

namespace IQCare.Web.API.DependancyResolution {
    public static class IoC {


        public static IContainer Initialize() {
            var container = new Container(c => c.AddRegistry<DefaultRegistry>());
            return container;
        }
    }
}