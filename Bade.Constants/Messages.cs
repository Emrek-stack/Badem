namespace Bade.Constants
{
    public class Messages
    {
        public class Success
        {
            public const string Default = "İşlem Başarılı";

        }

        public class Error
        {
            public const string Default = "Hata oluştu";
            public const string Ambiguous = "Aynı kayıttan mevcut";
            public const string InvalidFile = "Dosya tipi uygun değildir.";
            public const string NotFoundFile = "Dosya bulunamadı.";
            public const string NotFound = "Bulunamadı.";
            public const string AmbiguousEmail = "E-posta adresi daha önce kullanılmış.";
            public const string UserNotFound = "Kullanıcı bulunamadı.";
        }
    }
}