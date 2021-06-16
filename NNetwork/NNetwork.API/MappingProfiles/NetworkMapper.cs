using AutoMapper;
using NNetwork.API.Dtos;
using NNetwork.KerasApplication.Arguments;
using NNetwork.KerasApplication.Layers;
using NNetwork.KerasApplication.Networks;

namespace NNetwork.API.MappingProfiles
{
    public class NetworkMapper : Profile
    {
        public NetworkMapper()
        {
            CreateMap<NetworkInitializerDto, NetworkInitializationModel>().ReverseMap();
            CreateMap<LayerDto, Layer>().ReverseMap();
            CreateMap<ParametersDto, Parameters>().ReverseMap();
        }
    }
}
