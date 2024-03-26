using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBilling.Apis;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace FreeBilling.Tests.Apis;

public class ProjectApiTests : BaseTest
{
  private IBillingRepository _repo;

  public ProjectApiTests()
  {
    var svcs = GenerateServices();
    _repo = svcs.GetService<IBillingRepository>()!;
  }

  [Fact]
  public async Task CanReadProjects()
  {
    var result = await CustomerProjectsApi.GetProjects(_repo, 1);
    Assert.IsAssignableFrom<Ok<IEnumerable<Project>>>(result);
    var projects = ((Ok<IEnumerable<Project>>)result).Value;
    Assert.NotNull(projects);
    Assert.True(projects.Count() > 0);
  }

  [Fact]
  public async Task CanReadProject()
  {
    var result = await CustomerProjectsApi.GetProject(_repo, 1, 1);
    Assert.IsAssignableFrom<Ok<Project>>(result);
    var project = ((Ok<Project>)result).Value;
    Assert.NotNull(project);
    Assert.True(project.ProjectName == "SEO Help");
  }

  [Fact]
  public async Task CantFindProject()
  {
    var result = await CustomerProjectsApi.GetProject(_repo, 1, 30);
    Assert.IsAssignableFrom<NotFound>(result);
  }

  [Fact]
  public async Task BadRequestProjectWithWrongCustomer()
  {
    var result = await CustomerProjectsApi.GetProject(_repo, 2, 1);
    Assert.IsAssignableFrom<BadRequest<string>>(result);
  }
}
