using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Repositories;
using DAL.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IDTOService<OrderDTO>
    {
        private OrderRepository _repository;
        private IMapper _mapper;

        public OrderService(OrderRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.OfficeId, x => x.MapFrom(p => p.OfficeId))
                .ForMember(x => x.OrderDate, x => x.MapFrom(p => p.OrderDate))
                .ForMember(x => x.CartId, x => x.MapFrom(p => p.CartId));
            });
            _mapper = new Mapper(config);
        }

        public OrderDTO Add(OrderDTO orderDTO)
        {
            Order toAdd = new Order
            {
                CartId= orderDTO.CartId,
                OfficeId= orderDTO.OfficeId,
                OrderDate = orderDTO.OrderDate
            };
            _repository.AddUpdate(toAdd);
            orderDTO.Id = toAdd.Id;
            return orderDTO;
        }

        public void Delete(OrderDTO orderDTO)
        {
            Order toDelete = _repository.Get(orderDTO.Id);
            _repository.Remove(toDelete);
        }

        public OrderDTO Get(int id) => _mapper.Map<OrderDTO>(_repository.Get(id));

        public IEnumerable<OrderDTO> GetAll() => _mapper.Map<IEnumerable<OrderDTO>>(_repository.GetAll());

        public async Task<IEnumerable<OrderDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(OrderDTO entity)
        {
            Order toUpdate = _repository.Get(entity.Id);
            toUpdate.OfficeId = entity.OfficeId;
            toUpdate.OrderDate = entity.OrderDate;
            toUpdate.CartId = entity.CartId;
            _repository.AddUpdate(toUpdate);
        }
    }
}
