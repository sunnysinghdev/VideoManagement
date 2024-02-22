using Domain.Exceptions;
using Domain.Models;
using Domain.Repository;

namespace Domain.Services
{
    public class VideoService : IVideoService
    {
        readonly IVideoRepository _videoRepository;
        public VideoService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }
        public int Create(Video video)
        {
            return _videoRepository.Create(video);
            
        }

        public void CreateVideoInfo(VideoInfo info)
        {
            _videoRepository.CreateVideoInfo(info);

        }

        public void Delete(int id)
        {
            var video = _videoRepository.Get(id);
            if (video == null)
            {
                throw new VideoNotFoundException(id.ToString());
            }
            _videoRepository.Delete(video);
        }

        public Video Get(int id)
        {
            var video = _videoRepository.Get(id);
            if (video == null)
            {
                throw new VideoNotFoundException(id.ToString());
            }
            return video;
        }

        public Video Get(string name)
        {
            throw new NotImplementedException();
        }

        public List<Video> GetAll()
        {
            return _videoRepository.GetAll();
        }
    }
}
