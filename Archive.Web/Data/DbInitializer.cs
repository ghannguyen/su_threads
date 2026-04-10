using Archive.Web.Extensions;
using Archive.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Archive.Web.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext dbContext)
    {
        await dbContext.Database.EnsureCreatedAsync();

        if (await dbContext.Users.AnyAsync())
        {
            return;
        }

        var passwordHasher = new PasswordHasher<AppUser>();

        var adminRole = new Role
        {
            Name = "Admin",
            Description = "Quản trị hệ thống ARCHIVE"
        };

        var userRole = new Role
        {
            Name = "User",
            Description = "Người dùng thông thường"
        };

        dbContext.Roles.AddRange(adminRole, userRole);
        await dbContext.SaveChangesAsync();

        var topics = new List<Topic>
        {
            new() { Name = "Cà phê & matcha", Slug = "ca-phe-matcha", Description = "Đồ uống yêu thích, quán quen và những buổi sáng dễ thương." },
            new() { Name = "Đi học đi làm", Slug = "di-hoc-di-lam", Description = "Deadline, họp sớm, tan ca và nhịp sống mỗi ngày." },
            new() { Name = "Sống chill", Slug = "song-chill", Description = "Healing, cuối tuần, những dòng nhẹ và những ngày muốn chậm lại." },
            new() { Name = "Góc đẹp quán xinh", Slug = "goc-dep-quan-xinh", Description = "Decor, outfit, chụp ảnh và những nơi muốn ngồi thật lâu." }
        };

        dbContext.Topics.AddRange(topics);
        await dbContext.SaveChangesAsync();

        var users = new List<AppUser>
        {
            new()
            {
                RoleId = adminRole.Id,
                UserName = "admin",
                DisplayName = "Quản trị ARCHIVE",
                Email = "admin@archive.local",
                Bio = "Giữ feed gọn, sạch và tử tế.",
                IsActive = true,
                PasswordHash = string.Empty,
                CreatedAt = DateTime.UtcNow.AddDays(-14)
            },
            new()
            {
                RoleId = userRole.Id,
                UserName = "linh",
                DisplayName = "Linh An",
                Email = "linh@archive.local",
                Bio = "Matcha, quán nhỏ và những dòng status viết lúc sáng sớm.",
                IsActive = true,
                PasswordHash = string.Empty,
                CreatedAt = DateTime.UtcNow.AddDays(-12)
            },
            new()
            {
                RoleId = userRole.Id,
                UserName = "han",
                DisplayName = "Hân Vy",
                Email = "han@archive.local",
                Bio = "Deadline, trà sữa, tóc tai và một chút tâm trạng cuối ngày.",
                IsActive = true,
                PasswordHash = string.Empty,
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new()
            {
                RoleId = userRole.Id,
                UserName = "ngoc",
                DisplayName = "Ngọc Trâm",
                Email = "ngoc@archive.local",
                Bio = "Thích decor bàn học, skincare tối và chụp gương lúc đẹp trời.",
                IsActive = true,
                PasswordHash = string.Empty,
                CreatedAt = DateTime.UtcNow.AddDays(-9)
            },
            new()
            {
                RoleId = userRole.Id,
                UserName = "minh",
                DisplayName = "Minh Khoa",
                Email = "minh@archive.local",
                Bio = "Cà phê đá, mưa tan tầm và vài suy nghĩ trước khi ngủ.",
                IsActive = true,
                PasswordHash = string.Empty,
                CreatedAt = DateTime.UtcNow.AddDays(-8)
            }
        };

        foreach (var user in users)
        {
            var rawPassword = user.RoleId == adminRole.Id ? "Admin@123" : "User@123";
            user.PasswordHash = passwordHasher.HashPassword(user, rawPassword);
        }

        dbContext.Users.AddRange(users);
        await dbContext.SaveChangesAsync();

        var posts = new List<Post>
        {
            new()
            {
                UserId = users[1].Id,
                TopicId = topics[0].Id,
                Content = "Ly matcha sáng nay cứu nguyên buổi họp 8 giờ. Uống một ngụm là thấy mình hiền hơn hẳn. #matcha #buoisang",
                CreatedAt = DateTime.UtcNow.AddHours(-20)
            },
            new()
            {
                UserId = users[2].Id,
                TopicId = topics[1].Id,
                Content = "Deadline vẫn còn đó nhưng mình vừa kịp order trà sữa ít đá. Có đồ ngọt là mood làm bài tăng lên chút xíu. #deadline #trasua",
                CreatedAt = DateTime.UtcNow.AddHours(-18)
            },
            new()
            {
                UserId = users[3].Id,
                TopicId = topics[3].Id,
                Content = "Dọn lại bàn học, thêm đèn vàng và một lọ hoa nhỏ. Góc này tối nay xinh quá nên chưa muốn đi ngủ. #gocxinh #decor",
                CreatedAt = DateTime.UtcNow.AddHours(-15)
            },
            new()
            {
                UserId = users[4].Id,
                TopicId = topics[2].Id,
                Content = "Mưa Sài Gòn tới đúng lúc tan làm, đứng nép ở hiên quán cà phê 10 phút mà thấy ngày chậm lại hẳn. #saigon #troimua",
                CreatedAt = DateTime.UtcNow.AddHours(-12)
            },
            new()
            {
                UserId = users[1].Id,
                TopicId = topics[2].Id,
                Content = "Có những hôm chỉ muốn đăng một câu ngắn rồi thôi: mình ổn hơn hôm qua một chút. Vậy là được. #healing #nhatky",
                CreatedAt = DateTime.UtcNow.AddHours(-8)
            },
            new()
            {
                UserId = users[2].Id,
                TopicId = topics[3].Id,
                Content = "Outfit đi làm hôm nay chỉ là sơ mi trắng với quần đen mà lên ảnh gương lại ưng ghê. #outfit #dailylook",
                CreatedAt = DateTime.UtcNow.AddHours(-5)
            }
        };

        dbContext.Posts.AddRange(posts);
        await dbContext.SaveChangesAsync();

        dbContext.PostImages.AddRange(
            new PostImage { PostId = posts[0].Id, ImageUrl = "/images/seed/post-soft-room.svg", DisplayOrder = 0 },
            new PostImage { PostId = posts[1].Id, ImageUrl = "/images/seed/post-strawberry-cake.svg", DisplayOrder = 0 },
            new PostImage { PostId = posts[4].Id, ImageUrl = "/images/seed/post-pastel-desk.svg", DisplayOrder = 0 });

        dbContext.Follows.AddRange(
            new Follow { FollowerId = users[1].Id, FollowingId = users[2].Id },
            new Follow { FollowerId = users[1].Id, FollowingId = users[3].Id },
            new Follow { FollowerId = users[2].Id, FollowingId = users[1].Id },
            new Follow { FollowerId = users[3].Id, FollowingId = users[1].Id },
            new Follow { FollowerId = users[4].Id, FollowingId = users[1].Id });

        dbContext.Likes.AddRange(
            new Like { PostId = posts[0].Id, UserId = users[2].Id },
            new Like { PostId = posts[0].Id, UserId = users[3].Id },
            new Like { PostId = posts[1].Id, UserId = users[1].Id },
            new Like { PostId = posts[1].Id, UserId = users[4].Id },
            new Like { PostId = posts[2].Id, UserId = users[1].Id },
            new Like { PostId = posts[3].Id, UserId = users[2].Id },
            new Like { PostId = posts[4].Id, UserId = users[1].Id },
            new Like { PostId = posts[5].Id, UserId = users[3].Id });

        dbContext.Reposts.AddRange(
            new Repost { PostId = posts[0].Id, UserId = users[4].Id },
            new Repost { PostId = posts[3].Id, UserId = users[1].Id });

        dbContext.Comments.AddRange(
            new Comment
            {
                PostId = posts[0].Id,
                UserId = users[2].Id,
                Content = "Nghe đúng vibe sáng nhẹ nhàng luôn.",
                CreatedAt = DateTime.UtcNow.AddHours(-19)
            },
            new Comment
            {
                PostId = posts[1].Id,
                UserId = users[1].Id,
                Content = "Trà sữa đúng là phúc lợi mùa deadline.",
                CreatedAt = DateTime.UtcNow.AddHours(-17)
            },
            new Comment
            {
                PostId = posts[3].Id,
                UserId = users[3].Id,
                Content = "Đứng hiên quán nhìn mưa rồi chụp tấm story chắc xinh lắm.",
                CreatedAt = DateTime.UtcNow.AddHours(-11)
            });

        dbContext.Reports.Add(new Report
        {
            ReporterUserId = users[1].Id,
            TargetPostId = posts[2].Id,
            Reason = "Dữ liệu demo",
            Details = "Báo cáo mẫu để khu vực quản trị có dữ liệu hiển thị.",
            CreatedAt = DateTime.UtcNow.AddHours(-4)
        });

        await dbContext.SaveChangesAsync();

        foreach (var post in posts)
        {
            var tags = SlugHelper.ExtractHashtags(post.Content);
            foreach (var tag in tags)
            {
                var hashtag = await dbContext.Hashtags.FirstOrDefaultAsync(x => x.Slug == tag);
                if (hashtag is null)
                {
                    hashtag = new Hashtag
                    {
                        Name = tag,
                        Slug = tag
                    };
                    dbContext.Hashtags.Add(hashtag);
                    await dbContext.SaveChangesAsync();
                }

                dbContext.PostHashtags.Add(new PostHashtag
                {
                    PostId = post.Id,
                    HashtagId = hashtag.Id
                });
            }
        }

        dbContext.AdminLogs.Add(new AdminLog
        {
            AdminUserId = users[0].Id,
            Action = "Seed Complete",
            EntityName = "System",
            EntityId = 1,
            Note = "Khởi tạo dữ liệu mẫu ARCHIVE"
        });

        await dbContext.SaveChangesAsync();
    }
}
