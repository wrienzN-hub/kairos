using Microsoft.EntityFrameworkCore;

namespace Kairos.Infrastructure.Persistence;

public sealed class KairosDbContext(DbContextOptions<KairosDbContext> options)
    : DbContext(options);
