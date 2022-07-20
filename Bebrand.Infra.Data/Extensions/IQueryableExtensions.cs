using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, OwnerParameters queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (queryObj == null) return query;
            var sort = queryObj.Sort != null ? queryObj.Sort.ToLower() : queryObj.Sort;
            var result = query;

            if (string.IsNullOrEmpty(sort) || !columnsMap.ContainsKey(sort))
                return result;

            if (columnsMap[sort] == null || string.IsNullOrEmpty(sort))
                return result;

            if (queryObj.Order == "asc") return result.OrderBy(columnsMap[sort]);

            else if (queryObj.Order == "desc") return result.OrderByDescending(columnsMap[sort]);

            else return result.OrderBy(columnsMap[sort]);

        }
    }
}
