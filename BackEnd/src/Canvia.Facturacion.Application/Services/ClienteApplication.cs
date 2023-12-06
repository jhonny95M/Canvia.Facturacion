using AutoMapper;
using Canvia.Facturacion.Application.Commons;
using Canvia.Facturacion.Application.Dtos.Response;
using Canvia.Facturacion.Application.Interfaces;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;
using Canvia.Facturacion.Infraestructure.Persistences.Interfaces;
using Canvia.Facturacion.Utilities.Static;

namespace Canvia.Facturacion.Application.Services
{
    public class ClienteApplication : IClienteApplication
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ClienteApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<BaseEntityResponse<ClienteResponseDto>>> GetAll(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ClienteResponseDto>>();
            var clientes = await unitOfWork.Clientes.ObtenerTodosAsync(filters);
            if (clientes is not null)
            {
                response.IsSucces = true;
                response.Data = mapper.Map<BaseEntityResponse<ClienteResponseDto>>(clientes);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<ClienteSelectResponseDto>>> SelectClientes()
        {
            var response = new BaseResponse<IEnumerable<ClienteSelectResponseDto>>();
            var clientes = await unitOfWork.Clientes.ObtenerTodosAsync();
            if (clientes is not null)
            {
                response.IsSucces = true;
                response.Data = mapper.Map<IEnumerable<ClienteSelectResponseDto>>(clientes.Items);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
    }
}
