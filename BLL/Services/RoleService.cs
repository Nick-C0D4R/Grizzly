using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Repositories;
using DAL.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class RoleService : IDTOService<RoleDTO>
    {
        private RoleRepository _repository;
        private IMapper _mapper;

        public RoleService(RoleRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Role, RoleDTO>()
                .ForMember(x => x.Id, x=> x.MapFrom(p => p.Id))
                .ForMember(x => x.RoleName, x => x.MapFrom(p => p.RoleName));
            });

            _mapper = new Mapper(config);
        }

        public RoleDTO Add(RoleDTO roleDTO)
        {
            Role roletoAdd = new Role
            {
                RoleName = roleDTO.RoleName
            };
            _repository.AddUpdate(roletoAdd);
            roleDTO.Id = roletoAdd.Id;
            return roleDTO;
        }

        public void Delete(RoleDTO roleDTO)
        {
            Role role = _repository.Get(roleDTO.Id);
            _repository.Remove(role);
        }

        public RoleDTO Get(int id) => _mapper.Map<RoleDTO>(_repository.Get(id));

        public IEnumerable<RoleDTO> GetAll() => _mapper.Map<IEnumerable<RoleDTO>>(_repository.GetAll());

        public async Task<IEnumerable<RoleDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(RoleDTO roleDTO)
        {
            Role role = _repository.Get(roleDTO.Id);
            role.RoleName = roleDTO.RoleName;
            _repository.AddUpdate(role);
        }
    }
}
