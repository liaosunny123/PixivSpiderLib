// See https://aka.ms/new-console-template for more information

using PixivSpider;

var spider = new Spider(
    """
your ck
"""
    );
    var ids = await spider.GetIllustsByAuthorId("ids");
    var list = await spider.GetPixivPictureInPage(ids.Take(48).ToList());
    list.ForEach(Console.WriteLine);