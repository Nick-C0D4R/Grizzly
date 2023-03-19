using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Repositories;
using DAL.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProducerService : IService<ProducerDTO>
    {
        private IMapper _mapper;
        private ProducerRepository _repository;

        public ProducerService(ProducerRepository repository)
        {
            _repository = repository;

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Producer, ProducerDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(producer => producer.Id))
                .ForMember(x => x.ProducerName, x => x.MapFrom(producer => producer.ProducerName));
            });

            _mapper = new Mapper(config);
        }

        public IEnumerable<ProducerDTO> GetAll() => _mapper.Map<IEnumerable<ProducerDTO>>(_repository.GetAll());

        public async Task<IEnumerable<ProducerDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public ProducerDTO Get(int id) => _mapper.Map<ProducerDTO>(_repository.Get(id));

        public void Delete(ProducerDTO producerDTO)
        {
            var producer = _repository.Get(producerDTO.Id);
            _repository.Remove(producer);
        }

        public ProducerDTO Add(ProducerDTO producer)
        {
            Producer producerToAdd = new Producer
            {
                ProducerName = producer.ProducerName
            };
            _repository.AddUpdate(producerToAdd);
            producer.Id = producerToAdd.Id;
            return producer;
        }

        public void Update(ProducerDTO producer)
        {
            Producer producerToUpdate = _repository.Get(producer.Id);
            producerToUpdate.ProducerName = producerToUpdate.ProducerName;
            _repository.AddUpdate(producerToUpdate);
        }
    }
}
