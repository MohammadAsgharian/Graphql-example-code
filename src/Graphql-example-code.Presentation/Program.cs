using Graphql_example_code.Infrastructure;
using Graphql_example_code.Presentation.Configurations;
using Graphql_example_code.Presentation.Mutations;
using Graphql_example_code.Presentation.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseSetup(builder.Configuration);
builder.Services.RegisterServices();

builder.Services.AddGraphQLServer()
    .AddQueryType<ProductTypes>()
    .AddMutationType<ProductMutations>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.Services.ProviderServices();

//GraphQL
app.MapGraphQL();
app.Run();
