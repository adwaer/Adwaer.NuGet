using Adwaer.DependencyInversion;

namespace Adwaer.Mapping
{
    public class MapperServiceFactory
    {
        private readonly IDiContainer _diContainer;

        public MapperServiceFactory(IDiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public TDest GetFrom<TDest, TDto>(TDto model)
        {
            var mapperService = _diContainer.Resolve<IMapperService<TDest, TDto>>();
            return mapperService.GetFrom(model);
        }
    }
}
