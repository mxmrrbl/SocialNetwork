using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class GenericService<ViewModel, SaveViewModel, Entity> : IGenericService<ViewModel, SaveViewModel, Entity>
        where ViewModel : class
        where SaveViewModel : class
        where Entity : class
    {
        private readonly IGenericRepository<Entity> _genericRepository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        //Add
        public virtual async Task<SaveViewModel> Add(SaveViewModel svm)
        {
            Entity entity = _mapper.Map<Entity>(svm);


            entity = await _genericRepository.AddAsync(entity);

            SaveViewModel Svm = _mapper.Map<SaveViewModel>(entity);


            return Svm;
        }

        //Read
        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
            var entityList = await _genericRepository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entityList);
        }

        //Edit
        public virtual async Task Update(SaveViewModel vm, int id)
        {
            Entity entity =  _mapper.Map<Entity>(vm);

            await _genericRepository.UpdateAsync(entity, id);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            Entity entity = await _genericRepository.GetByIdAsync(id);

            SaveViewModel svm = _mapper.Map<SaveViewModel>(entity);

            return svm;
        }

        //Delete
        public virtual async Task Delete(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            await _genericRepository.DeleteAsync(entity);
        }
    }
}
