namespace Catalog.WebUI.Models.Link;

public class LinkedCollectionResourceDto<T> : ILinkResource where T : class, ILinkResource
{
    public IEnumerable<T> Value { get; set; }

    public IEnumerable<LinkDto> Links { get; set; }

    public LinkedCollectionResourceDto(IEnumerable<T> value, IEnumerable<LinkDto> links)
    {
        Value = value;
        Links = links;
    }
}
