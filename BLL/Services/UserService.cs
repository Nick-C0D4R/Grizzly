using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Repositories;
using DAL.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IDTOService<UserDTO>
    {
        private UserRepository _repository;
        private IMapper _mapper;

        public UserService(UserRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.UserName, x => x.MapFrom(p => p.UserName))
                .ForMember(x => x.UserSurname, x => x.MapFrom(p => p.UserSurname))
                .ForMember(x => x.RoleId, x => x.MapFrom(p => p.RoleId))
                .ForMember(x => x.JoinDate, x => x.MapFrom(p => p.JoinDate))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(p => p.PhoneNumber))
                .ForMember(x => x.Login, x => x.MapFrom(p => p.Login))
                .ForMember(x => x.Password, x => x.MapFrom( p => p.Password))
                .ForMember(x => x.Email, x => x.MapFrom(p => p.Email));
            });
            _mapper = new Mapper(config);
        }

        public UserDTO Add(UserDTO userDTO)
        {
            User toAdd = new User
            {
                UserName = userDTO.UserName,
                UserSurname= userDTO.UserSurname,
                RoleId = userDTO.RoleId,
                JoinDate= userDTO.JoinDate,
                PhoneNumber= userDTO.PhoneNumber,
                Login= userDTO.Login,
                Email= userDTO.Email,
                Password = userDTO.Password
            };

            _repository.AddUpdate(toAdd);
            userDTO.Id = toAdd.Id;
            return userDTO;
        }

        public void Delete(UserDTO userDTO)
        {
            User toDelete = _repository.Get(userDTO.Id);
            _repository.Remove(toDelete);
        }

        public UserDTO Get(int id) => _mapper.Map<UserDTO>(_repository.Get(id));

        public IEnumerable<UserDTO> GetAll() => _mapper.Map<IEnumerable<UserDTO>>(_repository.GetAll());

        public async Task<IEnumerable<UserDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(UserDTO userDTO)
        {
            User toUpdate = _repository.Get(userDTO.Id);
            toUpdate.UserName = userDTO.UserName;
            toUpdate.UserSurname = userDTO.UserSurname;
            toUpdate.RoleId = userDTO.RoleId;
            toUpdate.JoinDate = userDTO.JoinDate;
            toUpdate.PhoneNumber = userDTO.PhoneNumber;
            toUpdate.Email = userDTO.Email;
            toUpdate.Password = userDTO.Password;
            toUpdate.Login= userDTO.Login;
            _repository.AddUpdate(toUpdate);
        }
    }
}
