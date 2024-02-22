
using Domain.Models;

namespace Domain.Services
{
    public interface IVideoService
    {
        Video Get(int id);
        Video Get(string name);
        List<Video> GetAll();
        int Create(Video video);
        void CreateVideoInfo(VideoInfo info);
        void Delete(int id);
    }
}
