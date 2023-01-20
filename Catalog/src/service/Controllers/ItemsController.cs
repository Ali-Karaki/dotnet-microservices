using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using service.DTOs;

namespace service.ItemsController
{
  [ApiController] // api controller attribute
  [Route("items")]
  public class ItemsController : ControllerBase
  {
    // static so list is not recreated everytime an api is called
    private static readonly List<ItemDto> items = new()
    {
       new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.Now),
       new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.Now),
       new ItemDto(Guid.NewGuid(), "Bronze Sword", "Deals a small amount of damage", 20, DateTimeOffset.Now)
    };

    [HttpGet]
    public IEnumerable<ItemDto> Get()
    {
      return items;
    }

    // GET/items/{id}
    [HttpGet("{id}")]
    public /*ItemDto would be return type instead of ActionResult<ItemDto> if we were not returning the NotFound()*/ ActionResult<ItemDto> GetById(Guid id)
    {
      var item = items.Where(item => item.Id == id).SingleOrDefault();
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }

    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
    {
      var newItem = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.Now);
      items.Add(newItem);
      // indicate where we'd find new object / what route
      // 1- "GetById" is the function we already created above
      // 2- new {id = newItem.Id} is called anonymous type
      // 3- body of response
      // response header contains location of new item: localhost:5001/items/3ebb5...
      return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }

    [HttpPut("{id}")]
    public ActionResult<ItemDto> UpdateItem(Guid id, UpdateItemDto updatedItemDto)
    {
      var existingItem = items.Where(item => item.Id == id).SingleOrDefault();
      if (existingItem == null)
      {
        return NotFound();
      }
      var updatedItem = existingItem with
      {
        Name = updatedItemDto.Name,
        Description = updatedItemDto.Description,
        Price = updatedItemDto.Price,
      };
      var index = items.FindIndex(item => item.Id == id);
      items[index] = updatedItem;
      return updatedItem;
    }

    [HttpDelete("{id}")]
    // NoContent() to not return anything
    public IActionResult DeleteItem(Guid id)
    {
      var index = items.FindIndex(item => item.Id == id);
      if (index < 0)
      {
        return NotFound();
      }
      items.RemoveAt(index);
      return NoContent();
    }
  }
}