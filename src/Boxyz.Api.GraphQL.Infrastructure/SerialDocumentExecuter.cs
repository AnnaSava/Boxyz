using GraphQL;
using GraphQL.Execution;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.GraphQL.Infrastructure
{
    // https://fiyazhasan.me/graphql-with-net-core-part-x-execution-strategies/
    public class SerialDocumentExecuter : DocumentExecuter
    {
        private static IExecutionStrategy ParallelExecutionStrategy = new ParallelExecutionStrategy();
        private static IExecutionStrategy SerialExecutionStrategy = new SerialExecutionStrategy();
        private static IExecutionStrategy SubscriptionExecutionStrategy = new SubscriptionExecutionStrategy();

        protected override IExecutionStrategy SelectExecutionStrategy(ExecutionContext context)
        {
            return context.Operation.OperationType switch
            {
                OperationType.Query => SerialExecutionStrategy,
                OperationType.Mutation => SerialExecutionStrategy,
                OperationType.Subscription => SubscriptionExecutionStrategy,
                _ => throw new InvalidOperationException($"Unexpected OperationType {context.Operation.OperationType}"),
            };
        }
    }
}
