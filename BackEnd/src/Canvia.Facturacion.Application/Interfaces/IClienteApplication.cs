using Canvia.Facturacion.Application.Commons;
using Canvia.Facturacion.Application.Dtos.Response;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;

namespace Canvia.Facturacion.Application.Interfaces;

public interface IClienteApplication
{
    Task<BaseResponse<BaseEntityResponse<ClienteResponseDto>>> GetAll(BaseFiltersRequest filters);
    Task<BaseResponse<IEnumerable<ClienteSelectResponseDto>>> SelectClientes();
    //Task<BaseResponse<CategoryResponseDto>> CategoryById(int id);
    //Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto);
    //Task<BaseResponse<bool>> EditCategory(int id, CategoryRequestDto requestDto);
    //Task<BaseResponse<bool>> RemoveCategory(int id);
}
