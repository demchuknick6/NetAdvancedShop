namespace Catalog.Infrastructure.Configurations;

internal class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(ci => ci.Category)
            .WithMany(ci => ci.Items)
            .HasForeignKey(ci => ci.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ci => ci.Price)
            .IsRequired();

        builder.Property(ci => ci.Amount)
            .IsRequired();
    }
}
