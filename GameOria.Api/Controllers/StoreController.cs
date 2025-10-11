using GameOria.Application.Stores.DTOs;
using GameOria.Application.Stores.Service;
using GameOria.Domains.Entities.Stores;
using GameOria.Shared.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GameOria.Api.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepository;


        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public  IActionResult CreateMyStore( )
        {
            return View();
        }
        [HttpGet("Get-Store-Owner-By-Id")]
        public async Task<IActionResult> GetStoreOwnerByIdAsync(Guid Id)
        {
            var myStore = _storeRepository.GetStoreOwnerByIdAsync(Id);
            return Ok(myStore);
        }
        public async Task<IActionResult> CreateMyStore(Store store)
        {
            var newStore = _storeRepository.CreateAsync(store);
            return Ok();
        }



        [HttpGet("GetAllStores")]
        public async Task<IActionResult> GetAll()
        {
            var stores = await _storeRepository.GetAllAsync();
            return Ok(stores);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromBody] Guid id)
        {
            var store = await _storeRepository.GetByIdAsync(id);
            if (store == null) return NotFound();
            return Ok(store);
        }

        [HttpPost("EditStore")]
        public async Task<IActionResult> EditStore([FromBody] Guid id, BaseStore baseStore)
        {
            var store = await _storeRepository.GetByIdAsync(id);
            if (store == null)
                return NotFound(new APIResponse { Success = false, Message = "Store not found" });

            store.IsVerified = baseStore.IsVerified;
            store.Name = baseStore.StoreName;

            await _storeRepository.EditAsync(store); // now accepts Store

            return Ok(new APIResponse { Success = true, Message = "Store Edit successfully" });
        }

        [HttpPost("CompleteStoreProfile")]
        public async Task<IActionResult> CompleteStoreProfile(Guid id, [FromBody] CompleteStore completeStore, string type)
        {
            var existingStore = await _storeRepository.GetStoreOwnerByIdAsync(id);
            if (existingStore == null)
                return NotFound(new APIResponse { Success = false, Message = "StoreOwner not found" });
            if (type == "Complete")
            {
                if (existingStore.IsVerified)
                    return Ok(new APIResponse { Success = false, Message = "Store already completed" });

                existingStore.OwnerFirstName = completeStore.OwnerFirstName;
                existingStore.OwnerLastName = completeStore.OwnerLastName;
                existingStore.Email = completeStore.OwnerEmail;
                existingStore.Phone = completeStore.OwnerPhone;
                existingStore.Name = completeStore.StoreName;
                existingStore.LogoUrl = completeStore.LogoUrl;
                existingStore.CoverImageUrl = completeStore.CoverImageUrl;
                existingStore.IsVerified = true;
            }
            // Update store info (after completion)
            else
            {
                existingStore.OwnerFirstName = completeStore.OwnerFirstName;
                existingStore.OwnerLastName = completeStore.OwnerLastName;
                existingStore.Email = completeStore.OwnerEmail;
                existingStore.Phone = completeStore.OwnerPhone;
                existingStore.Name = completeStore.Title;
                existingStore.LogoUrl = completeStore.LogoUrl;
                existingStore.CoverImageUrl = completeStore.CoverImageUrl;
            }

            await _storeRepository.CompleteStoreProfile(existingStore);
            if (type == "Complete")
                return Ok(new APIResponse { Success = true, Message = "Store completed successfully" });
            else
                return Ok(new APIResponse { Success = true, Message = "Store Updated successfully" });
        }

        [HttpPost("DeleteStore")]
        public async Task<IActionResult> DeleteStore([FromBody] Guid id)
        {
            var success = await _storeRepository.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        //// GET: api/Store/{id}/Games
        //[HttpGet("{id:guid}/Games")]
        //public async Task<IActionResult> GetStoreGames(Guid id)
        //{
        //    var store = await _storeRepository.GetByIdAsync(id);
        //    if (store == null) return NotFound();

        //    return Ok(store.Games);
        //}

        //// GET: api/Store/{id}/Cards
        //[HttpGet("{id:guid}/Cards")]
        //public async Task<IActionResult> GetStoreCards(Guid id)
        //{
        //    var store = await _storeRepository.GetByIdAsync(id);
        //    if (store == null) return NotFound();

        //    return Ok(store.Cards);
        //}
    }
}
