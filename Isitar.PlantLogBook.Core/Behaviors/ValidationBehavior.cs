using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse> where TResponse : Response
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);
            var failures = validators
                .Select(validator => validator.Validate(context))
                .SelectMany(res => res.Errors)
                .Where(err => null != err)
                .ToList();
            if (failures.Count != 0)
            {
                var response = Activator.CreateInstance<TResponse>();
                response.Success = false;
                
                
                foreach (var failure in failures)
                {
                    response.AddErrorMessage(failure.PropertyName, failure.ErrorMessage);
                }

                return Task.Run(() => response, cancellationToken);
            }

            return next();
        }
    }
}