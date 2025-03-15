using Microsoft.AspNetCore.Mvc;

namespace LOVUnitTests;

public class ConverseControllerTests
{
    private IEntryRepository _repo = new FakeEntryRepository();
    private ConverseController _controller;
    private Contribution _contribution;
    private Conversation _conversation;
    private ResponseVM _response;

    public ConverseControllerTests()
    {
        _controller = new ConverseController(_repo, null);
        _contribution = new Contribution
        {
            Title = "A Brief Dynastic History",
            Content =
                "The Empire of Thasharith follows a cyclical pattern of rule, with each cycle lasting 125 years. Each cycle consists of five distinct periods: the Dragon's Enlightenment (25 years) followed by three Human Reigns (25 years each), and concluding with the Demon King's Ascension (25 years).\n\n### Cycle 1 (Years 1-125)\n\nThe first cycle began with the reign of Vathyri the Dragon, the legendary leader of the warriors who fought in the Demon War, and was marked by unprecedented magical advancement and the establishment of fundamental imperial institutions. The subsequent reigns under Alaric, Callista, and Kaelan saw the development of cultural traditions and defensive measures against emerging demon threats. The cycle concluded with Rhaegar the Suppressor's reign, the first of the Tyrannies.\n\n### Cycle 2 (Years 126-250)\n\nAegon the Rebuilder initiated the second cycle's Enlightenment, focusing on restoration and fortification. The reigns of Thoren, Isolde, and Valorian emphasized technological advancement and preparations for defense against the demons. Morwen's revelation as the Demon King marked the cycle's end, characterized by technological acceleration amid institutional corruption.\n\n### Cycles 3 and 4 (Years 251-500)\n\nLimited historical records exist for Cycles 3 and early 4. The current era falls under Vortigern's Ascension in Cycle 4, preceded by Valerien's reign which witnessed the emergence of the destabilizing Alsacian cult.\n\n### Historical Significance\n\nThis cyclical pattern demonstrates the ongoing struggle between draconic, human, and demonic influences in imperial governance. Each cycle shows evolving strategies in magical development, technological advancement, and demonic resistance, though the pattern of infiltration and corruption persists.",
            Date = DateTime.Now,
            Contributor = new AppUser
            {
                UserName = "filbert",
                SignUpDate = new DateTime(2020, 12, 16)
            }
        };
        _conversation = new Conversation
        {
            Title = "Dynastic cycles",
            Content = "Maybe it's just me, but I've always found this a very queer pattern.",
            StartDate = DateTime.Now,
            Author = new AppUser
            {
                UserName = "alkandrana",
                SignUpDate = new DateTime(2020, 12, 16)
            }
        };
        _response = new ResponseVM
        {
            ConversationId = 1,
            Content = "It is a remarkably reliable pattern.",
            Commenter = new AppUser
            {
                UserName = "hilda",
                SignUpDate = new DateTime(2020, 12, 16)
            }
        };
    }

    [Fact]
    public void TestContributionPost_Success()
    {
        var result = _controller.Contribute(_contribution).Result;
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public void TestContributionPost_Failure()
    {
        var result = _controller.Contribute(null).Result;
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void TestConversationPost_Success()
    {
        var result = _controller.Start(_conversation).Result;
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public void TestConversationPost_Failure()
    {
        var result = _controller.Start(null).Result;
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void ReplyPost_Success()
    {
        // Arrange
        _repo.StoreConversationAsync(_conversation);
        // Act
        var result = _controller.Reply(_response).Result;
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public void ReplyPost_Failure()
    {
        var result = _controller.Reply(null).Result;
        Assert.IsType<ViewResult>(result);
    }
}