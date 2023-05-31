namespace NetAdvancedShop.Common.WebUI.Extensions;

public static class SwaggerConfigurationExtensions
{
    public static SwaggerGenOptions IncludeXmlCommentsFromAssemblyOf<TType>(this SwaggerGenOptions options) =>
        options.IncludeXmlCommentsFromAssemblyOf(typeof(TType));

    public static SwaggerGenOptions IncludeXmlCommentsFromAssemblyOf(this SwaggerGenOptions options, Type type)
    {
        var xmlFile = $"{type.Assembly.GetName().Name}.xml";

        // documentation for swagger tool
        if (File.Exists(xmlFile))
        {
            options.IncludeXmlComments(xmlFile);
            return options;
        }

        var basePath = AppContext.BaseDirectory;
        var xmlFilePath = Path.Combine(basePath, xmlFile);

        // documentation locally
        if (File.Exists(xmlFilePath))
        {
            options.IncludeXmlComments(xmlFilePath);
            return options;
        }

        return options;
    }
}
