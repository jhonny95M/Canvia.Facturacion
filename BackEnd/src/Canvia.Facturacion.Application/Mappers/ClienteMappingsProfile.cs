using AutoMapper;
using Canvia.Facturacion.Application.Dtos.Response;
using Canvia.Facturacion.Domain.EntitiesAdoNet;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;

namespace Canvia.Facturacion.Application.Mappers;

public sealed class ClienteMappingsProfile : Profile
{
    public ClienteMappingsProfile()
    {
        CreateMap<BaseEntityResponse<Cliente>, BaseEntityResponse<ClienteResponseDto>>()
            .ReverseMap();
        CreateMap<Cliente, ClienteResponseDto>()
            .ReverseMap();

        CreateMap<Cliente, ClienteSelectResponseDto>()
            .ReverseMap();
        CreateMap<BaseEntityResponse<Cliente>, BaseEntityResponse<ClienteSelectResponseDto>>()
          .ReverseMap();
        CreateMap<Cliente, ClienteSelectResponseDto>()
            .ReverseMap();
        CreateMap<IEnumerable<Cliente>,List<ClienteSelectResponseDto>>()
            .ReverseMap();
    }
}
