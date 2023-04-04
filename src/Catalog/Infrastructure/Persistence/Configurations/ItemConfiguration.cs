namespace Catalog.Infrastructure.Persistence.Configurations;

internal class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(ci => ci.Category)
            .WithMany()
            .HasForeignKey(ci => ci.CategoryId);

        builder.Property(ci => ci.Price)
            .IsRequired();

        builder.Property(ci => ci.Amount)
            .IsRequired();
    }
}
