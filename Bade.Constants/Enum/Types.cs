namespace Bade.Constants.Enum
{
    namespace Types
    {
        /// <summary>
        /// View Componentlerinin tipini belirler
        /// Dynamic Componentler sunucu tarafında render edilirler, static Componentler sadece HTML içerirler.
        ///  </summary>
        public enum Component
        {
            Dynamic = 1,
            Static = 2
        }

        public enum Asset
        {
            Style = 1,
            Script = 2
        }

        public enum AuthorType
        {

            Author = 9,
            Reporter = 10,
            OuterAuthor = 11,
            Guest = 12,
            NetAuthor = 13

        }

        public enum ValidImage
        {
            jpeg = 1,
            jpg = 2,
            png = 3
        }

        public enum FileType
        {
            Image = 1,
            Pdf = 2,
            Doc = 3
        }

        public enum ContentType
        {
            All = 0,
            Article = 1,
            VideoGallery = 2,
            ImageGallery = 3
        }

        public enum ImageType
        {
            TeaserImage = 7,
            ArticleDetailImage = 9,
            AuthorImage=14
        }

        public enum GuidType
        {
            Digit ,
            Hyphens ,
            HyphensAndBrace,
      
        }

        public enum ImageResourceType
        {

            DHA = 4,
            Shutterstock = 5,
            Others =6
         
        }

        public enum AuthorImageAspectType
        {
            OneToOne=1
        }

        public enum Video
        {
        }        
    }
}