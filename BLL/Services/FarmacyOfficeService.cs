namespace BLL.Services
{
    using AutoMapper;
    using BLL.DTO;
    using BLL.Interfaces;
    using DAL.Repositories;
    using DAL.Tables;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FarmacyOfficeService : IDTOService<PharmacyOfficeDTO>
    {
        private FarmacyOfficeRepository _repository;
        private IMapper _mapper;

        public FarmacyOfficeService(FarmacyOfficeRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PharmacyOffice, PharmacyOfficeDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.FarmacyTitle, x => x.MapFrom(p => p.FarmacyTitle))
                .ForMember(x => x.FarmacyAddress, x => x.MapFrom(p => p.FarmacyAddress));
            });
            _mapper = new Mapper(config);
        }

        public PharmacyOfficeDTO Add(PharmacyOfficeDTO office)
        {
            PharmacyOffice toAdd = new PharmacyOffice
            {
                Id = office.Id,
                FarmacyAddress = office.FarmacyAddress,
                FarmacyTitle = office.FarmacyTitle
            };
            _repository.AddUpdate(toAdd);
            office.Id = toAdd.Id;
            return office;
        }

        public void Delete(PharmacyOfficeDTO office)
        {
            PharmacyOffice toDelete = _repository.Get(office.Id);
            _repository.Remove(toDelete);
        }

        public PharmacyOfficeDTO Get(int id) => _mapper.Map<PharmacyOfficeDTO>(_repository.Get(id));

        public IEnumerable<PharmacyOfficeDTO> GetAll() => _mapper.Map<IEnumerable<PharmacyOfficeDTO>>(_repository.GetAll());

        public async Task<IEnumerable<PharmacyOfficeDTO>> GetAllAsync()=> await Task.Run(() => GetAll());

        public void Update(PharmacyOfficeDTO office)
        {
            PharmacyOffice toUpdate = _repository.Get(office.Id);
            toUpdate.FarmacyTitle = office.FarmacyTitle;
            toUpdate.FarmacyAddress = office.FarmacyAddress;
            _repository.AddUpdate(toUpdate);
        }
    }
}
