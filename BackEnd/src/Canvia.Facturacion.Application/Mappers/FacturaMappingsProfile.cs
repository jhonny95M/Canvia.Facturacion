using AutoMapper;
using Canvia.Facturacion.Application.Dtos.Request;
using Canvia.Facturacion.Application.Dtos.Response;
using Canvia.Facturacion.Domain.EntitiesAdoNet;
using Canvia.Facturacion.Domain.EntitiesEntityFramework;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;

namespace Canvia.Facturacion.Application.Mappers;

public sealed class FacturaMappingsProfile : Profile
{
    public FacturaMappingsProfile()
    {
        CreateMap<FacturaCabeceraAdoNet, FacturaCabeceraResponseDto>()
            .ReverseMap();
        CreateMap<Domain.EntitiesAdoNet.FacturaDetalle, FacturaDetalleResponseDto>()
            .ReverseMap();
        CreateMap<BaseEntityResponse<FacturaCabeceraAdoNet>, BaseEntityResponse<FacturaCabeceraResponseDto>>()
            .ReverseMap();
        CreateMap<BaseEntityResponse<FacturaCabecera>, BaseEntityResponse<FacturaResponseDto>>()
            .ReverseMap();
        CreateMap<FacturaCabecera, FacturaResponseDto>()
            .ForMember(c => c.Nombre, c => c.MapFrom(c => c.Cliente!.Nombre))
            .ForMember(c => c.Apellido, c => c.MapFrom(c => c.Cliente!.Apellido))
            .ReverseMap();

        CreateMap<FacturaCabeceraRequestDto, FacturaCabeceraAdoNet>();
        CreateMap<FacturaDetalleRequestDto, Domain.EntitiesAdoNet.FacturaDetalle>();

    }
}