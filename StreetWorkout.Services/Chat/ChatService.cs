namespace StreetWorkout.Services.Chat
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using StreetWorkout.Data;
    using StreetWorkout.Data.Models;
    using StreetWorkout.ViewModels.Chat;

    public class ChatService : IChatService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IMapper mapper;

        public ChatService(StreetWorkoutDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task Create(string text, string userId)
        {
            await this.data.AddAsync(new ChatMessage
            {
                Text = text,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
            });
            await this.data.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetMessages()
            => await this.data
                .ChatMessages
                .ProjectTo<Message>(this.mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
