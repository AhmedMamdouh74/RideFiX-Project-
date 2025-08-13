using SharedData.DTOs.Admin.TechnicianCategory;
using SharedData.DTOs.Admin.Users;

namespace ServiceAbstraction.CoreServicesAbstractions.Admin
{
    public interface IAdminService
    {
        // Users
        Task<List<ReadUsersDTO>> GetAllUsersAsync();
        Task<List<ReadUsersDTO>> GetAllTechniciansAsync();
        Task<List<ReadUsersDTO>> GetAllCarOwnersAsync();
        Task<bool> SoftDeleteUserAsync(string userId);
        Task<bool> BanUserAsync(string userId);
        Task<bool> RestoreUserAsync(string userId);

        // Categories
        Task<List<ReadTCategoryDTO>> GetAllCategoriesAsync();
        Task<ReadTCategoryDTO> CreateCategoryAsync(CreateTCategoryDTO dto);
        Task<bool> UpdateCategoryAsync(int id, UpdateTCategoryDTO dto);
        Task<bool> SoftDeleteCategoryAsync(int id);
        Task<bool> RestoreCategoryAsync(int id);
    }
}
