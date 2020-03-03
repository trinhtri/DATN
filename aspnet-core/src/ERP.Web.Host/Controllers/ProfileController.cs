using Abp.AspNetCore.Mvc.Authorization;
using ERP.Storage;

namespace ERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(ITempFileCacheManager tempFileCacheManager, IAppFolders appFolders) :
            base(tempFileCacheManager, appFolders)
        {
        }
    }
}