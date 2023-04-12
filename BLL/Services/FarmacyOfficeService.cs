namespace BLL.Services
{
    using AutoMapper;
    using BLL.DTO;
    using BLL.Interfaces;
    using DAL.Repositories;
    using DAL.Tables;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FarmacyOfficeService : IDTOService<FarmacyOfficeDTO>
    {
        private FarmacyOfficeRepository _repository;
        private IMapper _mapper;

        public FarmacyOfficeService(FarmacyOfficeRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FarmacyOffice, FarmacyOfficeDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.FarmacyTitle, x => x.MapFrom(p => p.FarmacyTitle))
                .ForMember(x => x.FarmacyAddress, x => x.MapFrom(p => p.FarmacyAddress));
            });
            _mapper = new Mapper(config);
        }

        public FarmacyOfficeDTO Add(FarmacyOfficeDTO office)
        {
            FarmacyOffice toAdd = new FarmacyOffice
            {
                Id = office.Id,
                FarmacyAddress = office.FarmacyAddress,
                FarmacyTitle = office.FarmacyTitle
            };
            _repository.AddUpdate(toAdd);
            office.Id = toAdd.Id;
            return office;
        }

        public void Delete(FarmacyOfficeDTO office)
        {
            FarmacyOffice toDelete = _repository.Get(office.Id);
            _repository.Remove(toDelete);
        }

        public FarmacyOfficeDTO Get(int id) => _mapper.Map<FarmacyOfficeDTO>(_repository.Get(id));

        public IEnumerable<FarmacyOfficeDTO> GetAll() => _mapper.Map<IEnumerable<FarmacyOfficeDTO>>(_repository.GetAll());

        public async Task<IEnumerable<FarmacyOfficeDTO>> GetAllAsync()=> await Task.Run(() => GetAll());

        public void Update(FarmacyOfficeDTO office)
        {
            FarmacyOffice toUpdate = _repository.Get(office.Id);
            toUpdate.FarmacyTitle = office.FarmacyTitle;
            toUpdate.FarmacyAddress = office.FarmacyAddress;
            _repository.AddUpdate(toUpdate);
        }
    }
}
