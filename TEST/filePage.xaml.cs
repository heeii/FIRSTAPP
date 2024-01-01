namespace TEST
{
    
    public partial class filePage : ContentPage
    {
        private readonly FileService _fileService;

        public filePage()
        {
            InitializeComponent();
            _fileService = new FileService();
            GetFiles();
        }

        private async void GetFiles()
        {
            var files = await _fileService.GetFilesAsync();
            FileListView.ItemsSource = files;
        }
    }
    public class FileModel
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
    public class FileService
    {
        private readonly HttpClient _httpClient;

        public FileService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<FileModel>> GetFilesAsync()
        {
            var response = await _httpClient.GetAsync("your_server_url_here");

            if (response.IsSuccessStatusCode)
            {
                var fileNames = await response.Content.ReadAsStringAsync();
                var files = fileNames.Split(',').Select(fileName => new FileModel { FileName = fileName, FileUrl = "your_file_url_here" }).ToList();
                return files;
            }

            return null;
        }
    }
}