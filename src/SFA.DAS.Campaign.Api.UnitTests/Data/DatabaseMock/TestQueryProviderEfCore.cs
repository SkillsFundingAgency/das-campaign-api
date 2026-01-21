using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace SFA.DAS.Campaign.Api.UnitTests.Data.DatabaseMock;

public class TestAsyncEnumerableEfCore<T> : TestQueryProvider<T>, IAsyncEnumerable<T>, IAsyncQueryProvider
{
    public TestAsyncEnumerableEfCore(Expression expression) : base(expression)
    {
    }
    
    public TestAsyncEnumerableEfCore(IEnumerable<T> enumerable) : base(enumerable)
    {
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        var expectedResultType = typeof(TResult).GetGenericArguments()[0];
        object? executionResult = typeof(IQueryProvider)
            .GetMethods()
            .First(method => method.Name == nameof(IQueryProvider.Execute) && method.IsGenericMethod)
            .MakeGenericMethod(expectedResultType)
            .Invoke(this, [expression]);

        var fromResultMethod = (typeof(Task).GetMethod(nameof(Task.FromResult))?.MakeGenericMethod(expectedResultType)) ?? throw new InvalidOperationException("Could not find Task.FromResult method.");
        object? taskResult = fromResultMethod.Invoke(null, [executionResult]) ?? throw new InvalidOperationException("Task.FromResult invocation returned null.");
        return (TResult)taskResult;
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }
}