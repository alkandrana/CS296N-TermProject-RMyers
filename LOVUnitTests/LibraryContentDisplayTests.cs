namespace LOVUnitTests;

public class LibraryContentDisplayTest
{
    private Article _article;
    private ITestOutputHelper _output;
    private LibraryController _controller;
    private IEntryRepository _repo = new FakeEntryRepository();

    public LibraryContentDisplayTest(ITestOutputHelper output)
    {
        _output = output;
        _article = new Article
        {
            ArticleId = 1,
            Content =
                "Aegon, also known as the Rebuilder, ascended to the throne following the tumultuous reign of Rhaegar the Suppressor. His 25-year rule marked a period of restoration and growth for the Empire of Thasharith.\nKey Accomplishments:\n**Rebuilding of the Academy:**\nAegon took immediate action to rebuild the Dragon's Den Academy, which had been targeted and destroyed during Rhaegar's reign. This institution was dedicated to the pursuit of knowledge and wisdom, offering courses in history, magical studies, philosophy, and sciences.\n**Restoration of the Grand Library:**\nAlongside the Academy, Aegon focused on restoring the Grand Library, aiming to revive the wealth of knowledge lost during the previous reign and encourage intellectual growth.\n**Reinstitution of the Peace Accord:**\nTo maintain peace and harmony within the Empire, Aegon reinstated the Peace Accord, setting forth laws and principles aimed at promoting unity and discouraging conflict between nations.\n**Establishment of the Arx:** Aegon created a protected room known as the Arx for preserving war information. He used his magic to enshrine the room and protect it from unauthorized entry, creating a magical dagger that serves as the key for entry.\n**Revival of Cultural Traditions:**\nAegon encouraged the revival of cultural traditions and practices that were suppressed during the demon king's rule, leading to a renaissance of arts and literature.\n**Establishment of the Dragon's Council:**\nAegon gathered allies, headed by Thoren Tekor, to serve as his advisors and help him rule the Empire.\nPolicies and Reforms:\n**Promulgation of the Dragon's Decree:**\nAegon issued a proclamation outlining the exact expectations and responsibilities of the aristocracy, allowing those who could not abide by the rules to be stripped of rank.\n**Decrees of Equality:**\nAegon issued decrees to ensure equal rights and opportunities for all citizens, regardless of their origin or status, reinforcing the principles of fairness and justice in the Empire.\n**Investment in Education:**\nAegon implemented policies to encourage education among all citizens, establishing more schools and learning centers throughout the Empire.\n**Promotion of Cultural Exchange:**\nAegon initiated programs to promote cultural exchange among the various regions of the Empire, fostering unity and the exchange of cultural knowledge.\nLegacy:\nAegon's reign was characterized by a focus on rebuilding, education, and cultural revival. His efforts laid the foundation for a new era of prosperity and stability in the Empire. At the end of his reign, Aegon peacefully handed the reins of power to Thoren, confident that the provisions he had laid down would protect the people during the next cycle.",
            Title = "Aegon the Rebuilder",
            CategoryId = "B",
            Category = new Category {CategoryId = "B", Name = "Biography"},
            Author = new AppUser {UserName = "vratha"}
        };
        
        // controller

        _controller = new LibraryController(_repo, null);
        _repo.StoreArticleAsync(_article);
    }
    [Fact]
    public void TestContentIteration()
    {
        int i = 0;
        // string outString = "This is a test.";
        string[] paragraphs = _article.Content.Split('\n');
        for (; i < 1 && i < _article.Content.Length; i++)// okay, so my for loop wasn't working 
        {   // the way I expected because the unit in the array is a PARAGRAPH, 
            // not a sentence!
            _output.WriteLine(i + ": " + paragraphs[i] + "\n");
        }
        Assert.Equal(1, i);
    }
    
}