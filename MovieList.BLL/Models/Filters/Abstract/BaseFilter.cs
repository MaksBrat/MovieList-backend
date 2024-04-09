using System.Linq.Expressions;
using MovieList.Services.Extentions;

namespace MovieList.Common.EntitiesFilters.Abstract
{
    public abstract class BaseFilter<T> where T : class
    {
        public string? SearchQuery { get; set; }
        public string? OrderBy { get; set; }
        public string? AscOrDesc { get; set; }
        public int Take { get; set; } = 0;
        public int PageIndex { get; set; } = 0;

        public Expression<Func<T, bool>>? Predicate { get; set; } = null;
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderByQuery { get; set; } = null;

        public abstract void CreateFilter();

        protected void ApplySearchQueryFilter(string propertyName)
        {   
            if (SearchQuery == null)
            {
                return;
            }

            var parameterExp = Expression.Parameter(typeof(T), "a");
            var propertyExp = Expression.Property(parameterExp, propertyName);

            var containsMethodExp = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsExp = Expression.Call(propertyExp, containsMethodExp, Expression.Constant(SearchQuery));

            Predicate = Expression.Lambda<Func<T, bool>>(containsExp, parameterExp);
        }

        protected void ApplyEnumFilter<E>(string enumValue, string propertyName) where E : struct
        {
            if (enumValue == null)
            {
                return;
            }

            var parsedEnumValue = Enum.Parse<E>(enumValue);
            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var propertyExpression = Expression.Property(parameterExpression, propertyName);
            var equalExpression = Expression.Equal(propertyExpression, Expression.Constant(parsedEnumValue));

            var lambda = Expression.Lambda<Func<T, bool>>(equalExpression, parameterExpression);
            Predicate = Predicate == null ? lambda : Predicate.And(lambda);
        }

        protected void ApplyOrderByFilter(string propertyName, string ascOrDesc)
        {
            if (propertyName == null)
            {
                return;
            }

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression property = Expression.Property(param, propertyName);
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), param);

            if (ascOrDesc.ToUpper() == "ASC")
            {
                OrderByQuery = x => x.OrderBy(lambda);
            }
            else if (ascOrDesc.ToUpper() == "DESC")
            {
                OrderByQuery = x => x.OrderByDescending(lambda);
            }
        }
    }
}
