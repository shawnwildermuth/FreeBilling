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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace FreeBilling.Tests.Apis;

public class ProjectApiTests : BaseTest
{
  private IBillingRepository _repo;
  private ILogger<CustomerProjectsApi> _logger;
  int customerId = 1;

  public ProjectApiTests()
  {
    var svcs = GenerateServices();
    _repo = svcs.GetService<IBillingRepository>()!;
    _logger = svcs.GetService<ILogger<CustomerProjectsApi>>()!;
  }

  [Fact]
  public async Task CanReadProjects()
  {
    var result = await CustomerProjectsApi.GetProjects(_repo, customerId);
    Assert.IsAssignableFrom<Ok<IEnumerable<Project>>>(result);
    var projects = ((Ok<IEnumerable<Project>>)result).Value;
    Assert.NotNull(projects);
    Assert.True(projects.Count() > 0);
  }

  [Fact]
  public async Task CanReadProject()
  {
    var result = await CustomerProjectsApi.GetProject(_repo, customerId, 1);
    Assert.IsAssignableFrom<Ok<Project>>(result);
    var project = ((Ok<Project>)result).Value;
    Assert.NotNull(project);
    Assert.True(project.ProjectName == "SEO Help");
  }

  [Fact]
  public async Task CantFindProject()
  {
    var result = await CustomerProjectsApi.GetProject(_repo, customerId, 30);
    Assert.IsAssignableFrom<NotFound>(result);
  }

  [Fact]
  public async Task BadRequestProjectWithWrongCustomer()
  {
    var result = await CustomerProjectsApi.GetProject(_repo, customerId + 1, 1);
    Assert.IsAssignableFrom<BadRequest<string>>(result);
  }

  [Fact]
  public async Task CanSaveProject()
  {
    var newItem = new Project()
    {
      Id = 0,
      ProjectName = "Average Grades",
      CustomerId = customerId,
      StartDate = DateTime.Today
    };

    var result = await CustomerProjectsApi.Post(_repo, _logger, customerId, newItem);
    Assert.IsAssignableFrom<Created<Project>>(result);
    var project = ((Created<Project>)result).Value;
    Assert.NotNull(project);
    var found = await CustomerProjectsApi.GetProject(_repo, customerId, project.Id);
    Assert.IsAssignableFrom<Ok<Project>>(found);
    var existing = ((Ok<Project>)found).Value;
    Assert.NotNull(existing);
    Assert.True(existing.ProjectName == newItem.ProjectName);
  }

  [Fact]
  public async Task CanUpdateCustomer()
  {
    var result = await CustomerProjectsApi.GetProject(_repo, customerId, 1);
    Assert.IsAssignableFrom<Ok<Project>>(result);
    var project = ((Ok<Project>)result).Value;
    Assert.NotNull(project);
    var originalName = project.ProjectName;
    project.ProjectName += "-";
    var updated = await CustomerProjectsApi.Put(_repo, _logger, customerId, project.Id, project);
    Assert.IsAssignableFrom<Ok<Project>>(updated);
    var found = await CustomerProjectsApi.GetProject(_repo, customerId, project.Id);
    Assert.IsAssignableFrom<Ok<Project>>(found);
    var existing = ((Ok<Project>)found).Value;
    Assert.NotNull(existing);
    Assert.True(existing.ProjectName == originalName + "-");
  }


  [Fact]
  public async Task CanDeleteProject()
  {
    var newItem = new Project()
    {
      Id = 0,
      ProjectName = "Average Grades",
      CustomerId = customerId,
      StartDate = DateTime.Today
    };

    var created = await CustomerProjectsApi.Post(_repo, _logger, customerId, newItem);
    Assert.IsAssignableFrom<Created<Project>>(created);
    var project = ((Created<Project>)created).Value;
    Assert.NotNull(project);
    var result = await CustomerProjectsApi.Delete(_repo, _logger, customerId, project.Id);
    Assert.IsAssignableFrom<Ok>(result);
    var found = await CustomerProjectsApi.GetProject(_repo, customerId, project.Id);
    Assert.IsAssignableFrom<NotFound>(found);
  }

}
