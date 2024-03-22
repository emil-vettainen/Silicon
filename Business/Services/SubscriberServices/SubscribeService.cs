using AutoMapper;
using Business.Dtos.Subscribe;
using Business.Dtos.Subscriber;
using Infrastructure.Entities.SubscribeEntities;
using Infrastructure.Repositories.SqlRepositories;
using Shared.Factories;
using Shared.Responses;

namespace Business.Services.SubscriberServices;

public class SubscribeService
{

    private readonly SubscribeRepository _subscribeRepository;
    private readonly IMapper _mapper;

    public SubscribeService(SubscribeRepository subscribeRepository, IMapper mapper)
    {
        _subscribeRepository = subscribeRepository;
        _mapper = mapper;
    }

    public async Task<ResponseResult> CreateAsync(CreateSubsriberDto dto)
    {
        try
        {
            var existingSubscriber = await _subscribeRepository.ExistsAsync(x => x.Email == dto.Email);
            if (!existingSubscriber)
            {
                var newSubscriber = await _subscribeRepository.CreateAsync(_mapper.Map<SubscribeEntity>(dto));
                return newSubscriber != null ? ResponseFactory.Ok() : ResponseFactory.Error();
            }

            return ResponseFactory.Exists();
        }
        catch (Exception)
        {
            //logger

        }
        return ResponseFactory.Error();
    }


    public async Task<IEnumerable<GetSubscriberDto>> GetAsync()
    {
        try
        {
            var subscribers = await _subscribeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetSubscriberDto>>(subscribers);  
            
        }
        catch (Exception)
        {
            //logger
        }
        return [];
    }


    public async Task<GetSubscriberDto> GetAsync(int id)
    {
        try
        {
            var subscriber = await _subscribeRepository.GetOneAsync(x => x.Id == id);
            if (subscriber != null)
            {
                return _mapper.Map<GetSubscriberDto>(subscriber);   
            }
          
        }
        catch (Exception)
        {
            //logger
        }
        return null!;
    }

    public async Task<GetSubscriberDto> UpdateAsync(int id, CreateSubsriberDto dto)
    {
        try
        {
            var result = await _subscribeRepository.UpdateAsync(x => x.Id == id, _mapper.Map<SubscribeEntity>(dto));
            if (result != null)
            {
                return _mapper.Map<GetSubscriberDto>(result);
            }
        }
        catch (Exception)
        {
            //logger

        }
        return null!;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var result = await _subscribeRepository.DeleteAsync(x => x.Id == id);
            return result;
        }
        catch (Exception)
        {
            //logger
        }
        return false;
    }
}