using AutoMapper;
using Canvia.Facturacion.Application.Dtos.Response;
using Canvia.Facturacion.Domain.EntitiesAdoNet;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;

namespace Canvia.Facturacion.Application.Mappers;

public sealed class ClienteMappingsProfile : Profile
{
    public ClienteMappingsProfile()
    {
        CreateMap<Cliente, ClienteResponseDto>()
            .ReverseMap();
        CreateMap<BaseEntityResponse<Cliente>, BaseEntityResponse<ClienteResponseDto>>()
            .ReverseMap();
        CreateMap<Cliente, ClienteSelectResponseDto>()
            .ReverseMap();
    }
}
