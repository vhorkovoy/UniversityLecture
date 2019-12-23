using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using UniversityLecture.Repo.Interfaces;

namespace UniversityLecture.Bll.Specifications
{
    public abstract class BaseSpec<T> : ISpecification<T>
    {
        public BaseSpec(Expression<Func<T, bool>> criteria) : this()
        {
            Criteria = criteria;
           
        }
        public BaseSpec()
        {
            Includes = new List<Expression<Func<T, object>>>();
            IncludeStrings = new List<string>();
        }
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public List<string> IncludeStrings { get; }

       
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        // string-based includes allow for including children of children, e.g. Basket.Items.Product
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
