using Microsoft.AspNetCore.Identity;
using CS296N_TermProject_RMyers.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CS296N_TermProject_RMyers.Data;

public class SeedData
{
    public static void Seed(AppDbContext ctx, IServiceProvider provider)
    {
        if (!ctx.Articles.Any())
        {
            #region categories

            Category catH = new Category { CategoryId = "H", Name = "History" };
            Category catM = new Category { CategoryId = "M", Name = "Magic" };
            Category catL = new Category { CategoryId = "L", Name = "Legend" };
            Category catI = new Category { CategoryId = "I", Name = "Invention" };
            Category catB = new Category { CategoryId = "B", Name = "Biography" };

            ctx.Categories.Add(catH);
            ctx.Categories.Add(catM);
            ctx.Categories.Add(catL);
            ctx.Categories.Add(catI);
            ctx.Categories.Add(catB);

            #endregion

            #region users

            var userManager = provider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            const string password = "Alk*ndr2na";
            const string role = "Admin";
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(role)).Wait();
            }

            AppUser contributor1 = new AppUser
            {
                UserName = "vratha",
                SignUpDate = new DateTime(2013, 07, 26)
            };

            AppUser contributor2 = new AppUser
            {
                UserName = "damian",
                SignUpDate = new DateTime(2013, 07, 30)
            };

            AppUser alkandrana = new AppUser
            {
                UserName = "alkandrana",
                SignUpDate = new DateTime(2013, 12, 25)
            };
            AppUser filbert = new AppUser
            {
                UserName = "filbert",
                SignUpDate = new DateTime(2020, 12, 16)
            };
            AppUser hilda = new AppUser
            {
                UserName = "hilda",
                SignUpDate = new DateTime(2020, 12, 20)
            };
            bool isSuccess = userManager.CreateAsync(contributor1, password).Result.Succeeded;
            if (isSuccess)
            {
                userManager.AddToRoleAsync(contributor1, role).Wait();
            }

            isSuccess = userManager.CreateAsync(contributor2, password).Result.Succeeded;
            isSuccess = userManager.CreateAsync(alkandrana, password).Result.Succeeded;
            isSuccess = userManager.CreateAsync(filbert, password).Result.Succeeded;
            isSuccess = userManager.CreateAsync(hilda, password).Result.Succeeded;

            #endregion

            #region articles

