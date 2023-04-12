using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Repositories;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CartService : IDTOService<CartDTO>
    {
        private CartRepository _repository;
        private IMapper _mapper;

        public CartService(CartRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cart, CartDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.UserLogin, x => x.MapFrom(p => p.UserLogin))
                .ForMember(x => x.OrderDate, x => x.MapFrom(p => p.OrderDate));
            });
            _mapper = new Mapper(config);
        }

        public CartDTO Add(CartDTO cart)
        {
            Cart toAdd = new Cart
            {
                UserLogin = cart.UserLogin,
                OrderDate = cart.OrderDate
            };
            _repository.AddUpdate(toAdd);
            cart.Id = toAdd.Id;
            return cart;
        }

        public void Delete(CartDTO cart)
        {
            Cart toDelete = _repository.Get(cart.Id);
            _repository.Remove(toDelete);
        }

        public CartDTO Get(int id) => _mapper.Map<CartDTO>(_repository.Get(id));

        public IEnumerable<CartDTO> GetAll() => _mapper.Map<IEnumerable<CartDTO>>(_repository.GetAll());

        public async Task<IEnumerable<CartDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(CartDTO cart)
        {
            Cart toUpdate = _repository.Get(cart.Id);
            toUpdate.OrderDate = cart.OrderDate;
            toUpdate.UserLogin = cart.UserLogin;
            _repository.AddUpdate(toUpdate);
        }
    }
}
