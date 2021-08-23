namespace StreetWorkout.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StreetWorkout.Data.Models;

    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> chatMessage)
        {
            chatMessage
                .HasOne(x => x.User)
                .WithMany(x => x.ChatMessages)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
