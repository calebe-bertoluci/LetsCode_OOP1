using Microsoft.AspNetCore.Mvc.Filters;
using Projeto_WEBAPI_CalebeBertoluci.Interfaces;
//using Projeto_WEBAPI_CalebeBertoluci.Logs;
using Projeto_WEBAPI_CalebeBertoluci.Models;
using Projeto_WEBAPI_CalebeBertoluci.Utils;

namespace Projeto_WEBAPI_CalebeBertoluci.Filters
{
    public class CustomLogsFilter : IResultFilter, IActionFilter
    {
        private readonly List<int> _sucessStatusCodes;
        private readonly IBaseRepository<Movies> _repository;
        private readonly Dictionary<int, Movies> _contextDict;

        public CustomLogsFilter(IBaseRepository<Movies> repository)
        {
            // (_repository, _contextDict, _sucessStatusCodes) = (repository, new Dictionary<int, Games>(), new List<int>() { StatusCodes.Status200OK, StatusCodes.Status201Created });
            _repository = repository;
            _contextDict = new Dictionary<int, Movies>();
            _sucessStatusCodes = new List<int>() { StatusCodes.Status200OK, StatusCodes.Status201Created };
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.Equals(context.ActionDescriptor.RouteValues["controller"], "movies", StringComparison.InvariantCultureIgnoreCase))
            {
                int id = 0;
                if (context.ActionArguments.ContainsKey("id") && int.TryParse(context.ActionArguments["id"].ToString(), out id))
                {
                    if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var movie = _repository.GetByKey(id).Result;
                        if (movie != null)
                        {
                            //var gameClone = CloneService.Clone<Games>(game); // Deep Clone
                            var movieClone = movie.clone(); // Shallow Clone
                            _contextDict.Add(id, movieClone);
                        }
                    }
                }

            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            //if (context.HttpContext.Request.Path.Value.StartsWith("/api/Games", StringComparison.InvariantCulture))
            //{
            //    if (_sucessStatusCodes.Contains(context.HttpContext.Response.StatusCode))
            //    {
            //        var id = int.Parse(context.HttpContext.Request.Path.ToString().Split("/").Last());
            //        if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
            //            || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase))
            //        {
            //            var afterUpdate = _repository.GetByKey(id).Result;
            //            if (afterUpdate != null)
            //            {
            //                Games beforeUpdate;
            //                if (_contextDict.TryGetValue(id, out beforeUpdate))
            //                {
            //                    CustomLogs.SaveLog(afterUpdate.Id, "Game", afterUpdate.Name, context.HttpContext.Request.Method, beforeUpdate, afterUpdate);
            //                    _contextDict.Remove(id);
            //                }
            //            }
            //        }
            //        else if (context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
            //        {
            //            Games beforeUpdate;
            //            if (_contextDict.TryGetValue(id, out beforeUpdate))
            //            {
            //                CustomLogs.SaveLog(beforeUpdate.Id, "Game", beforeUpdate.Name, context.HttpContext.Request.Method);
            //                _contextDict.Remove(id);
            //            }
            //        }
            //    }
            //}
        }




        #region Não Utilizados
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {

        }
        #endregion
    }
}
