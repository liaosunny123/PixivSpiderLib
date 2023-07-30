using System.Text.Json;
using System.Text.Json.Nodes;
using RestSharp;

namespace PixivSpider;

public class Spider
{
    private string _version = "19921c54619a740796d32683244aad17e288a534";

    public void SetVersion(string version)
    {
        _version = version;;
    }
    
    private string _cookie = String.Empty;

    public Spider(string cookie)
    {
        _cookie = cookie;
    }
    
    public async Task<List<string>> GetIllustsByAuthorId(string id)
    {
        var client = new RestClient($"https://www.pixiv.net/ajax/user/{id}/profile/all?lang=zh&version={_version}");
        var req = new RestRequest();
        var res = await client.ExecuteAsync(req);
        var node = JsonDocument.Parse(res.Content!);
        return node.RootElement.GetProperty("body").GetProperty("illusts").EnumerateObject().Select(s => s.Name)
            .ToList();
    }
    
    public async Task<List<string>> GetPixivPictureInPage(List<string> id, bool isFirstPage = true)
    {
        var client = new RestClient($"https://www.pixiv.net/ajax/user/{id}/profile/illusts");
        var req = new RestRequest();
        id.ForEach(sp => req.AddParameter("ids[]", sp));
        req.AddParameter("work_category", "illustManga");
        req.AddParameter("is_first_page", isFirstPage ? "1" : "0");
        req.AddParameter("lang", "zh");
        req.AddParameter("version", _version);
        var res = await client.ExecuteAsync(req);
        var node = JsonDocument.Parse(res.Content!);
        return node.RootElement.GetProperty("body").GetProperty("works").EnumerateObject()
            .Select(s => s.Value.GetProperty("url").ToString()).ToList();
    }
}