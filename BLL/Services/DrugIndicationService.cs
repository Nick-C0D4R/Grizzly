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
    public class DrugIndicationService : IDTOService<DrugIndicationDTO>
    {
        private DrugIndicationRepository _repository;
        private IMapper _mapper;

        public DrugIndicationService(DrugIndicationRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DrugIndication, DrugIndicationDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.Indication, x => x.MapFrom(p => p.Indication));
            });
            _mapper = new Mapper(config);
        }

        public DrugIndicationDTO Add(DrugIndicationDTO indication)
        {
            DrugIndication toAdd = new DrugIndication
            {
                Indication = indication.Indication
            };
            _repository.AddUpdate(toAdd);
            indication.Id = toAdd.Id;
            return indication;
        }

        public void Delete(DrugIndicationDTO indication)
        {
            DrugIndication toDelete = _repository.Get(indication.Id);
            _repository.Remove(toDelete);
        }

        public DrugIndicationDTO Get(int id) => _mapper.Map<DrugIndicationDTO>(_repository.Get(id));

        public IEnumerable<DrugIndicationDTO> GetAll() => _mapper.Map<IEnumerable<DrugIndicationDTO>>(_repository.GetAll());

        public async Task<IEnumerable<DrugIndicationDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(DrugIndicationDTO indication)
        {
            //TODO Update I
            DrugIndication toUpdate = _repository.Get(indication.Id);
            toUpdate.Indication = indication.Indication;
            _repository.AddUpdate(toUpdate);
        }
    }
}
