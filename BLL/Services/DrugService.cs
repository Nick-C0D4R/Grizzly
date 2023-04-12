using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Repositories;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DrugService : IDTOService<DrugDTO>
    {
        private DrugRepository _repository;
        private IMapper _mapper;
        private IMapper _indicationMapper;
        private IMapper _contraIndicationMapper;
        private IMapper _applicationTypeMapper;

        public DrugService(DrugRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Drug, DrugDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.Quantity, x => x.MapFrom(p => p.Quantity))
                .ForMember(x => x.ActiveIngredientId, x => x.MapFrom(p => p.ActiveIngredientId))
                .ForMember(x => x.ProducerId, x => x.MapFrom(p => p.ProducerId))
                .ForMember(x => x.DrugTypeId, x => x.MapFrom(p => p.DrugTypeId))
                .ForMember(x => x.DrugName, x => x.MapFrom(p => p.DrugName))
                .ForMember(x => x.Description, x => x.MapFrom(p => p.Description))
                .ForMember(x => x.Price, x => x.MapFrom(p => p.Price))
                .ForMember(x => x.Indications, x => x.MapFrom(p => p.Indications))
                .ForMember(x => x.ContraIndications, x => x.MapFrom(p => p.ContraIndications))
                .ForMember(x => x.ApplicationTypes, x => x.MapFrom(p => p.ApplicationTypes));
            });
            _mapper = new Mapper(config);

            //Indication mapper
            var indicationConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DrugIndicationDTO, DrugIndication>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.Indication, x => x.MapFrom(p => p.Indication));
            });
            _indicationMapper = new Mapper(indicationConfig);

            //Contraindication mapper
            var contraIndicationConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DrugContraIndicationDTO, DrugContraIndication>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.ContraIndication, x => x.MapFrom(p => p.ContraIndication));
            });
            _contraIndicationMapper = new Mapper(contraIndicationConfig);

            //Application type mapper
            var applicationTypeConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DrugApplicationTypeDTO, DrugApplicationType>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.ApplicationType, x => x.MapFrom(p => p.ApplicationType));
            });
            _applicationTypeMapper = new Mapper(applicationTypeConfig);
        }

        public DrugDTO Add(DrugDTO drugDTO)
        {
            Drug toAdd = new Drug
            {
                ActiveIngredientId = drugDTO.ActiveIngredientId,
                Indications = _indicationMapper.Map<ICollection<DrugIndication>>(drugDTO.Indications),
                ContraIndications = _contraIndicationMapper.Map<ICollection<DrugContraIndication>>(drugDTO.ContraIndications),
                ApplicationTypes = _applicationTypeMapper.Map<ICollection<DrugApplicationType>>(drugDTO.ApplicationTypes),
                Description= drugDTO.Description,
                DrugName= drugDTO.DrugName,
                DrugTypeId= drugDTO.DrugTypeId,
                Price= drugDTO.Price,
                ProducerId= drugDTO.ProducerId,
                Quantity= drugDTO.Quantity
            };
            _repository.AddUpdate(toAdd);
            drugDTO.Id = toAdd.Id;
            return drugDTO;
        }

        public void Delete(DrugDTO drugDTO)
        {
            Drug toDelete = _repository.Get(drugDTO.Id);
            _repository.Remove(toDelete);
        }

        public DrugDTO Get(int id) => _mapper.Map<DrugDTO>(_repository.Get(id));

        public IEnumerable<DrugDTO> GetAll() => _mapper.Map<IEnumerable<DrugDTO>>(_repository.GetAll());

        public async Task<IEnumerable<DrugDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(DrugDTO drugDTO)
        {
            Drug toUpdate = _repository.Get(drugDTO.Id);
            toUpdate.ActiveIngredientId = drugDTO.ActiveIngredientId;
            toUpdate.Indications = _indicationMapper.Map<ICollection<DrugIndication>>(drugDTO.Indications);
            toUpdate.ContraIndications = _contraIndicationMapper.Map<ICollection<DrugContraIndication>>(drugDTO.ContraIndications);
            toUpdate.ApplicationTypes = _applicationTypeMapper.Map<ICollection<DrugApplicationType>>(drugDTO.ApplicationTypes);
            toUpdate.Description = drugDTO.Description;
            toUpdate.DrugName = drugDTO.DrugName;
            toUpdate.DrugTypeId = drugDTO.DrugTypeId;
            toUpdate.Price = drugDTO.Price;
            toUpdate.ProducerId = drugDTO.ProducerId;
            toUpdate.Quantity = drugDTO.Quantity;
            _repository.AddUpdate(toUpdate);
        }
    }
}
