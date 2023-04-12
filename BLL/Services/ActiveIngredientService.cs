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
    public class ActiveIngredientService : IDTOService<ActiveIngredientDTO>
    {
        private ActiveIngredientRepository _repository;
        private IMapper _mapper;

        public ActiveIngredientService(ActiveIngredientRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActiveIngredient, ActiveIngredientDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.IngredientName, x => x.MapFrom(p => p.IngredientName))
                .ForMember(x => x.Milligrams, x => x.MapFrom(p => p.Milligrams));
            });
            _mapper = new Mapper(config);
        }

        public ActiveIngredientDTO Add(ActiveIngredientDTO ingredient)
        {
            ActiveIngredient toAdd = new ActiveIngredient
            {
                IngredientName = ingredient.IngredientName,
                Milligrams = ingredient.Milligrams
            };
            _repository.AddUpdate(toAdd);
            ingredient.Id = toAdd.Id;
            return ingredient;
        }

        public void Delete(ActiveIngredientDTO ingredient)
        {
            ActiveIngredient toDelete = _repository.Get(ingredient.Id);
            _repository.Remove(toDelete);
        }

        public ActiveIngredientDTO Get(int id) => _mapper.Map<ActiveIngredientDTO>(_repository.Get(id));

        public IEnumerable<ActiveIngredientDTO> GetAll() => _mapper.Map<IEnumerable<ActiveIngredientDTO>>(_repository.GetAll());

        public async Task<IEnumerable<ActiveIngredientDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public void Update(ActiveIngredientDTO ingredient)
        {
            ActiveIngredient toUpdate = _repository.Get(ingredient.Id);
            toUpdate.Milligrams = ingredient.Milligrams;
            toUpdate.IngredientName = ingredient.IngredientName;
            _repository.AddUpdate(toUpdate);
        }
    }
}
