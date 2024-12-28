using Microsoft.AspNetCore.Mvc;

namespace SkeinGang.AdminUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/admin-ui/statics")]
public class TeamsController
{
    [HttpGet]
    public IActionResult Index() => throw new NotImplementedException();
    
    [HttpGet]
    [Route("edit/{teamId:long}")]
    public IActionResult EditTeam(long teamId) => throw new NotImplementedException();
    
    [HttpGet]
    [Route("create")]
    public IActionResult Create() => throw new NotImplementedException();
    
    [HttpGet]
    [Route("members/find-user")]
    public IActionResult FindUser() => throw new NotImplementedException();
    
    [HttpPost]
    [Route("updateStatic")]
    public IActionResult UpdateTeam() => throw new NotImplementedException();
    
    [HttpPost]
    [Route("updateStaticRow")]
    public IActionResult UpdateTeamRow() => throw new NotImplementedException();
    
    [HttpPost]
    [Route("createStatic")]
    public IActionResult CreateTeam() => throw new NotImplementedException();
    
    [HttpGet]
    [Route("{teamId:long}/memberSearch")]
    public IActionResult FindTeamMembers(long teamId, [FromQuery] string playerName) => throw new NotImplementedException();
    
    [HttpPut]
    [Route("{teamId:long}/members/{memberId:long}")]
    public IActionResult UpdateTeamMember(long teamId, long memberId) => throw new NotImplementedException();
    
    [HttpDelete]
    [Route("{teamId:long}/members/{memberId:long}")]
    public IActionResult DeleteTeamMember(long teamId, long memberId) => throw new NotImplementedException();
    
    [HttpPost]
    [Route("{teamId:long}/members")]
    public IActionResult AddTeamMember(long teamId) => throw new NotImplementedException();
    
    [HttpPost]
    [Route("{teamId:long}/membersCreateAndAdd")]
    public IActionResult CreateAndAddMember(long teamId) => throw new NotImplementedException();
}