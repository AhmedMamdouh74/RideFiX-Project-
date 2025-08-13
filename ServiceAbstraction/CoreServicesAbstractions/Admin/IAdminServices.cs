using SharedData.DTOs.Admin.TechnicianCategory;
using SharedData.DTOs.Admin.Users;

namespace ServiceAbstraction.CoreServicesAbstractions.Admin
{
    public interface IAdminServices
    {
        // Users
        Task<List<ReadUsersDTO>> GetAllUsersAsync();
        Task<bool> SoftDeleteUserAsync(int userId);
        Task<bool> RestoreUserAsync(int userId);

        // Categories
        Task<List<TechnicianCategoryDTO>> GetAllCategoriesAsync();
        Task<TechnicianCategoryDTO> CreateCategoryAsync(CreateCategoryDTO dto);
        Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDTO dto);
        Task<bool> SoftDeleteCategoryAsync(int id);
        Task<bool> RestoreCategoryAsync(int id);
    }
}
