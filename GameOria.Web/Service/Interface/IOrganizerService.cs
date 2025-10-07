using GameOria.Shared.DTOs.Organizer;

namespace GameOria.Web.Service.Interface
{
    public interface IOrganizerService
    {
        Task <HttpResponseMessage>  RequestBecomeOrganizer(OrganizerRequestDto organizerRequestDto);
    }
}
