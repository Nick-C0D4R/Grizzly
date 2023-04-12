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
    public class ApplicationTypeService : IDTOService<DrugApplicationTypeDTO>
    {
        private ApplicationTypeRepository _repository;
        private IMapper _mapper;

        public ApplicationTypeService(ApplicationTypeRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DrugApplicationType, DrugApplicationTypeDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.ApplicationType, x => x.MapFrom(p => p.ApplicationType));
            });
            _mapper = new Mapper(config);
        }

        public DrugApplicationTypeDTO Add(DrugApplicationTypeDTO type)
        {
            DrugApplicationType toAdd = new DrugApplicationType
            {
                ApplicationType = type.ApplicationType
            };
            _repository.AddUpdate(toAdd);
            type.Id = toAdd.Id;
            return type;
        }

        public void Delete(DrugApplicationTypeDTO type)
        {
            DrugApplicationType toDelete = _repository.Get(type.Id);
            _repository.Remove(toDelete);
        }

        public DrugApplicationTypeDTO Get(int id) => _mapper.Map<DrugApplicationTypeDTO>(_repository.Get(id));

        public IEnumerable<DrugApplicationTypeDTO> GetAll() => _mapper.Map<IEnumerable<DrugApplicationTypeDTO>>(_repository.GetAll());

        public async Task<IEnumerable<DrugApplicationTypeDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(DrugApplicationTypeDTO type)
        {
            DrugApplicationType toUpdate = _repository.Get(type.Id);
            toUpdate.ApplicationType = type.ApplicationType;
            _repository.AddUpdate(toUpdate);
        }
    }
}
