using System.Collections;
using System.Linq.Expressions;

namespace SFA.DAS.Campaign.Api.UnitTests.Data.DatabaseMock;

public abstract class TestQueryProvider<T> : IOrderedQueryable<T>, IQueryProvider
{
    private IEnumerable<T>? _enumerable;

    protected TestQueryProvider(Expression expression)
    {
        Expression = expression;
    }
    
    protected TestQueryProvider(IEnumerable<T> enumerable)
    {
        _enumerable = enumerable;
        Expression = _enumerable.AsQueryable().Expression;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        if (expression is MethodCallExpression m)
        {
            var resultType = m.Method.ReturnType; // it should be IQueryable<T>
            var tElement = resultType.GetGenericArguments().First();
            return (IQueryable)CreateInstance(tElement, expression);
        }

        return CreateQuery<T>(expression);
    }

    public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
    {
        return (IQueryable<TEntity>)CreateInstance(typeof(TEntity), expression);
    }

    private object CreateInstance(Type tElement, Expression expression)
    {
        var queryType = GetType().GetGenericTypeDefinition().MakeGenericType(tElement);
        var instance = Activator.CreateInstance(queryType, expression);
        return instance ?? throw new InvalidOperationException($"Failed to create an instance of type '{queryType.FullName}'.");
    }

    public object Execute(Expression expression)
    {
        return CompileExpressionItem<object>(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return CompileExpressionItem<TResult>(expression);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        _enumerable ??= CompileExpressionItem<IEnumerable<T>>(Expression);
        return _enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        _enumerable ??= CompileExpressionItem<IEnumerable<T>>(Expression);
        return _enumerable.GetEnumerator();
    }

    public Type ElementType => typeof(T);

    public Expression Expression { get; }

    public IQueryProvider Provider => this;

    private static TResult CompileExpressionItem<TResult>(Expression expression)
    {
        var visitor = new TestExpressionVisitor();
        var body = visitor.Visit(expression);
        var f = Expression.Lambda<Func<TResult>>(body ?? throw new InvalidOperationException($"{nameof(body)} is null"), []);
        return f.Compile()();
    }
}