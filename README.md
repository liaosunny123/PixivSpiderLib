# PixivSpiderLib
Pixiv 协议爬虫支持

# Demo

```c#
var spider = new Spider(
    """
your ck
"""
    );
    var ids = await spider.GetIllustsByAuthorId("ids");
    var list = await spider.GetPixivPictureInPage(ids.Take(48).ToList());
    list.ForEach(Console.WriteLine);
```

