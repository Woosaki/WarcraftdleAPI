using Microsoft.AspNetCore.Mvc;
using System.Collections;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AffiliationController : ControllerBase
{
	[HttpGet]
	public async Task<IEnumerable<Affiliation>> Get()
	{

	}
}
