using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel, SaveViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<SaveViewModel> Add(SaveViewModel svm);
        Task<List<ViewModel>> GetAllViewModel();
        Task Update(SaveViewModel vm, int id);
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task Delete(int id);


    }
}