            if (isSuccess)
            {
                Article magic = new Article
                {
                    Content =
                        "Magic in the Empire\nMagic in the Empire of Thasharith is a complex and evolving phenomenon, deeply intertwined with the history and cycles of power within the realm. Its nature and prevalence have fluctuated throughout the Empire's history, influenced by the reigns of the dragons versus the human dynasties.\nOrigins and Early Prominence\nThe origins of magic in the Empire can be traced back to the initial War with the demons. During this time, magic played a crucial role in defending the world against the demonic incursion. THe dragon who led the humans during the War possessed a potent magic that was bane to the demons, and this power was key to their victory.\nThe Great Magical Surge\nDuring the first reign of the dragon, known as Vathyri, there was a significant increase in magical energy throughout the Empire. This period, called 'The Great Magical Surge,' led to numerous magical discoveries and advancements in spellcraft. Traditional mages and sages, who were attuned to the indigenous currents of magic, recognised the dragon's influence on magical energies.\nThe Role of Sages\nSages played a crucial role in preserving and teaching magical knowledge. They were the custodians of the mystical forms of magic in the world and were entrusted with the Dragon's Den Academy, an institution dedicated to magical studies among other disciplines.\nSuppression and Decline\nHowever, magic faced a severe setback during the reign of Rhaegar the Suppressor, a period when the demon king's spirit possessed the Emperor. Rhaegar issued decrees suppressing the practice of magic, aiming to quell potential threats to his rule and suppress any form of magic that was not of demonic origin. This led to the execution or renunciation of many sages, resulting in a significant loss of magical knowledge.\nCurrent State\nIn the present day, magic in the Empire is viewed with a mixture of skepticism and caution. The eradication of the sages and decades of estrangement from magic, coupled with the negative associations created by the demon king's reign, have led to a jaded view of magic among the citizens. However, efforts have been made by subsequent rulers to revive and study magical practices, particularly in preparation for potential future conflicts with demons.\nTechnological Integration\nIn recent times, there has been a push to integrate magical knowledge with technological advancements. Special forces have been trained in both combat and the use of new technologies, which may include magical elements. This blend of magic and technology represents the Empire's current approach to harnessing magical power while maintaining control and understanding of its applications.",
                    Title = "A Brief Survey of Magic in the Empire",
                    Author = contributor1,
                    Category = catM,
                    CategoryId = "M"
                };
                ctx.Articles.Add(magic);

                Article ustores = new Article
                {
                    Content =
                        "Since the reign of the Emperor Kaelan in the early days of the Empire, Harbinger of the First Cycle, ustores have terrorised the peoples of the Empire. Although they have, from time to time, faded from memory due to their scarcity, and due to the comparative prosperity of most regions of the Empire, they always return to remind us of the ongoing threat of the demons.\nThe most that any citizen seems to know of these creatures is that they possess demonic powers and are, most likely, related to and under the control of the demons. The truth is far more terrible, and far more ancient, than that.\n**Origins**\nIn the aftermath of the Demon War, one of the greatest of the warriors who had fought in that conflict fell prey to the Cruciatus. Ever since that time, the demons had been experimenting on their human victims, trying to find a way to bring them under their control. For, if they could exercise such power over a creature that was not truly demonic in origin, such a creature would be able to transcend the magical laws that hold the demons bound to their world and prevent their existence in the human one. Around the time of the reign of the Emperor Kaelan, the demon king at last succeeded in instilling his will in those victims and sent them out into the world to do his bidding. Because they were under his control, yet still human in origin and thus able to move freely in the human world, they were able to conduct lightning raids against the neighbouring communities to gather more victims to swell their own ranks. To this day, those who inhabit the regions closest to the ustores' nightmarish dwelling-place, the Tower of Shadows, are at risk from such raids.\n**The Nature of Ustores**\nUstores represent the demon king's will in the world. They fill a loophole in the magical agreement between the warriors of the Demon War and the demons, and thus there is no appeal against their presence in the world. It is a loophole that the warriors who drafted that agreement could not have foreseen, for what the demon king did to his human victims was so reprehensible as to have been completely inconceivable from the perspective of those warriors who defended the world of humanity during the War. Nevertheless, until recent times, the presence of ustores in the world did little to disturb the enduring prosperity of the regions of the Empire, and thus an attitude of complacency that outright dismissed the existence of ustores was allowed to grow and flourish, paving the way for the demon king to use them to gain a foothold in the world.\n**Humanity**\nTo this day, there has never been recorded an instance of an ustor resisting the will of the demon king. Whatever he did to them, it seems to have completely eradicated their own will, or even, to judge by the pattern of their behaviour, sense of self. Whether they retain enough of a semblance of their humanity that they could still be healed would be even harder to determine, for no human has ever escaped an encounter with even a single ustor unscathed. I would urge the citizens of the Empire to remain hopeful in that regard, but to exercise extreme caution in any such encounter. For so long as the demon king retains control over them, he will do anything to keep his power over them from slipping.\n**Present Day**\nIt is an established fact that ustores have come out into the open since Vortigern's assumption of the throne. Besides being an ongoing threat to the everyday safety of all citizens, this represents a disturbing trend in the ancient threads of power, for it suggests that the demons themselves are growing bolder and testing the limits of their power. Yet there is cause for hope, as well. Despite the common complacency that marks the Empire's recent attitudes towards both demons and ustores, the Empire still retains a handful of warriors who still remember the ways of the warriors who fought the demons themselves. Such warriors possess a weapon, based on ancient magics, that is able to stop ustores cold. Such warriors must, for their own protection, remain anonymous, but they will always turn up where they are most needed.",
                    Title = "On Ustores",
                    Author = contributor1,
                    Category = catL,
                    CategoryId = "L"
                };
                ctx.Articles.Add(ustores);

                Article dynast1 = new Article
                {
                    Title = "Aegon the Rebuilder",
                    Content =
                        "Aegon, also known as the Rebuilder, ascended to the throne following the tumultuous reign of Rhaegar the Suppressor. His 25-year rule marked a period of restoration and growth for the Empire of Thasharith.\nKey Accomplishments:\n**Rebuilding of the Academy:**\nAegon took immediate action to rebuild the Dragon's Den Academy, which had been targeted and destroyed during Rhaegar's reign. This institution was dedicated to the pursuit of knowledge and wisdom, offering courses in history, magical studies, philosophy, and sciences.\n**Restoration of the Grand Library:**\nAlongside the Academy, Aegon focused on restoring the Grand Library, aiming to revive the wealth of knowledge lost during the previous reign and encourage intellectual growth.\n**Reinstitution of the Peace Accord:**\nTo maintain peace and harmony within the Empire, Aegon reinstated the Peace Accord, setting forth laws and principles aimed at promoting unity and discouraging conflict between nations.\n**Establishment of the Arx:**\nAegon created a protected room known as the Arx for preserving war information. He used his magic to enshrine the room and protect it from unauthorized entry, creating a magical dagger that serves as the key for entry.\n**Revival of Cultural Traditions:**\nAegon encouraged the revival of cultural traditions and practices that were suppressed during the demon king's rule, leading to a renaissance of arts and literature.\n**Establishment of the Dragon's Council:**\nAegon gathered allies, headed by Thoren Tekor, to serve as his advisors and help him rule the Empire.\nPolicies and Reforms:\n**Promulgation of the Dragon's Decree:**\nAegon issued a proclamation outlining the exact expectations and responsibilities of the aristocracy, allowing those who could not abide by the rules to be stripped of rank.\n**Decrees of Equality:**\nAegon issued decrees to ensure equal rights and opportunities for all citizens, regardless of their origin or status, reinforcing the principles of fairness and justice in the Empire.\n**Investment in Education:**\nAegon implemented policies to encourage education among all citizens, establishing more schools and learning centers throughout the Empire.\n**Promotion of Cultural Exchange:**\nAegon initiated programs to promote cultural exchange among the various regions of the Empire, fostering unity and the exchange of cultural knowledge.\nLegacy:\nAegon's reign was characterized by a focus on rebuilding, education, and cultural revival. His efforts laid the foundation for a new era of prosperity and stability in the Empire. At the end of his reign, Aegon peacefully handed the reins of power to Thoren, confident that the provisions he had laid down would protect the people during the next cycle.",
                    Author = contributor2,
                    Category = catB,
                    CategoryId = "B"
                };

                ctx.Articles.Add(dynast1);

                Article dynast2 = new Article
                {
                    Title = "Valorian the Prescient",
                    Content =
                        "Valorian, known as \"the Prescient,\" reigned over the Empire as the third sovereign of Thoren's line, of the Second Era. His reign was marked by significant technological advancements and a heightened focus on preparing the Empire for potential threats.\nKey Achievements:\n**Introduction of Advanced Technologies:**\nValorian oversaw the implementation of advanced technologies across various sectors of the Empire, leading to significant social and economic transformations. These innovations were the culmination of technological developments initiated during Thoren's reign.\n**Public Awareness Campaigns:**\nBreaking with the policies of previous monarchs, Valorian launched public awareness campaigns about the history and threat of demons. This unprecedented move was aimed at ensuring citizens were informed and prepared for potential future conflicts, but in practice it only served to reinforce the common perception of the Emperor as a bit paranoid and even mentally unstable.\n**Training of Special Forces:**\nValorian established a program to train specialized military units skilled in both combat and the use of new technologies. These elite forces worked directly under Imperial mandate and possessed knowledge inaccessible to the average citizen.\nControversies and Downfall:\nAs his reign progressed, Valorian became increasingly paranoid about the potential return of the demon king. His suspicions extended even to his own sons, leading to persecutions of those he deemed suspect. This behavior ultimately led to his daughter, Morwen, ordering his imprisonment and taking control of the Empire.\nAlthough Valorian was much maligned in his own time, posterity judged him prescient after it became public knowledge that the Empress Morwen was Influenced by the demon king. The mysterious circumstances of his disappearance after imprisonment have led to all kinds of theories arising surrounding his ultimate fate, including the rather far-fetched theory that he never died, but was imprisoned by the demon king and survives to this day as a kind of supernatural captive of the demons.\nValorian's reign marked a pivotal period in the Empire's history, characterized by technological progress and increased vigilance against supernatural threats. However, his legacy is complicated by his descent into paranoia and the mysterious circumstances surrounding the end of his rule.",
                    Author = alkandrana,
                    Category = catB,
                    CategoryId = "B"
                };

                ctx.Articles.Add(dynast2);
                ctx.SaveChanges();
            }

