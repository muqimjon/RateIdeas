using Newtonsoft.Json;
using RateIdeas.Domain.Common;
using RateIdeas.Domain.Configurations;

namespace RateIdeas.Application.Commons.Extensions
{
    public static class CollectionExtension
    {
        public static IQueryable<T> ToPaginate<T>(this IQueryable<T> values, PaginationParams @params)
            => values.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);

        public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, int pageSize, int pageIndex)
            where TEntity : Auditable
        {
            PaginationParams @params = new()
            {
                PageSize = pageSize > 0 ? pageSize : 1,
                PageIndex = pageIndex > 0 ? pageIndex : 1
            };

            var metaData = new PaginationMetaData(entities.Count(), @params);
            var json = JsonConvert.SerializeObject(metaData);

            if (HttpContextHelper.ResponseHeaders != null)
            {
                if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                    HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

                //HttpContextHelper.ResponseHeaders["X-Pagination"] = json;
                HttpContextHelper.ResponseHeaders.Append("X-Pagination", json);
            }

            return entities.ToPaginate(@params);
        }
    }
}
