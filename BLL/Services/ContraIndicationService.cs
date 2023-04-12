using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Repositories;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ContraIndicationService : IDTOService<DrugContraIndicationDTO>
    {
        private ContraIndicationRepository _repository;
        private IMapper _mapper;

        public ContraIndicationService(ContraIndicationRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DrugContraIndication, DrugContraIndicationDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.ContraIndication, x => x.MapFrom(p => p.ContraIndication));
            });
            _mapper = new Mapper(config);
        }

        public DrugContraIndicationDTO Add(DrugContraIndicationDTO contraIndication)
        {
            DrugContraIndication toAdd = new DrugContraIndication
            {
                ContraIndication = contraIndication.ContraIndication
            };
            _repository.AddUpdate(toAdd);
            contraIndication.Id = toAdd.Id;
            return contraIndication;
        }

        public void Delete(DrugContraIndicationDTO contraIndication)
        {
            DrugContraIndication toDelete = _repository.Get(contraIndication.Id);
            _repository.Remove(toDelete);
        }

        public DrugContraIndicationDTO Get(int id) => _mapper.Map<DrugContraIndicationDTO>(_repository.Get(id));

        public IEnumerable<DrugContraIndicationDTO> GetAll() => _mapper.Map<IEnumerable<DrugContraIndicationDTO>>(_repository.GetAll());

        public async Task<IEnumerable<DrugContraIndicationDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(DrugContraIndicationDTO contraIndication)
        {
            DrugContraIndication toUpdate = _repository.Get(contraIndication.Id);
            toUpdate.ContraIndication = contraIndication.ContraIndication;
            _repository.AddUpdate(toUpdate);
        }
    }
}
