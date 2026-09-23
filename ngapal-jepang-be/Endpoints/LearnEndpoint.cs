using ngapal_jepang_be.Data;

namespace ngapal_jepang_be.Endpoints
{
    public static class LearnEndpoint
    {
        public static void MapLearnEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("learn/");

        }

        
    }
}
