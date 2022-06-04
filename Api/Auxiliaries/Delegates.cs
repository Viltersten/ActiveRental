using System.Reflection;

namespace Api.Auxiliaries;

static class Delegates
{
    public static Action<DbContextOptionsBuilder> ContextOptions(ConfigurationManager config, bool inMemory = default)
    {
        if(inMemory)
            return builder => builder.UseInMemoryDatabase("TargetDb");

        string? connection = config.GetConnectionString("TargetDb");
        string? assembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        ArgumentNullException.ThrowIfNull(connection);
        Action<DbContextOptionsBuilder> output = builder
            => builder.UseSqlServer(
                connection,
                sqlOptions => sqlOptions.MigrationsAssembly(assembly));

        return output;
    }
}