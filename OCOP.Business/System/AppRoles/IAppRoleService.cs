using OCOP.ViewModel.Common;
using OCOP.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.System.AppRoles
{
    public interface IAppRoleService
    {
        Task<ResponseResult<List<RoleViewModel>>> GetAllRole();

        Task<ResponseResult<bool>> CreateRole(RoleCreateRequest request);

        Task<ResponseResult<bool>> DeleteRole(Guid id);

        Task<ResponseResult<bool>> RoleAssign(RoleAssignRequest request);

        Task<ResponseResult<IList<string>>> GetListRoleByUser(Guid appUserId);
    }
}
