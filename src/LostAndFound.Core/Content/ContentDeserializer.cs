using LostAndFound.Core.System;
using Newtonsoft.Json;

namespace LostAndFound.Core.Content
{
    internal class ContentDeserializer : IContentDeserializer
    {
        private readonly IFileSystem _fileSystem;

        public ContentDeserializer(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public T Get<T>(string path)
        {
            var text = _fileSystem.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<T>(text);
            return data;
        }
    }
}