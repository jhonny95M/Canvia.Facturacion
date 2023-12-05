using Canvia.Facturacion.Application.Commons;
using Canvia.Facturacion.Application.Dtos.Request;
using Canvia.Facturacion.Application.Dtos.Response;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;

namespace Canvia.Facturacion.Application.Interfaces;

public interface IFacturaApplication
{
    Task<BaseResponse<BaseEntityResponse<FacturaResponseDto>>> GetAll(BaseFiltersRequest filters);
    Task<BaseResponse<bool>> EditarFacturaAsync(int id,FacturaCabeceraRequestDto requestDto);
    Task<BaseResponse<bool>> RegistroFacturaAsync(FacturaCabeceraRequestDto requestDto);
    Task<BaseResponse<bool>> AnularFacturaAsync(int idFactura);
    Task<BaseResponse<FacturaCabeceraResponseDto>> GetById(int id);
    
}