            #endregion
            
            #region contributions

            Contribution dynasty = new Contribution
            {
                Title = "A Brief Dynastic History",
                Content =
                    "The Empire of Thasharith follows a cyclical pattern of rule, with each cycle lasting 125 years. Each cycle consists of five distinct periods: the Dragon's Enlightenment (25 years) followed by three Human Reigns (25 years each), and concluding with the Demon King's Ascension (25 years).\n\n### Cycle 1 (Years 1-125)\n\nThe first cycle began with the reign of Vathyri the Dragon, the legendary leader of the warriors who fought in the Demon War, and was marked by unprecedented magical advancement and the establishment of fundamental imperial institutions. The subsequent reigns under Alaric, Callista, and Kaelan saw the development of cultural traditions and defensive measures against emerging demon threats. The cycle concluded with Rhaegar the Suppressor's reign, the first of the Tyrannies.\n\n### Cycle 2 (Years 126-250)\n\nAegon the Rebuilder initiated the second cycle's Enlightenment, focusing on restoration and fortification. The reigns of Thoren, Isolde, and Valorian emphasized technological advancement and preparations for defense against the demons. Morwen's revelation as the Demon King marked the cycle's end, characterized by technological acceleration amid institutional corruption.\n\n### Cycles 3 and 4 (Years 251-500)\n\nLimited historical records exist for Cycles 3 and early 4. The current era falls under Vortigern's Ascension in Cycle 4, preceded by Valerien's reign which witnessed the emergence of the destabilizing Alsacian cult.\n\n### Historical Significance\n\nThis cyclical pattern demonstrates the ongoing struggle between draconic, human, and demonic influences in imperial governance. Each cycle shows evolving strategies in magical development, technological advancement, and demonic resistance, though the pattern of infiltration and corruption persists.",
                Date = DateTime.Now,
                Contributor = filbert
            };

            #endregion

            #region conversations

            Conversation sociology = new Conversation
            {
                Title = "Dynastic cycles",
                Content = "Maybe it's just me, but I've always found this a very queer pattern.",
                StartDate = DateTime.Now,
                Author = alkandrana
            };
            Response soc1 = new Response
            {
                Author = hilda,
                Content = "It is a remarkably reliable pattern.",
                Date = DateTime.Now,
                Conversation = sociology
            };

            #endregion
        }
    }
}