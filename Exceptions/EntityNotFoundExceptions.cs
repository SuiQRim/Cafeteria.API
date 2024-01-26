﻿namespace Profit_Food.API.Exceptions
{
	public class EntityNotFoundExceptions : Exception
	{
        public EntityNotFoundExceptions(Type type, string id) :
            base($"Entity ({type.Name}) with Id {id} is Not Found")
        { 
        }
    }
}