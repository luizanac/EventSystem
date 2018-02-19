﻿using System;
using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;


namespace EventSystem.Shared.Entities
{
	public class Entity 
	{
		public Entity()
		{
			Id = Guid.NewGuid();
			CreateDate = DateTime.Now;
		}
		[Key]
		public Guid Id { get; set; }
		public DateTime CreateDate { get; set; }
		
	}
}