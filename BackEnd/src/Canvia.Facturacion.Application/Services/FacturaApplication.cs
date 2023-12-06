using AutoMapper;
using Canvia.Facturacion.Application.Commons;
using Canvia.Facturacion.Application.Dtos.Request;
using Canvia.Facturacion.Application.Dtos.Response;
using Canvia.Facturacion.Application.Interfaces;
using Canvia.Facturacion.Application.Validators.Factura;
using Canvia.Facturacion.Domain.EntitiesAdoNet;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;
using Canvia.Facturacion.Infraestructure.Persistences.Interfaces;
using Canvia.Facturacion.Utilities.Static;
using FluentValidation;

namespace Canvia.Facturacion.Application.Services;

public sealed class FacturaApplication : IFacturaApplication
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly FacturaValidator validationRules;

    public FacturaApplication(IUnitOfWork unitOfWork, IMapper mapper, FacturaValidator validationRules)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.validationRules = validationRules;
    }

    public async Task<BaseResponse<bool>> AnularFacturaAsync(int idFactura)
    {
        var response = new BaseResponse<bool>();
        var factura = await unitOfWork.Facturas.ObtenerPorIdAsync(idFactura);
        if(factura is not null)
        {
            await unitOfWork.Facturas.AnularFacturaAsync(idFactura);
            unitOfWork.Commit();
            response.IsSucces = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        else {         
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> EditarFacturaAsync(int id, FacturaCabeceraRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var validationResult = await validationRules.ValidateAsync(requestDto);
        if (!validationResult.IsValid)
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_VALIDATE;
            response.Errors = validationResult.Errors;
            return response;
        }
        var factura = mapper.Map<FacturaCabeceraAdoNet>(requestDto);
        factura.FacturaID = id;
        var facturaDetalle = mapper.Map<IEnumerable<FacturaDetalle>>(requestDto.Items);
        response.Data = await unitOfWork.Facturas.EditarFacturaAsync(factura);
        if (response.Data)
        {
            await unitOfWork.Facturas.EliminarFacturaDetalleByIdFacturaAsync(id);
            int itemDetalle = 0;
            foreach (var item in facturaDetalle)
            {
                itemDetalle++;
                item.FacturaID = id;
                item.DetalleID = itemDetalle;
                await unitOfWork.Facturas.InsertarDetalleFacturaAsync(item);
            }
            unitOfWork.Commit();
            response.IsSucces = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        else
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        return response;
    }

    public async Task<BaseResponse<BaseEntityResponse<FacturaResponseDto>>> GetAll(BaseFiltersRequest filters)
    {
        var response = new BaseResponse<BaseEntityResponse<FacturaResponseDto>>();
        var facturas = await unitOfWork.Facturas.ObtenerTodasAsync(filters);
        if (facturas is not null)
        {
            response.IsSucces = true;
                response.Data = mapper.Map<BaseEntityResponse<FacturaResponseDto>>(facturas);
            
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        else
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        return response;
    }

    public async Task<BaseResponse<FacturaCabeceraResponseDto>> GetById(int id)
    {
        var response = new BaseResponse<FacturaCabeceraResponseDto>();
        var factura = await unitOfWork.Facturas.ObtenerPorIdAsync(id);
        if (factura is not null)
        {
            var detalles = await unitOfWork.Facturas.ObtenerDetallePorIdAsync(id);
            var responseFactura= mapper.Map<FacturaCabeceraResponseDto>(factura);
            responseFactura.Items=mapper.Map<List<FacturaDetalleResponseDto>>(detalles);
            response.IsSucces = true;
            response.Data = responseFactura;
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        else
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> RegistroFacturaAsync(FacturaCabeceraRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var validationResult = await validationRules.ValidateAsync(requestDto);
        if (!validationResult.IsValid)
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_VALIDATE;
            response.Errors = validationResult.Errors;
            return response;
        }
        var factura = mapper.Map<FacturaCabeceraAdoNet>(requestDto);
        var facturaDetalle = mapper.Map<IEnumerable<FacturaDetalle>>(requestDto.Items);
        response.Data = await unitOfWork.Facturas.InsertarFacturaAsync(factura);
        if (response.Data)
        {
            int itemDetalle = 0;
            foreach (var item in facturaDetalle)
            {
                itemDetalle++;
                item.FacturaID = factura.FacturaID;
                item.DetalleID = itemDetalle;
                await unitOfWork.Facturas.InsertarDetalleFacturaAsync(item);
            }
            unitOfWork.Commit();
            response.IsSucces = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        else
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        return response;
    }
}
