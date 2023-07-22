using Graphql_example_code.Infrastructure;
using Graphql_example_code.Infrastructure.Persistence;
using Graphql_example_code.Presentation.Configurations;
using Graphql_example_code.Presentation.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseSetup(builder.Configuration);
builder.Services.RegisterServices();

builder.Services.AddGraphQLServer()
    .AddQueryType<ProductQuery>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.Services.ProviderServices();

//GraphQL
app.MapGraphQL();
app.Run();
