﻿using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelatedUsersDtos;

public class DeleteRelatedUsersDto
{
    [Required]
    public string UserAId {  get; set; }
    [Required]
    public string UserBId { get; set; }
}
