namespace BLL.Services
{
    using AutoMapper;
    using BLL.DTO;
    using BLL.Interfaces;
    using DAL.Repositories;
    using DAL.Tables;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DrugTypeService : IDTOService<DrugTypeDTO>
    {
        private DrugTypeRepository _repository;
        private IMapper _mapper;

        public DrugTypeService(DrugTypeRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DrugType, DrugTypeDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.TypeName, x => x.MapFrom(p => p.TypeName));
            });
            _mapper = new Mapper(config);
        }

        public DrugTypeDTO Add(DrugTypeDTO type)
        {
            DrugType toAdd = new DrugType
            {
                TypeName = type.TypeName
            };
            _repository.AddUpdate(toAdd);
            type.Id = toAdd.Id;
            return type;
        }

        public void Delete(DrugTypeDTO type)
        {
            DrugType toDelete = _repository.Get(type.Id);
            _repository.Remove(toDelete);
        }

        public DrugTypeDTO Get(int id) => _mapper.Map<DrugTypeDTO>(_repository.Get(id));

        public IEnumerable<DrugTypeDTO> GetAll() => _mapper.Map<IEnumerable<DrugTypeDTO>>(_repository.GetAll());

        public async Task<IEnumerable<DrugTypeDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(DrugTypeDTO type)
        {
            DrugType toUpdate = _repository.Get(type.Id);
            toUpdate.TypeName = type.TypeName;
            _repository.AddUpdate(toUpdate);
        }
    }
}
