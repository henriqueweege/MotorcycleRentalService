using MassTransit;
using MotorcycleRentalService.Domain.Events;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;
using Serilog;

namespace MotorcycleRentalService.Application.Consumers
{
    public class MotorcycleCreatedConsumer : IConsumer<MotorcycleCreated>
    {
        private readonly IDefaultRepository<MotorcycleCreated> _repository;
        public MotorcycleCreatedConsumer(IDefaultRepository<MotorcycleCreated> repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<MotorcycleCreated> context)
        {
            try
            {
                if (context.Message.MotorcycleYear == 2024)
                {
                    Log.Information($"Motorcycle {context.Message.Id} is from 2024!");
                }

                await _repository.Add(context.Message);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
            }
        }
    }
}
