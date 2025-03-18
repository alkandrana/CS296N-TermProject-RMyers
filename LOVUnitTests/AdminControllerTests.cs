using Microsoft.AspNetCore.Mvc;

namespace LOVUnitTests;

public class AdminControllerTests
{
    private AdminController _controller;
    private IEntryRepository _repo = new FakeEntryRepository();
    private Article _article;
    private Contribution _contribution;

    public AdminControllerTests()
    {
        _controller = new AdminController(_repo, null, null);
        _article = new Article
        {
            ArticleId = 1,
            Content =
                "Aegon, also known as the Rebuilder, ascended to the throne following the tumultuous reign of Rhaegar the Suppressor. His 25-year rule marked a period of restoration and growth for the Empire of Thasharith.\nKey Accomplishments:\n**Rebuilding of the Academy:**\nAegon took immediate action to rebuild the Dragon's Den Academy, which had been targeted and destroyed during Rhaegar's reign. This institution was dedicated to the pursuit of knowledge and wisdom, offering courses in history, magical studies, philosophy, and sciences.\n**Restoration of the Grand Library:**\nAlongside the Academy, Aegon focused on restoring the Grand Library, aiming to revive the wealth of knowledge lost during the previous reign and encourage intellectual growth.\n**Reinstitution of the Peace Accord:**\nTo maintain peace and harmony within the Empire, Aegon reinstated the Peace Accord, setting forth laws and principles aimed at promoting unity and discouraging conflict between nations.\n**Establishment of the Arx:** Aegon created a protected room known as the Arx for preserving war information. He used his magic to enshrine the room and protect it from unauthorized entry, creating a magical dagger that serves as the key for entry.\n**Revival of Cultural Traditions:**\nAegon encouraged the revival of cultural traditions and practices that were suppressed during the demon king's rule, leading to a renaissance of arts and literature.\n**Establishment of the Dragon's Council:**\nAegon gathered allies, headed by Thoren Tekor, to serve as his advisors and help him rule the Empire.\nPolicies and Reforms:\n**Promulgation of the Dragon's Decree:**\nAegon issued a proclamation outlining the exact expectations and responsibilities of the aristocracy, allowing those who could not abide by the rules to be stripped of rank.\n**Decrees of Equality:**\nAegon issued decrees to ensure equal rights and opportunities for all citizens, regardless of their origin or status, reinforcing the principles of fairness and justice in the Empire.\n**Investment in Education:**\nAegon implemented policies to encourage education among all citizens, establishing more schools and learning centers throughout the Empire.\n**Promotion of Cultural Exchange:**\nAegon initiated programs to promote cultural exchange among the various regions of the Empire, fostering unity and the exchange of cultural knowledge.\nLegacy:\nAegon's reign was characterized by a focus on rebuilding, education, and cultural revival. His efforts laid the foundation for a new era of prosperity and stability in the Empire. At the end of his reign, Aegon peacefully handed the reins of power to Thoren, confident that the provisions he had laid down would protect the people during the next cycle.",
            Title = "Aegon the Rebuilder",
            CategoryId = "B",
            Category = new Category { CategoryId = "B", Name = "Biography" },
            Author = new AppUser { UserName = "vratha" }
        };
        _repo.StoreArticleAsync(_article);
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
        _repo.StoreContributionAsync(_contribution);
    }

    [Fact]
    public void TestEditPost_Success()
    {
        var result = _controller.Edit(_article).Result;
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public void TestEditPost_Failure()
    {
        var result = _controller.Edit(new Article
        {
            ArticleId = 1,
            Title = "This Is A Test",
            CategoryId = "T",
            Content = ""
        }).Result;
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void TestGetContribution_Success()
    {
        var result = _controller.EditContribution(1).Result;
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void TestGetContribution_Failure()
    {
        var result = _controller.EditContribution(0).Result;
        Assert.IsType<RedirectToActionResult>(result);
    }
}