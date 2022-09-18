using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Projeto_WEBAPI_CalebeBertoluci.Interfaces
{
    public interface IBaseController<T, K, D>
    {
        Task<IActionResult> Get(int page, int maxResults);

        Task<IActionResult> Get(int key);

        Task<IActionResult> Post(K entity);

        Task<IActionResult> Put(int key, K entity);

        Task<IActionResult> Patch(int key, D entity);

        Task<IActionResult> Delete(int key);
    }
}